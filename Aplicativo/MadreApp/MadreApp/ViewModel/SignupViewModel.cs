using MadreApp.Behaviors;
using MadreApp.Customs;
using MadreApp.Helpers;
using MadreApp.Pages;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Newtonsoft.Json.Linq;

namespace MadreApp.ViewModel
{
    public class SignupViewModel : ProfileViewModel
    {
        private string _confirmationPassword;
        private string _madreCardPassword;
        private ICommand _signupCommand;

        public string ConfirmationPassword
        {
            get { return _confirmationPassword; }
            set
            {
                if (_confirmationPassword == value) return;
                SetProperty(ref _confirmationPassword, value);
            }
        }

        public string MadreCardPassword
        {
            get { return _madreCardPassword; }
            set
            {
                if (_madreCardPassword == value) return;
                SetProperty(ref _madreCardPassword, value);
            }
        }

        public ICommand SignupCommand => _signupCommand ?? (_signupCommand = new ButtonCommand(() =>
            {
                IsBusy = true;
                
                Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            var newUser = Settings.CreateNewUser(Phone, Name, Email, Gender, Birthday, FiscalNumber, MadreCardPassword);
                            var result = await HttpRequest.Instance.GetOneRequest<JObject>("/usuario/" + newUser);

                            Settings.Update(result);

                            await Application.Current.MainPage?.DisplayAlert("Informação da Conta", Settings.GetAccountInformation(), "Ok");
                            Application.Current.MainPage = new ProfilePage();
                        }
                        catch (Exception e)
                        {
                            await Application.Current.MainPage?.DisplayAlert("Error", e.Message, "Ok");
                            Application.Current.MainPage = new MainPage();
                        }
                        IsBusy = false;
                        
                    });
            }, CanSignup, this));

        public SignupViewModel()
            : base()
        {
            _madreCardPassword = Settings.MadreCardPassword;
        }

        private bool CanSignup()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                Validators.PhoneNumberValidator(Phone) &&
                Validators.FiscalNumberValidator(FiscalNumber) &&
                Validators.BirthdayValidator(Birthday) &&
                Validators.EmailValidator(Email) &&
                Validators.PasswordValidator(MadreCardPassword) &&
                MadreCardPassword == ConfirmationPassword;
        }
    }
}