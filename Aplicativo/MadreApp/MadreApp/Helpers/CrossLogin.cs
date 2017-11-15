using System;

using MadreApp.Interfaces;
using Firebase.Auth;
using System.Threading.Tasks;
using MadreApp.Models;
#if __ANDROID__
using MadreApp.Droid.Helpers;
#elif __IOS__
using MadreApp.iOS.Helpers;
#endif

namespace MadreApp.Helpers
{
    public class CrossLogin
    {
        private ILogin _login;

        private static CrossLogin _instance;
        public static CrossLogin Instance
        {
            get
            {
                return _instance ?? throw new NullReferenceException("Inicitalize the CrossLogin first using CrossLogin.Initialize");
            }
        }

        private CrossLogin()
        {
            _login = new LoginHelper();
            _login.OnVerifyPhoneNumberCodeSent += OnVerifyPhoneNumberCodeSent;
            _login.OnVerifyPhoneNumberSucess += OnVerifyPhoneNumberSucess;
            _login.OnVerifyPhoneNumberFail += OnVerifyPhoneNumberFail;
        }

        public Action OnShouldValidateCodeSMS { get; set; }

        internal void Logout()
        {
            _login.Logout();
        }

        public Action OnSuccess { get; set; }
        public Action<string> OnError { get; set; }

        public bool IsLogged => _login.IsLogged;

        public MadreApp.Models.User LoggedUser => _login.LoggedUser;

        public void VerifyPhoneNumber(string phoneNumber)
        {
            _login.VerifyPhoneNumber(phoneNumber);
        }

        public void ValidateCodeSMS(string codeSMS)
        {
            _login.AuthenticatePhoneNumber(codeSMS);
        }
        
        private void OnVerifyPhoneNumberCodeSent(object sender, EventArgs e)
        {
            OnShouldValidateCodeSMS?.Invoke();
        }

        public async Task<bool> Register(string email, string password, string name, string photoUrl)
        {
            return await _login.RegisterWithEmailAndPassword(email, password, name, photoUrl);
        }

        private void OnVerifyPhoneNumberSucess(object sender, EventArgs e)
        {
            OnSuccess?.Invoke();
        }

        private void OnVerifyPhoneNumberFail(string message)
        {
            OnError?.Invoke(message);
        }

        public static void Initialize()
        {
            if(_instance == null)
            {
                _instance = new CrossLogin();
            }
        }

        public Task<bool> Login(string email, string password)
        {
            return _instance._login.LoginWithEmailAndPassword(email, password);
        }

        public Task<bool> UpdatePassword(string email, string currentPassword, string newPassword)
        {
            return _instance._login.UpdatePassword(email, currentPassword, newPassword);
        }

        public Task<bool> UpdateProfile(string name, string photoUrl)
        {
           return  _instance._login.UpdateProfile(name, photoUrl);
        }

        public static void Dispose()
        {
            if(_instance != null && _instance._login != null)
            {
                _instance._login.Close();

                _instance._login.OnVerifyPhoneNumberSucess -= _instance.OnVerifyPhoneNumberSucess;
                _instance._login.OnVerifyPhoneNumberFail -= _instance.OnVerifyPhoneNumberFail;
            }
        }
    }
}
