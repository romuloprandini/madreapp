using MadreApp.Pages;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class PresentationLoginViewModel : BaseViewModel
    {
        private ICommand _registerCommand;
        private ICommand _loginCommand;

        public ICommand RegisterCommand => _registerCommand ?? (_registerCommand = new Command(ExecuteRegister));
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new Command(ExecuteLogin));

        public void ExecuteRegister()
        {
            IsBusy = true;
            Application.Current.MainPage = new PhoneRegisterPage();
            IsBusy = false;
        }

        public void ExecuteLogin()
        {
            IsBusy = true;
            Application.Current.MainPage = new LoginPage();
            IsBusy = false;
        }

    }
}
