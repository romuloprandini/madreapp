using MadreApp.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PresentationLoginView : ContentView
    {
        public PresentationLoginView()
        {
            InitializeComponent();

            BindingContext = new PresentationLoginViewModel();
        }
    }
}