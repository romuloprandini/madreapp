using MadreApp.Behaviors;
using MadreApp.Customs;
using MadreApp.Helpers;
using MadreApp.Pages;
using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace MadreApp.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        private string _birthday;
        private string _email;
        private string _fiscalNumber;
        private string _madreCard;
        private string _name;
        private string _phone;
        private string _password;
        private ICommand _madreCardLoginCommand;
        private ICommand _newUserLoginCommand;

        public ICommand MadreCardLoginCommand => _madreCardLoginCommand ?? (_madreCardLoginCommand = new ButtonCommand(async () =>
            {
                Settings.FiscalNumber = FiscalNumber;
                Settings.MadreCardPassword = Password;
                Settings.Phone = Phone;

                try
                {
                    var result = await HttpRequest.Instance.GetOneRequest<object>("/madrecard/" + Settings.MadreCardLogin());
                }
                catch (Exception e)
                {
                    
                }
                
                Application.Current.MainPage = new MainPage();

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
            _madreCard = Settings.MadreCardNumber;
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

        public string MadreCard
        {
            get { return _madreCard; }
            set
            {
                if (_madreCard == value) return;
                SetProperty(ref _madreCard, value);
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

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;
                SetProperty(ref _password, value);
            }
        }
        
        private bool CanExecuteMadreCard()
        {
            return
                Validators.PhoneNumberValidator(Phone) &&
                Validators.PasswordValidator(Password) &&
                Validators.FiscalNumberValidator(FiscalNumber);
        }

        private bool CanExecuteNewUser()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                Validators.PhoneNumberValidator(Phone);
        }
    }
}