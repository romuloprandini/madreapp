using MadreApp.Behaviors;
using MadreApp.Customs;
using MadreApp.Helpers;
using MadreApp.Pages;
using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Newtonsoft.Json.Linq;

namespace MadreApp.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        private string _birthday;
        private string _email;
        private string _fiscalNumber;
        private string _madreCardNumber;
        private bool _madreCardActive;
        private string _madreCardBalance;
        private string _madreCardPassword;
        private string _name;
        private string _phone;
        private ICommand _madreCardLoginCommand;
        private ICommand _newUserLoginCommand;

        public ICommand MadreCardLoginCommand => _madreCardLoginCommand ?? (_madreCardLoginCommand = new ButtonCommand(() =>
            {
                IsBusy = true;
                Device.BeginInvokeOnMainThread(async () =>
                    {
                        var result = await HttpRequest.Instance.GetOneRequest<JObject>("/madrecard/" + Settings.FiscalNumber);

                        Settings.MadreCardNumber = result["numero"].ToString();
                        Settings.MadreCardActive = bool.Parse(result["ativo"].ToString());
                        Settings.MadreCardBalance = result["saldo"].ToString();
                        Settings.MadreCardPassword = MadreCardPassword;
                        Settings.FiscalNumber = result["cpf"].ToString();
                        Settings.Name = result["nome"].ToString();
                        Settings.Phone = Phone;

                        IsBusy = false;
                        Application.Current.MainPage = new MainPage();
                    });              
            }, CanExecuteMadreCard, this));

        public ICommand NewUserLoginCommand => _newUserLoginCommand ?? (_newUserLoginCommand = new ButtonCommand(() =>
            {
                Application.Current.MainPage = new MainPage();
            }, CanExecuteNewUser, this));

        public ProfileViewModel()
        {
            _birthday = Settings.Birthday;
            _email = Settings.Email;
            _fiscalNumber = Settings.FiscalNumber;
            _name = Settings.Name;
            _madreCardNumber = Settings.MadreCardNumber;
            _madreCardActive = Settings.MadreCardActive;
            _madreCardBalance = Settings.MadreCardBalance;
            _madreCardPassword = Settings.MadreCardPassword;
            _phone = Settings.Phone;
        }

        public string Birthday
        {
            get { return _birthday; }
            set
            {
                if (_birthday == value) return;
                SetProperty(ref _birthday, value);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value) return;
                SetProperty(ref _email, value);
            }
        }

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

        public string MadreCardNumber
        {
            get { return _madreCardNumber; }
            set
            {
                if (_madreCardNumber == value) return;
                SetProperty(ref _madreCardNumber, value);
            }
        }

        public bool MadreCardActive
        {
            get { return _madreCardActive; }
            set
            {
                if (_madreCardActive == value) return;
                SetProperty(ref _madreCardActive, value);
            }
        }

        public string MadreCardBalance
        {
            get { return _madreCardBalance; }
            set
            {
                if (_madreCardBalance == value) return;
                SetProperty(ref _madreCardBalance, value);
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
            return
                Validators.PhoneNumberValidator(Phone) &&
                Validators.PasswordValidator(MadreCardPassword) &&
                Validators.FiscalNumberValidator(FiscalNumber);
        }

        private bool CanExecuteNewUser()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                Validators.PhoneNumberValidator(Phone);
        }
    }
}