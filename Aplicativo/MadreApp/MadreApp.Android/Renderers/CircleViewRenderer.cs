using Android.App;
using MadreApp.Customs;
using MadreApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CircleView), typeof(CircleViewRenderer))]
namespace MadreApp.Droid.Renderers
{
    public class CircleViewRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Context != null)
            {
                var view = (Context as Activity).LayoutInflater.Inflate(Resource.Layout.PulseCircle, null, false);
                SetNativeControl(view);
            }
        }
    }
}