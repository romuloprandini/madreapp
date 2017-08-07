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
    public partial class UserPage : ContentPage
    {
        UserViewModel _viewModel;
        public UserPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new UserViewModel();
        }
    }
}