using BottomBar.XamarinForms;
using MadreApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : BottomBarPage
    {
        public MainPage()
        {
            InitializeComponent();


            Children.Add(new CallPage
            {
                Title = "Principal",
                Icon = (FileImageSource)ImageSource.FromFile("ic_home")
            });
            if (CrossLogin.Instance.IsLogged && !string.IsNullOrEmpty(CrossLogin.Instance.LoggedUser.Email))
            {
                Children.Add(new NavigationPage(new ProfilePage())
                {
                    Title = "Perfil",
                    Icon = (FileImageSource)ImageSource.FromFile("ic_friends")
                });
            }
            else
            {
                Children.Add(new NavigationPage(new CompleteRegisterPage())
                {
                    Title = "Perfil",
                    Icon = (FileImageSource)ImageSource.FromFile("ic_friends")
                });
            }
        }
    }
}
