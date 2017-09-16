using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Presentation1View : ContentView
    {
        public Presentation1View()
        {
            var view = new RelativeLayout();

            var logo = new Image() { Source = "logotipo.png", Scale = .9 };

            view.Children.Add(logo, yConstraint: Constraint.RelativeToParent(p => p.Height / 2 - logo.Measure(p.Width, p.Height).Request.Height / 2));

            Content = view;
        }
    }
}