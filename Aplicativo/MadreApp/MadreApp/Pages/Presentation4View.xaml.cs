﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Presentation4View : ContentView
    {
        public Presentation4View()
        {
            Func<RelativeLayout, View, double> yMeasure = (p, c) => c.Measure(p.Width, p.Height).Request.Height;
            Func<RelativeLayout, View, double> xCenter = (p, c) => p.Width / 2 - c.Measure(p.Width, p.Height).Request.Width / 2;

            var view = new RelativeLayout();

            var logo = new Image() { Source = "logotipo.png", Scale = 1.0 / 3 };
            var image = new Image() { Source = "map.png", Scale = .6 };
            var title = new Label()
            {
                Text = "Rede",
                FontFamily = "Droid Sans Mono",
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 3,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            var text = new Label()
            {
                Text = "Conheça a maior rede" + Environment.NewLine + "de saúde do Brasil",
                FontFamily = "Droid Sans Mono",
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 2,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            view.Children.Add(logo,
                 yConstraint: Constraint.RelativeToParent(p => p.Height - yMeasure(p, logo)));

            view.Children.Add(title,
                 xConstraint: Constraint.RelativeToParent(p => xCenter(p, title)),
                 yConstraint: Constraint.RelativeToParent(p => p.Height * 5 / 9));

            view.Children.Add(text,
                 xConstraint: Constraint.RelativeToParent(p => xCenter(p, text)),
                 yConstraint: Constraint.RelativeToView(title, (p, s) => s.Y + yMeasure(p, s)));

            view.Children.Add(image,
                 yConstraint: Constraint.RelativeToView(title, (p, s) => -s.Height / 2 + 60));

            Content = view;
        }
    }
}