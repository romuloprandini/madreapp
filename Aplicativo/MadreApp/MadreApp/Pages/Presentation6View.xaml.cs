using MadreApp.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Presentation6View : ContentView
    {
        PresentationViewModel _viewModel;
        public Presentation6View()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PresentationViewModel();
        }
    }
}