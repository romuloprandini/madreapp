using MadreApp.Behaviors;
using MadreApp.Helpers;
using MadreApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class FullRegisterViewModel : BaseNavigationViewModel
    {
        private string _phone;
        private string _name;
        private string _email;
        private string _password;
        private string _currentPassword;
        private ICommand _backCommand;
        private ICommand _registerCommand;
        private ICommand _updateCommand;

        public FullRegisterViewModel()
        {
            _name = CrossLogin.Instance.LoggedUser.Name;
            _phone = CrossLogin.Instance.LoggedUser.Phone;
            _email = CrossLogin.Instance.LoggedUser.Email;
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

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;
                SetProperty(ref _password, value);
            }
        }

        public string CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                if (_currentPassword == value) return;
                SetProperty(ref _currentPassword, value);
            }
        }

        public bool IsEdit => !string.IsNullOrEmpty(CrossLogin.Instance.LoggedUser.Email);

        public ICommand BackCommand => _backCommand ?? (_backCommand = new Command(ExecuteBackCommand));
        public ICommand RegisterCommand => _registerCommand ?? (_registerCommand = new Command(ExecuteRegisterCommand));
        public ICommand UpdateCommand => _updateCommand ?? (_updateCommand = new Command(ExecuteUpdateCommand));

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

        public async void ExecuteRegisterCommand()
        {
            IsBusy = true;
            if(await CrossLogin.Instance.Register(_email, _password, _name, ""))
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Sucesso",
                    Message = "Seu cadastro foi atualizado",
                    Cancel = "Ok"
                }, MessageKeys.DisplayAlert);

                Application.Current.MainPage = new MainPage();
            }
            else
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Erro",
                    Message = "Não foi possível completar seu cadastro, verifique os dados informados",
                    Cancel = "Ok"
                }, MessageKeys.DisplayAlert);
            }
            IsBusy = false;
        }

        public async void ExecuteUpdateCommand()
        {
            IsBusy = true;
            bool updatedPassword = true;
            bool updatedName = true;
            if (Validators.PasswordValidator(_password))
            {
                if(!await CrossLogin.Instance.UpdatePassword(_email, _currentPassword, _password))
                {
                    updatedPassword = false;
                }
            }
            if(!string.IsNullOrWhiteSpace(_name))
            {
                if (!await CrossLogin.Instance.UpdateProfile(_name, null))
                {
                    updatedName = false;
                }
            }

            if (updatedPassword && updatedName)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Sucesso",
                    Message = "Seu cadastro foi atualizado",
                    Cancel = "Ok"
                }, MessageKeys.DisplayAlert);

                Application.Current.MainPage = new MainPage();
            }
            else if(!updatedPassword)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Sucesso",
                    Message = "Seu cadastro foi atualizado parcialmente, sua senha não pode ser alterada. Verifique se ela tem pelo menos 8 caracteres",
                    Cancel = "Ok"
                }, MessageKeys.DisplayAlert);

                Application.Current.MainPage = new MainPage();
            }
            else if(!updatedName)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Sucesso",
                    Message = "Seu cadastro foi atualizado parcialmente, seu nome não pode ser atualizado. Verifique se o nome não está vazio",
                    Cancel = "Ok"
                }, MessageKeys.DisplayAlert);

                Application.Current.MainPage = new MainPage();
            }
            else
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Erro",
                    Message = "Não foi possível completar seu cadastro, verifique os dados informados",
                    Cancel = "Ok"
                }, MessageKeys.DisplayAlert);
            }
            IsBusy = false;
        }
    }
}
