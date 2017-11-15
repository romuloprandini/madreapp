using MadreApp.Customs;
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
	public partial class CallPage : ContentPage
	{
        CallViewModel _viewModel;
		public CallPage ()
		{
			InitializeComponent();

            BindingContext = _viewModel = new CallViewModel();
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                Animate("wave_1", image);
                await Task.Delay(1250);
                Animate("wave_2", image2);
            });
        }

        private void Animate(string name, View image)
        {
            new Animation(v => { image.Scale = v; image.Opacity = 1 - (v / 2.5); }, 1, 2.5, Easing.Linear)
                .Commit(this, name, 16, 2500, Easing.Linear, (v, c) => { image.Scale = 1; image.Opacity = .6; }, () => true );
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
        }
    }
}