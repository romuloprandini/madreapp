using MadreApp.Helpers;
using MadreApp.Pages;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (_nome == value) return;
                SetProperty(ref _nome, value);
            }
        }

        private string _telefone;
        public string Telefone
        {
            get { return _telefone; }
            set
            {
                if (_telefone == value) return;
                SetProperty(ref _telefone, value);
            }
        }

        private bool Validate()
        {
            if(string.IsNullOrEmpty(_nome) && string.IsNullOrEmpty(_telefone))
            {
                Application.Current.MainPage?.DisplayAlert("Erro", "Informe o nome e o telefone", "Ok");
                return false;
            }
            if (string.IsNullOrEmpty(_nome))
            {
                Application.Current.MainPage?.DisplayAlert("Erro", "Informe o nome", "Ok");
                return false;
            }
            if (string.IsNullOrEmpty(_telefone))
            {
                Application.Current.MainPage?.DisplayAlert("Erro", "Informe o telefone", "Ok");
                return false;
            }

            return true;
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new Command(() => {
            if (!Validate()) return;

            Settings.Nome = _nome;
            Settings.Telefone = _telefone;

            Application.Current.MainPage = new MainPage();
        }));

        private ICommand _facebookLoginCommand;
        public ICommand FacebookLoginCommand => _facebookLoginCommand ?? (_facebookLoginCommand = new Command(() =>
        {
            IsBusy = true;
        }));

        private ICommand _onSucessFacebook;
        public ICommand OnSucessFacebook => _onSucessFacebook ?? (_onSucessFacebook = new Command(result =>
        {
            FacebookResult facebookResult = (FacebookResult)result;

            //TODO - Buscar na api as informações do usuário ou buscar no SDK do facebook

            //Settings.Nome = ;
            //Settings.Telefone = ;

            IsBusy = false;
        }));

        private ICommand _onErrorFacebook;
        public ICommand OnErrorFacebook => _onErrorFacebook ?? (_onErrorFacebook = new Command(() =>
        {
            Application.Current.MainPage?.DisplayAlert("Erro", "Não foi possível logar pelo facebook", "Ok");
            IsBusy = false;
        }));

        private ICommand _onCancelFacebook;
        public ICommand OnCancelFacebook => _onCancelFacebook ?? (_onCancelFacebook = new Command(() =>
        {
            IsBusy = false;
        }));
    }
}
