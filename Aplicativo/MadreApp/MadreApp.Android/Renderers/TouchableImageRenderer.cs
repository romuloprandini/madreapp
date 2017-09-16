using Android.Views;
using Android.Widget;
using MadreApp.Customs;
using MadreApp.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using static Android.Views.View;

[assembly: Xamarin.Forms.ExportRenderer(typeof(TouchableImage), typeof(TouchableImageRenderer))]
namespace MadreApp.Droid.Renderers
{
    public class TouchableImageRenderer : ViewRenderer<TouchableImage, View>, IOnLongClickListener
    {
        private TouchableImage _touchableImage;
        
        protected override void OnElementChanged(ElementChangedEventArgs<TouchableImage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null) return;

            _touchableImage = e.NewElement;

            if (Control != null) return;

            var image = new ImageView(Context);

            int resImage = (int)typeof(Resource.Drawable).GetField(_touchableImage.Source).GetVa‌​lue(null);
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
            if(_touchableImage?.OnLongClick != null && _touchableImage.OnLongClick.CanExecute(null)) _touchableImage.OnLongClick.Execute(null);
            return true;
        }

        private void Control_Touch(object sender, TouchEventArgs e)
        {
            e.Handled = false;

            if (_touchableImage == null) return;

            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    if (_touchableImage.OnTouchStartCommand != null && _touchableImage.OnTouchStartCommand.CanExecute(null)) _touchableImage.OnTouchStartCommand.Execute(null);
                    break;
                case MotionEventActions.Up:
                    if (_touchableImage.OnTouchEndCommand != null && _touchableImage.OnTouchEndCommand.CanExecute(null)) _touchableImage.OnTouchEndCommand.Execute(null);
                    break;
            }
        }
    }
}