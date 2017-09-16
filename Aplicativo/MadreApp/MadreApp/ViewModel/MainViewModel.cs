using MadreApp.Helpers;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand _onCallCommand;
        public ICommand OnCallCommand => _onCallCommand ?? (_onCallCommand = new Command(async () =>
        {
            try
            {
                var ligacao = await HttpRequest.Instance.PostRequest<object>("/ligacao", new { nome = Settings.Name, telefone = Settings.Phone });
            } catch(Exception ex)
            {
                Application.Current.MainPage?.DisplayAlert("Erro", "Não foi possível efetuar a chamada", "Ok");
                Debug.WriteLine(ex.Message);
            }
        }));
    }
}
