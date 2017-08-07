using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using MadreApp.Droid.Renderers;
using MadreApp.Customs;

[assembly: ExportRenderer(typeof(CircleView), typeof(CircleViewRenderer))]
namespace MadreApp.Droid.Renderers
{
    public class CircleViewRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                var view = (Context as Activity).LayoutInflater.Inflate(Resource.Layout.PulseCircle, null, false);
                SetNativeControl(view);
            }
        }
    }
}