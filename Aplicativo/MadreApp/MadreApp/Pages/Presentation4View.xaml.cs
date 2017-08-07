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
    public partial class Presentation4View : ContentView
    {
        PresentationViewModel _viewModel;
        public Presentation4View()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PresentationViewModel();
        }
    }
}