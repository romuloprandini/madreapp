using MadreApp.Helpers;
using MadreApp.Pages;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class ValidatePhoneViewModel : BaseNavigationViewModel
    {
        private string _codeSMS;
        private ICommand _resendSMSCommand;
        private ICommand _validadeCodeSMSCommand;

        public ValidatePhoneViewModel(string name)
        {
            Title = "Validar Número Celular";

            CrossLogin.Instance.OnSuccess = async () =>
            {
                IsBusy = true;
                await CrossLogin.Instance.UpdateProfile(name, null);
                Application.Current.MainPage = new MainPage();
                IsBusy = false;
            };
        }

        public string CodeSMS
        {
            get
            {
                return _codeSMS;
            }
            set
            {
                _codeSMS = value;
                SetProperty(ref _codeSMS, value);
            }
        }

        public ICommand ValidadeCodeSMSCommand => _validadeCodeSMSCommand ?? (_validadeCodeSMSCommand = new Command(VerifyCodeSMS));

        public ICommand ResendSMSCommand => _resendSMSCommand ?? (_resendSMSCommand = new Command(ResendSMS));

        private void VerifyCodeSMS()
        {
            CrossLogin.Instance.ValidateCodeSMS(_codeSMS);   
        }

        private void ResendSMS()
        {
            throw new NotImplementedException();
        }
    }
}
