using MadreApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class CompleteRegisterViewModel : BaseNavigationViewModel
    {
        private ICommand _registerCommand;
        private ICommand _loginCommand;

        public ICommand RegisterCommand => _registerCommand ?? (_registerCommand = new Command(ExecuteRegister));
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new Command(ExecuteLogin));

        public async void ExecuteRegister()
        {
            IsBusy = true;
            var mainPage = Application.Current.MainPage as BottomBar.XamarinForms.BottomBarPage;
            await mainPage.CurrentPage.Navigation.PushAsync(new FullRegisterPage());
            IsBusy = false;
        }

        public async void ExecuteLogin()
        {
            var mainPage = Application.Current.MainPage as BottomBar.XamarinForms.BottomBarPage;
            IsBusy = true;
            await mainPage.CurrentPage.Navigation.PushAsync(new LoginPage());
            IsBusy = false;
        }
    }
}
