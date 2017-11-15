using MadreApp.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ValidatePhonePage : ContentPage
	{
		public ValidatePhonePage (string name)
		{
			InitializeComponent ();

            BindingContext = new ValidatePhoneViewModel(name);

        }
	}
}