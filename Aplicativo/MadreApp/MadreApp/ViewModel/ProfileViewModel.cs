using MadreApp.Helpers;
using MadreApp.Models;
using MadreApp.Pages;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        private ICommand _editCommand;
        private ICommand _logoutCommand;

        public ProfileViewModel()
        {
        }

        public User User => CrossLogin.Instance.LoggedUser;

        public ICommand EditCommand => _editCommand ?? (_editCommand = new Command(ExecuteEditCommand));
        public ICommand LogoutCommand => _logoutCommand ?? (_editCommand = new Command(ExecuteLogoutCommand));

        private async void ExecuteEditCommand()
        {
            var mainPage = Application.Current.MainPage as MainPage;
            await mainPage.CurrentPage.Navigation.PushAsync(new FullRegisterPage());
        }
        private void ExecuteLogoutCommand()
        {
            MessagingCenter.Send(new MessagingCenterQuestion
            {
                Title = "Sair",
                Question = "Deseja desconectar sua conta?",
                Positive = "Ok",
                Negative = "Cancelar",
                OnCompleted = (resposta) =>
                {
                    if (resposta)
                    {
                        CrossLogin.Instance.Logout();
                        Application.Current.MainPage = new PresentationPage();
                    }
                }
            }, MessageKeys.DisplayQuestion);
        }
    }
}