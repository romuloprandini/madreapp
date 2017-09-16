using MadreApp.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Presentation5View : ContentView
    {
        PresentationViewModel _viewModel;
        public Presentation5View()
        {
            Func<RelativeLayout, View, double> xMeasure = (p, c) => c.Measure(p.Width, p.Height).Request.Width;
            Func<RelativeLayout, View, double> yMeasure = (p, c) => c.Measure(p.Width, p.Height).Request.Height;
            Func<RelativeLayout, View, double> xCenter = (p, c) => p.Width / 2 - c.Measure(p.Width, p.Height).Request.Width / 2;

            var view = new RelativeLayout();

            var logo = new Image() { Source = "logotipo.png", Scale = .9 };
            var icons = new Image() { Source = "iconlist.png", Scale = .5 };
            var title = new Label()
            {
                Text = "Maior rede credenciada do Brasil",
                FontFamily = "Droid Sans Mono",
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 1.7,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            var line1 = new Label()
            {
                Text = 
                    "+500 especialidades médicas" + Environment.NewLine + 
                    "+20 mil tipos de serviços médicos disponiveis" + Environment.NewLine +
                    "+4 mil unidades de atendimento",
                FontFamily = "Droid Sans Mono",
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start,
            };

            view.Children.Add(logo, yConstraint: Constraint.Constant(20));
                        
            view.Children.Add(title,
                 xConstraint: Constraint.RelativeToParent(p => xCenter(p, title)),
                 yConstraint: Constraint.RelativeToView(logo, (p, s) => s.Y + yMeasure(p, s) + yMeasure(p, title)));

            view.Children.Add(line1,
                 xConstraint: Constraint.RelativeToParent(p => xCenter(p, line1)),
                 yConstraint: Constraint.RelativeToView(title, (p, s) => s.Y + yMeasure(p, s)));

            view.Children.Add(icons, yConstraint: Constraint.RelativeToView(title, (p, s) => s.Y));// + yMeasure(p, s)));

            Content = view;

            //InitializeComponent();

            //BindingContext = _viewModel = new PresentationViewModel();
        }
    }
}