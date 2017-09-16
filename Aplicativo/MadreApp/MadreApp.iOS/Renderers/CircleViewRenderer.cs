using MadreApp.Customs;
using MadreApp.iOS.Renderers;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CircleView), typeof(CircleViewRenderer))]
namespace MadreApp.iOS.Renderers
{
    public class CircleViewRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                // define coordinates and size of the circular view
                var frame = new RectangleF(50, 50, 200, 200);
                // Initialize Button
                var circularView = new UIButton(frame);
                // Set background color, border color and width to see the circular view
                circularView.BackgroundColor = UIColor.FromRGB(0, 176, 144);
                // corner radius needs to be one half of the size of the view
                circularView.Layer.CornerRadius = frame.Width / 3;
                // add button to view controller
                SetNativeControl(circularView);
            }
        }
    }
}