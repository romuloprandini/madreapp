using MadreApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MadreCardLoginPage : ContentPage
	{
        ProfileViewModel _viewModel;
        public MadreCardLoginPage()
		{
			InitializeComponent();
            BindingContext = _viewModel = new ProfileViewModel();
        }
	}
}