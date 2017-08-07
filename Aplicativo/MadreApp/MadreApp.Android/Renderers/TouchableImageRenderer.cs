using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MadreApp.Droid.Renderers;
using FFImageLoading.Forms.Droid;
using Xamarin.Forms.Platform.Android;
using MadreApp.Customs;
using Android.Widget;
using static Android.Views.View;
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Util;

[assembly: Xamarin.Forms.ExportRenderer(typeof(TouchableImage), typeof(TouchableImageRenderer))]
namespace MadreApp.Droid.Renderers
{
    public class TouchableImageRenderer : ViewRenderer<TouchableImage, View>, IOnLongClickListener
    {
        TouchableImage touchableImage;
        
        protected override void OnElementChanged(ElementChangedEventArgs<TouchableImage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null) return;

            touchableImage = e.NewElement;

            if (Control != null) return;

            var image = new ImageView(Context);

            int resImage = (int)typeof(Resource.Drawable).GetField(touchableImage.Source).GetVa‌​lue(null);
            image.SetImageResource(resImage);
            image.SetScaleType(ImageView.ScaleType.FitCenter);
            image.SetAdjustViewBounds(true);
            image.Touch += Control_Touch;
            image.LongClickable = true;
            image.SetOnLongClickListener(this);

            SetNativeControl(image);
        }

        public bool OnLongClick(View v)
        {
            if(touchableImage?.OnLongClick != null && touchableImage.OnLongClick.CanExecute(null)) touchableImage.OnLongClick.Execute(null);
            return true;
        }

        private void Control_Touch(object sender, TouchEventArgs e)
        {
            e.Handled = false;

            if (touchableImage == null) return;

            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    if (touchableImage.OnTouchStartCommand != null && touchableImage.OnTouchStartCommand.CanExecute(null)) touchableImage.OnTouchStartCommand.Execute(null);
                    break;
                case MotionEventActions.Up:
                    if (touchableImage.OnTouchEndCommand != null && touchableImage.OnTouchEndCommand.CanExecute(null)) touchableImage.OnTouchEndCommand.Execute(null);
                    break;
            }
        }
    }
}