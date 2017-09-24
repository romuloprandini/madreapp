using MadreApp.Helpers;
using MvvmHelpers;

namespace MadreApp.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        private string _phone;
        private string _name;
        private string _email;
        private string _gender;
        private string _birthday;
        private string _fiscalNumber;
        private string _madreCardNumber;
        private bool _madreCardActive;
        private string _madreCardBalance;
   
        public ProfileViewModel()
        {
            _phone = Settings.Phone;
            _name = Settings.Name;
            _email = Settings.Email;
            _gender = Settings.Gender;
            _birthday = Settings.Birthday;
            _fiscalNumber = Settings.FiscalNumber;
            _madreCardNumber = Settings.MadreCardNumber;
            _madreCardActive = Settings.MadreCardActive;
            _madreCardBalance = Settings.MadreCardBalance;          
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

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                SetProperty(ref _name, value);
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

        public string Gender
        {
            get { return _gender; }
            set
            {
                if (_gender == value) return;
                SetProperty(ref _gender, value);
            }
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

        public string FiscalNumber
        {
            get { return _fiscalNumber; }
            set
            {
                if (_fiscalNumber == value) return;
                SetProperty(ref _fiscalNumber, value);              
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
    }
}