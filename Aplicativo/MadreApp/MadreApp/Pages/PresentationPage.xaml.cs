using MadreApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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