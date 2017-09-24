using MadreApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        LoginViewModel _viewModel;
        public LoginPage(bool isNewUser)
		{
			InitializeComponent();
            BindingContext = _viewModel = new LoginViewModel(isNewUser);
        }
	}
}