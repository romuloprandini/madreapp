using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class PresentationTemplateView : ContentView
    {
        public PresentationTemplateView(string mainImage, string title, string message)
        {
            Func<RelativeLayout, View, double> xCenter = (p, c) => p.Width / 2 - c.Measure(p.Width, p.Height).Request.Width / 2;

            var view = new RelativeLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var doctorImage = new Image()
            {
                Source = mainImage,
                Aspect = Aspect.AspectFit
            };

            var logo = new Image()
            {
                Source = "logotipo",
                HeightRequest = 20
            };

            var titleLabel = new Label()
            {
                Text = title,
                TextColor = (Color)Application.Current.Resources["LightTextColor"],
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center,
            };

            var messageLabel = new Label()
            {
                Text = message,
                TextColor = (Color)Application.Current.Resources["LightTextColor"],
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center,
            };

            view.Children.Add(doctorImage,
                  yConstraint: Constraint.RelativeToParent(p => 20),
                  xConstraint: Constraint.RelativeToParent(p => xCenter(p, doctorImage)),
                 heightConstraint: Constraint.RelativeToParent(p => p.Height / 2.7));

            view.Children.Add(titleLabel,
                 xConstraint: Constraint.RelativeToParent(p => xCenter(p, titleLabel)),
                 yConstraint: Constraint.RelativeToView(doctorImage, (p ,s) => {
                     return 10 + s.Y + s.Height;
                 }));

            view.Children.Add(messageLabel,
                 xConstraint: Constraint.RelativeToParent(p => xCenter(p, messageLabel)),
                 yConstraint: Constraint.RelativeToView(titleLabel, (p, s) => {
                     return 10 + s.Y + s.Height;
                 }));

            view.Children.Add(logo,
                 xConstraint: Constraint.RelativeToParent(p => xCenter(p, logo)),
                 yConstraint: Constraint.RelativeToParent(p => p.Height - 50));

            Content = view;
        }
    }
}
