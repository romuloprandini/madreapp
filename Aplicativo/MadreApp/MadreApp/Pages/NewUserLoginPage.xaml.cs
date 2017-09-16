using MadreApp.Customs;
using MadreApp.ViewModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUserLoginPage : ContentPage
    {
        ProfileViewModel _viewModel;
        
        public NewUserLoginPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ProfileViewModel();
        }
    }
}