using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using MadreApp.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;

using MadreApp.Customs;
using UIKit;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(TouchableImage), typeof(TouchableImageRenderer))]
namespace MadreApp.iOS.Renderers
{
    public class TouchableImageRenderer : ViewRenderer<TouchableImage, UIView>
    {
        TouchableImage image;

        protected override void OnElementChanged(ElementChangedEventArgs<TouchableImage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null) return;
            
            image = e.NewElement;

            var buttom = UIButton.FromType(UIButtonType.Custom);

            var imagePhone = UIImage.FromBundle(image.Source);
            buttom.SetImage(imagePhone, UIControlState.Normal);

            buttom.UserInteractionEnabled = true;
            buttom.TouchDown += Buttom_TouchDown;
            buttom.TouchUpInside += Buttom_TouchUpInside;
            buttom.TouchUpOutside += Buttom_TouchUpInside;

            //UILongPressGestureRecognizer longp = new UILongPressGestureRecognizer(LongPress);
            //buttom.AddGestureRecognizer(longp);

            SetNativeControl(buttom);
        }

        private void Buttom_TouchDown(object sender, EventArgs e)
        {
            if (image.OnTouchStartCommand == null) return;
            if (image.OnTouchStartCommand.CanExecute(null)) image.OnTouchStartCommand.Execute(null);
        }

        private void Buttom_TouchUpInside(object sender, EventArgs e)
        {
            if (image.OnTouchEndCommand == null) return;
            if (image.OnTouchEndCommand.CanExecute(null)) image.OnTouchEndCommand.Execute(null);
        }

        private void LongPress()
        {
            if (image?.OnLongClick == null) return;
            if (image.OnLongClick.CanExecute(null)) image.OnLongClick?.Execute(null);
        }
    }
}