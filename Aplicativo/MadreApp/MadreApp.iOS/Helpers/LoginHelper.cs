using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Firebase.Auth;
using MadreApp.Interfaces;
using System.Diagnostics;
using MadreApp.Models;
using System.Threading.Tasks;
using ObjCRuntime;
using iAd;

namespace MadreApp.iOS.Helpers
{
    public class LoginHelper : ILogin
    {
        private Auth _firebaseAuth;
        private Firebase.Auth.User _firebaseUser;
        private string _verificationId;
        private MadreApp.Models.User _user;

        public LoginHelper()
        {
            _firebaseAuth = Auth.DefaultInstance;
            _firebaseUser = _firebaseAuth.CurrentUser;
            UpdateUser();
            _firebaseAuth.AddAuthStateDidChangeListener(OnAuthStateChanged);
        }

        public bool IsLogged => !(_firebaseUser == null || string.IsNullOrEmpty(_firebaseUser.PhoneNumber));
        public MadreApp.Models.User LoggedUser => _user;

        public IntPtr Handle => throw new NotImplementedException();

        public event EventHandler OnVerifyPhoneNumberCodeSent;
        public event EventHandler OnVerifyPhoneNumberSucess;
        public event VerifyPhoneNumberFail OnVerifyPhoneNumberFail;

        public void VerifyPhoneNumber(string phoneNumber)
        {
            PhoneAuthProvider.DefaultInstance.VerifyPhoneNumber(phoneNumber, OnVerificationCompleted);
        }

        private void OnVerificationCompleted(string verificationId, NSError error)
        {
            _verificationId = verificationId;
            NSUserDefaults.StandardUserDefaults.SetString(verificationId, "authVerificationID");

            OnVerifyPhoneNumberCodeSent?.Invoke(this, EventArgs.Empty);
        }

        public void AuthenticatePhoneNumber(string codeSMS)
        {
            _verificationId = NSUserDefaults.StandardUserDefaults.StringForKey("AuthVerificationID");
            SignInWithPhoneNumber(PhoneAuthProvider.DefaultInstance.GetCredential(_verificationId, codeSMS));
        }

        private void SignInWithPhoneNumber(PhoneAuthCredential credential)
        {
            try
            {
                _firebaseAuth.SignIn(credential, (user, error) =>
                {
                    if (error != null)
                    {
                        Console.WriteLine(error.LocalizedDescription);
                        OnVerifyPhoneNumberFail?.Invoke("Não foi possível validar as informações. Verifique se o código informado está correto");
                        return;
                    }

                    OnVerifyPhoneNumberSucess?.Invoke(this, EventArgs.Empty);
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                OnVerifyPhoneNumberFail?.Invoke("Não foi possível validar as informações. Verifique se o código informado está correto");
            }

        }

        public Task<bool> LoginWithEmailAndPassword(string email, string password)
        {
            return Task.Factory.StartNew(() =>
            {
                var retorno = false;
                _firebaseAuth.SignIn(EmailAuthProvider.GetCredential(email, password), (user, error) =>
                {
                    if (error != null)
                    {
                        Console.WriteLine(error.LocalizedDescription);
                        return;
                    }
                    retorno = true;
                });

                return retorno;
            });
        }

        public Task<bool> RegisterWithEmailAndPassword(string email, string password, string displayName, string photoUrl)
        {
            if (_firebaseUser == null) throw new Exception("Usuário não habilitado para login por email");

            return Task.Factory.StartNew(() =>
            {
                var retorno = false;
                _firebaseUser.Link(EmailAuthProvider.GetCredential(email, password), async (user, error) =>
                {
                    if (error != null)
                    {
                        Console.WriteLine(error.LocalizedDescription);
                        return;
                    }

                    _firebaseUser.SendEmailVerification((errorEmail) =>
                    {
                        if (error != null)
                        {
                            Console.WriteLine(error.LocalizedDescription);
                            return;
                        }
                    });

                    await UpdateProfile(displayName, photoUrl);
                    retorno = true;
                });

                return retorno;
            });
        }

        public Task<bool> UpdateProfile(string name, string photoUrl)
        {
            return Task.Factory.StartNew(() =>
            {
                var builder = _firebaseUser.ProfileChangeRequest();
                if (name != null)
                {
                    builder.DisplayName = name;
                }
                if (photoUrl != null)
                {
                    builder.PhotoUrl = new NSUrl(photoUrl);
                }

                var retorno = true;
                builder.CommitChanges(error =>
                {
                    if (error != null)
                    {
                        Console.WriteLine(error.LocalizedDescription);
                    }
                    retorno = false;
                });

                UpdateUser();

                return retorno;
            });
        }

        public Task<bool> UpdatePassword(string email, string currentPassword, string newPassword)
        {
            if (_firebaseUser == null) throw new Exception("Usuário não encontrado");

            return Task.Factory.StartNew(() =>
            {
                var retorno = false;
                _firebaseUser.Reauthenticate(EmailAuthProvider.GetCredential(email, currentPassword), error =>
                {
                    if(error != null)
                    {
                        Console.WriteLine(error.LocalizedDescription);
                        return;
                    }

                    _firebaseUser.UpdatePassword(newPassword, errorPassword =>
                    {
                        if(errorPassword != null)
                        {
                            Console.WriteLine(errorPassword.LocalizedDescription);
                            return;
                        }

                        retorno = true;
                    });
                });
                return retorno;
            });
        }

        public Task<bool> UpdateEmail(string currentEmail, string currentPassword, string newEmail)
        {
            if (_firebaseUser == null) throw new Exception("Usuário não encontrado");

            return Task.Factory.StartNew(() =>
             {
                 var retorno = false;
                 _firebaseUser.Reauthenticate(EmailAuthProvider.GetCredential(currentEmail, currentPassword), error =>
                 {
                     if (error != null)
                     {
                         Console.WriteLine(error.LocalizedDescription);
                         return;
                     }

                     _firebaseUser.UpdateEmail(newEmail, errorEmail =>
                     {
                         if (errorEmail != null)
                         {
                             Console.WriteLine(errorEmail.LocalizedDescription);
                             return;
                         }

                         retorno = true;
                     });
                 });
                 return retorno;
             });
        }

        private void OnAuthStateChanged(Auth auth, Firebase.Auth.User user)
        {
            _firebaseUser = user;
            UpdateUser();
        }

        private void UpdateUser()
        {
            if (_firebaseUser != null)
            {
                _user = new MadreApp.Models.User
                {
                    Name = _firebaseUser.DisplayName,
                    Phone = _firebaseUser.PhoneNumber,
                    Email = _firebaseUser.Email,
                    FirebaseUid = _firebaseUser.Uid
                };
            }
        }

        public void Logout()
        {
            _firebaseAuth.SignOut(out NSError error);
        }

        public void Close()
        {
            if (_firebaseAuth != null)
            {
                //_firebaseAuth.RemoveAuthStateDidChangeListener();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}