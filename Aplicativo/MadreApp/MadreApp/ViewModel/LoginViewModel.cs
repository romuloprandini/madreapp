using MadreApp.Behaviors;
using MadreApp.Customs;
using MadreApp.Helpers;
using MadreApp.Pages;
using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace MadreApp.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private bool _isNewUserLogin;
        private string _fiscalNumber;
        private string _madreCardPassword;
        private string _name;
        private string _phone;
        private ICommand _madreCardLoginCommand;
        private ICommand _newUserLoginCommand;

        public ICommand MadreCardLoginCommand => _madreCardLoginCommand ?? (_madreCardLoginCommand = new ButtonCommand(() =>
            {
                IsBusy = true;
                Settings.Phone = Phone;
                Settings.FiscalNumber = FiscalNumber;
                Settings.MadreCardPassword = MadreCardPassword;

                Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                        var result = await HttpRequest.Instance.GetOneRequest<JObject>("/madrecard/" + Settings.FiscalNumber);

                            Settings.Update(result);

                            await Application.Current.MainPage?.DisplayAlert("Informação da Conta", Settings.GetAccountInformation(), "Ok");
                            Application.Current.MainPage = new MainPage();
                        }
                        catch
                        {
                            await Application.Current.MainPage?.DisplayAlert("CPF não encontrado", $"Usuário com CPF {FiscalNumber} não possui conta.", "Ok");
                            Application.Current.MainPage = new LoginPage(true);
                        }
                        IsBusy = false;

                    });              
            }, CanExecuteMadreCard, this));

        public ICommand NewUserLoginCommand => _newUserLoginCommand ?? (_newUserLoginCommand = new ButtonCommand(() =>
            {
                Application.Current.MainPage = new MainPage();
            }, CanExecuteNewUser, this));

        public LoginViewModel(bool isNewUserLogin)
        {
            _isNewUserLogin = isNewUserLogin;
            _phone = Settings.Phone;
            _name = Settings.Name;
            _fiscalNumber = Settings.FiscalNumber;
            _madreCardPassword = Settings.MadreCardPassword;
        }

        public bool IsNewUserLogin => _isNewUserLogin;

        public bool IsMadreCardLogin => !_isNewUserLogin;

        public string FiscalNumber
        {
            get { return _fiscalNumber; }
            set
            {
                if (_fiscalNumber == value) return;
                SetProperty(ref _fiscalNumber, value);              
            }
        }

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

        public string MadreCardPassword
        {
            get { return _madreCardPassword; }
            set
            {
                if (_madreCardPassword == value) return;
                SetProperty(ref _madreCardPassword, value);
            }
        }
        
        private bool CanExecuteMadreCard()
        {
            return IsMadreCardLogin &&
                Validators.PhoneNumberValidator(Phone) &&
                Validators.PasswordValidator(MadreCardPassword) &&
                Validators.FiscalNumberValidator(FiscalNumber);
        }

        private bool CanExecuteNewUser()
        {
            return IsNewUserLogin &&
                !string.IsNullOrWhiteSpace(Name) &&
                Validators.PhoneNumberValidator(Phone);
        }
    }
}