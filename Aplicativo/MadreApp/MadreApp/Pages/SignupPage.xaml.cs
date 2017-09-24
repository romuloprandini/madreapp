using MadreApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupPage : ContentPage
	{
        SignupViewModel _viewModel;
        public SignupPage()
		{
			InitializeComponent();
            BindingContext = _viewModel = new SignupViewModel();
        }
	}
}