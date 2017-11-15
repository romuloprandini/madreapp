using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using MadreApp.Helpers;
using Firebase.Auth;
using MadreApp.Interfaces;
using Firebase;
using Plugin.CurrentActivity;
using Java.Util.Concurrent;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MadreApp.Models;

namespace MadreApp.Droid.Helpers
{
    public class LoginHelper : PhoneAuthProvider.OnVerificationStateChangedCallbacks, FirebaseAuth.IAuthStateListener, ILogin
    {
        private FirebaseAuth _firebaseAuth;
        private FirebaseUser _firebaseUser;
        private string _verificationId;
        private MadreApp.Models.User _user;
        private PhoneAuthProvider.ForceResendingToken _forceResendingToken;

        public event EventHandler OnVerifyPhoneNumberCodeSent;
        public event EventHandler OnVerifyPhoneNumberSucess;
        public event VerifyPhoneNumberFail OnVerifyPhoneNumberFail;

        public LoginHelper()
        {
            _firebaseAuth = FirebaseAuth.Instance;
            _firebaseUser = _firebaseAuth.CurrentUser;
            UpdateUser();
            _firebaseAuth.AddAuthStateListener(this);
        }

        public bool IsLogged => !(_firebaseUser == null || string.IsNullOrEmpty(_firebaseUser.PhoneNumber));
        public MadreApp.Models.User LoggedUser => _user;
        
        #region Phone Login
        public void VerifyPhoneNumber(string phoneNumber)
        {
            var phoneNumberDigitsOnly = Regex.Replace(phoneNumber, "[^A-Za-z0-9 _]+", "");
            if (phoneNumberDigitsOnly.Length < 11) throw new Exception("Phone in wrong format");
            PhoneAuthProvider.Instance.VerifyPhoneNumber(phoneNumber, 60, TimeUnit.Seconds, CrossCurrentActivity.Current.Activity, this);
        }

        public override void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken)
        {
            base.OnCodeSent(verificationId, forceResendingToken);

            _verificationId = verificationId;
            _forceResendingToken = forceResendingToken;

            OnVerifyPhoneNumberCodeSent?.Invoke(this, EventArgs.Empty);
        }

        public override void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            SignInWithPhoneNumber(credential);
        }

        public override void OnVerificationFailed(FirebaseException exception)
        {
            OnVerifyPhoneNumberFail?.Invoke(exception.Message);
        }

        public void AuthenticatePhoneNumber(string codeSMS)
        {
            SignInWithPhoneNumber(PhoneAuthProvider.GetCredential(_verificationId, codeSMS));
        }

        private async void SignInWithPhoneNumber(PhoneAuthCredential credential)
        {
            try
            {
                var authResult = await _firebaseAuth.SignInWithCredentialAsync(credential);
                if (authResult == null || authResult.User == null)
                {
                    OnVerifyPhoneNumberFail?.Invoke("Não foi possível validar as informações. Verifique se o código informado está correto");
                }

                OnVerifyPhoneNumberSucess?.Invoke(this, EventArgs.Empty);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                OnVerifyPhoneNumberFail?.Invoke("Código inválido. Verifique o código enviado e tente novamente");
            }
            catch (FirebaseTooManyRequestsException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                OnVerifyPhoneNumberFail?.Invoke("Muitas tentativas de validações enviadas. Aguarde um pouco antes realizar de novas tentativas");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                OnVerifyPhoneNumberFail?.Invoke("Não foi possível validar as informações. Verifique se o código informado está correto");
            }

        }
        #endregion

        #region Email Login
        public async Task<bool> RegisterWithEmailAndPassword(string email, string password, string displayName, string photoUrl)
        {
            if (_firebaseUser == null) throw new Exception("Usuário não habilitado para login por email");
            
            var authResult = await _firebaseUser.LinkWithCredentialAsync(EmailAuthProvider.GetCredential(email, password));
            if (authResult == null || authResult.User == null) return false;

            _firebaseUser.SendEmailVerification();

            await UpdateProfile(displayName, photoUrl);

            return true;
        }

        public async Task<bool> UpdateEmail(string currentEmail, string currentPassword, string newEmail)
        {
            if (_firebaseUser == null) throw new Exception("Usuário não encontrado");

            await _firebaseUser.ReauthenticateAsync(EmailAuthProvider.GetCredential(currentEmail, currentPassword));
            await _firebaseUser.UpdateEmailAsync(newEmail);

            UpdateUser();

            return true;
        }

        public async Task<bool> UpdatePassword(string email, string currentPassword, string newPassword)
        {
            if (_firebaseUser == null) throw new Exception("Usuário não encontrado");

            await _firebaseUser.ReauthenticateAsync(EmailAuthProvider.GetCredential(email, currentPassword));
            await _firebaseUser.UpdatePasswordAsync(newPassword);
            
            return true;
        }

        public async Task<bool> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var authResult = await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
                return authResult != null && authResult.User != null;
            }
            catch(FirebaseAuthInvalidUserException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return false;
        }
        #endregion
        
        public async Task<bool> UpdateProfile(string name, string photoUrl)
        {
            var builder = new UserProfileChangeRequest.Builder();
            if (name != null)
            {
                builder.SetDisplayName(name);
            }
            if (photoUrl != null)
            {
                builder.SetPhotoUri(Android.Net.Uri.Parse(photoUrl));
            }
            await _firebaseUser.UpdateProfileAsync(builder.Build());

            UpdateUser();

            return true;
        }

        public void OnAuthStateChanged(FirebaseAuth auth)
        {
            _firebaseUser = auth.CurrentUser;
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
            _firebaseAuth.SignOut();
        }

        public void Close()
        {
            if (_firebaseAuth != null)
            {
                _firebaseAuth.RemoveAuthStateListener(this);
            }
        }
    }
}