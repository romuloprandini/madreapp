using MadreApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PhoneRegisterPage : ContentPage
	{
        PhoneRegisterViewModel _viewModel;
        public PhoneRegisterPage()
		{
			InitializeComponent();
            BindingContext = _viewModel = new PhoneRegisterViewModel();
        }
	}
}