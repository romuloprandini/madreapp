using MadreApp.Helpers;
using MadreApp.Pages;
using Xamarin.Forms;

namespace MadreApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (true)//string.IsNullOrEmpty(Settings.Name))
            {
                MainPage = new PresentationPage();
            }
            else
            {
                MainPage = new MainPage();
            }
        }
    }
}