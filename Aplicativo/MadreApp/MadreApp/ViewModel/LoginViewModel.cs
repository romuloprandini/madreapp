using MadreApp.Behaviors;
using MadreApp.Customs;
using MadreApp.Helpers;
using MadreApp.Pages;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class LoginViewModel : BaseNavigationViewModel
    {
        private string _email;
        private string _password;
        private ICommand _loginCommand;
        private ICommand _facebookLoginCommand;
        private ICommand _facebookSuccessCommand;
        private ICommand _facebookErrorCommand;
        private ICommand _facebookCancelCommand;
        private ICommand _backCommand;

        public LoginViewModel()
        {
            IsBusy = false;
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

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;
                SetProperty(ref _password, value);
            }
        }

        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(ExecuteBackCommand));
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new ButtonCommand(ExecuteLoginCommand, ValidateLoginCommand, this));
        public ICommand FacebookLoginCommand => _facebookLoginCommand ?? (_facebookLoginCommand = new Command(() => IsBusy = true));
        public ICommand FacebookSuccessCommand => _facebookSuccessCommand ?? (_facebookSuccessCommand = new Command(ExecuteFacebookSuccessCommand));
        public ICommand FacebookErrorCommand => _facebookErrorCommand ?? (_facebookErrorCommand = new Command(ExecuteFacebookErrorCommand));
        public ICommand FacebookCancelCommand => _facebookCancelCommand ?? (_facebookCancelCommand = new Command(() => IsBusy = false));

        private async void ExecuteBackCommand()
        {
            if (Application.Current.MainPage is MainPage)
            {
                var mainPage = Application.Current.MainPage as BottomBar.XamarinForms.BottomBarPage;
                await mainPage.CurrentPage.Navigation.PopAsync(true);
            }
            else
            {
                Application.Current.MainPage = new PresentationPage(3);
            }
        }

        private async void ExecuteLoginCommand()
        {
            IsBusy = true;
            try
            {
                if (await CrossLogin.Instance.Login(_email, _password))
                {
                    Settings.Email = _email;
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Erro",
                        Message = "Não foi possível fazer login, verifique o email e senha informados",
                        Cancel = "Ok"
                    }, MessageKeys.DisplayAlert);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);

                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Erro",
                    Message = "Não foi possível fazer login, verifique o email e senha informados",
                    Cancel = "Ok"
                }, MessageKeys.DisplayAlert);
            }
            IsBusy = false;
        }

        private void ExecuteFacebookSuccessCommand(object result)
        {
            if (result == null)
            {
                ExecuteFacebookErrorCommand();
            }
            else
            {
                try
                {
                    Settings.Update(JObject.Parse(result.ToString()));
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    ExecuteFacebookErrorCommand();
                }
            }
            Application.Current.MainPage = new MainPage();
        }

        private void ExecuteFacebookErrorCommand()
        {
            MessagingCenter.Send(new MessagingCenterAlert
            {
                Title = "Erro",
                Message = "Não foi possível logar pelo facebook",
                Cancel = "Ok"
            }, MessageKeys.DisplayAlert);
            IsBusy = false;
        }

        private bool ValidateLoginCommand()
        {
            return Validators.EmailValidator(_email) && Validators.PasswordValidator(_password);
        }
    }
}
