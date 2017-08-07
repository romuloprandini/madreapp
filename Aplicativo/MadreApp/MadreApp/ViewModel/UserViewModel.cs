using MadreApp.Helpers;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreApp.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel()
        {
            _nome = Settings.Nome;
            _telefone = Settings.Telefone;
            _email = "Não informado";
            _nascimento = "Não informado";
        }

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
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value) return;
                SetProperty(ref _email, value);
            }
        }

        private string _nascimento;
        public string Nascimento
        {
            get { return _nascimento; }
            set
            {
                if (_nascimento == value) return;
                SetProperty(ref _nascimento, value);
            }
        }

    }
}
