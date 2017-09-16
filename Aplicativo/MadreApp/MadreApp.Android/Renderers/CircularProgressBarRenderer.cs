using Android.App;
using MadreApp.Customs;
using MadreApp.Droid.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CirularProgressBar), typeof(CircularProgressBarRenderer))]
namespace MadreApp.Droid.Renderers
{
    public class CircularProgressBarRenderer : ProgressBarRenderer
    {
        private CirularProgressBar _progressBar;

        protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null) return;

            _progressBar = (CirularProgressBar)e.NewElement;

            var view = (Android.Widget.ProgressBar)(Context as Activity).LayoutInflater.Inflate(Resource.Layout.CircularProgressBar, null, false);
            view.Progress = 0;
            SetNativeControl(view);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "Progress")
            {
                Control.Progress = _progressBar.Progress > 0 ? (int)(_progressBar.Progress * 100) : 0;
            }
        }
    }
}