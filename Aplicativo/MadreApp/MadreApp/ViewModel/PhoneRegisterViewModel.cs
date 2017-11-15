using MadreApp.Behaviors;
using MadreApp.Customs;
using MadreApp.Helpers;
using MadreApp.Pages;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class PhoneRegisterViewModel : BaseViewModel
    {
        private string _name;
        private string _phone;
        private ICommand _registerCommand;
        private ICommand _backCommand;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                SetProperty(ref _name, value);
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone == value) return;
                SetProperty(ref _phone, value);
            }
        }


        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(() =>
        {
            Application.Current.MainPage = new PresentationPage(3);
        }));
        public ICommand RegisterCommand => _registerCommand ?? (_registerCommand = new ButtonCommand(ExecuteRegister, CanExecuteRegister, this));
        
        public PhoneRegisterViewModel()
        {
        }

        private void ExecuteRegister()
        {
            IsBusy = true;
            CrossLogin.Instance.OnShouldValidateCodeSMS = () =>
            {
                Application.Current.MainPage = new ValidatePhonePage(_name);
                IsBusy = false;
            };
            
            CrossLogin.Instance.OnError = (error) =>
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Erro",
                    Message = error,
                    Cancel = "Ok"
                }, MessageKeys.DisplayAlert);

                IsBusy = false;
            };
            try
            {
                CrossLogin.Instance.VerifyPhoneNumber(_phone);
            }
            catch(Exception ex)
            {

                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Erro",
                    Message = ex.Message,
                    Cancel = "Ok"
                }, MessageKeys.DisplayAlert);
            }
            IsBusy = true;
        }
        
        private bool CanExecuteRegister()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                Validators.PhoneNumberValidator(Phone);
        }
    }
}