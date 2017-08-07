using MadreApp.Customs;
using MadreApp.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CircleView), typeof(CircleViewRenderer))]
namespace MadreApp.iOS.Renderers
{
    public class CircleViewRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                // define coordinates and size of the circular view
                float x = 50;
                float y = 50;
                float width = 200;
                float height = width;
                // corner radius needs to be one half of the size of the view
                float cornerRadius = width / 3;
                RectangleF frame = new RectangleF(x, y, width, height);
                // initialize button
                UIButton circularView = new UIButton(frame);
                // set corner radius
                circularView.Layer.CornerRadius = cornerRadius;
                // set background color, border color and width to see the circular view
                circularView.BackgroundColor = UIColor.FromRGB(0, 176, 144);
                circularView.Layer.CornerRadius = cornerRadius;
                // add button to view controller
                SetNativeControl(circularView);
            }
        }
    }
}
