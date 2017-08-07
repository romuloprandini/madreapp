using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MadreApp.Customs;
using MadreApp.Droid.Renderers;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CirularProgressBar), typeof(CircularProgressBarRenderer))]
namespace MadreApp.Droid.Renderers
{
    public class CircularProgressBarRenderer : ProgressBarRenderer
    {
        CirularProgressBar progressBar;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ProgressBar> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null) return;

            progressBar = (CirularProgressBar)e.NewElement;

            Android.Widget.ProgressBar view = (Android.Widget.ProgressBar)(Context as Activity).LayoutInflater.Inflate(Resource.Layout.CircularProgressBar, null, false);
            view.Progress = 0;
            SetNativeControl(view);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "Progress")
            {
                if (progressBar.Progress > 0)
                {
                    Control.Progress = (int)(progressBar.Progress * 100);
                }
                else
                {
                    Control.Progress = 0;
                }
            }
        }
    }
}