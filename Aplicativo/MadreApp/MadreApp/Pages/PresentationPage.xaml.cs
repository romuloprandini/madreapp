using MadreApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PresentationPage : ContentPage
    {
        PresentationViewModel _viewModel;
        public PresentationPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PresentationViewModel();
        }
    }
}