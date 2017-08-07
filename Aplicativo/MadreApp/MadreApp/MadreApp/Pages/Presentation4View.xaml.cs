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
        public Presentation4View()
        {
            InitializeComponent();
        }

        private void AvancarButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}