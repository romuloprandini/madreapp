using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MadreApp.Customs
{
    public class MadreAppLogoView : ContentView
    {
        public const string SourcePropertyName = "Source";
        public const string IsBusyPropertyName = "IsBusy";
        public const string BackCommandPropertyName = "BackCommand";

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            propertyName: SourcePropertyName,
            returnType: typeof(View),
            declaringType: typeof(MadreAppLogoView),
            defaultValue: null,
            propertyChanged: OnSourcePropertyChanged);

        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
            propertyName: IsBusyPropertyName,
            returnType: typeof(bool),
            declaringType: typeof(MadreAppLogoView),
            defaultValue: false,
            propertyChanged: OnIsBusyPropertyChanged);

        public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(
            propertyName: BackCommandPropertyName,
            returnType: typeof(Command),
            declaringType: typeof(MadreAppLogoView),
            defaultValue: null,
            propertyChanged: OnBackCommandPropertyChanged);


        public View Source
        {
            get { return (View)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public Command BackCommand
        {
            get { return (Command)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }

        private static void OnSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var madreAppLogoView = bindable as MadreAppLogoView;
            if (madreAppLogoView == null || madreAppLogoView._mainView == null) return;

            var view = newValue as View;
            madreAppLogoView._mainView.Content = view;
        }

        private static void OnIsBusyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var madreAppLogoView = bindable as MadreAppLogoView;
            if (madreAppLogoView == null) return;

            madreAppLogoView._loadingActivityIndicator.IsVisible = (bool)newValue;
            madreAppLogoView._loadingActivityIndicator.IsRunning = (bool)newValue;
            madreAppLogoView._loadingBox.IsVisible = (bool)newValue;
        }

        private static void OnBackCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var madreAppLogoView = bindable as MadreAppLogoView;
            if (madreAppLogoView == null) return;

            if (newValue == null)
            {
                madreAppLogoView._backImage.IsVisible = false;
                madreAppLogoView._backImage.GestureRecognizers.Clear();
                return;
            }
            madreAppLogoView._backImage.IsVisible = true;
            madreAppLogoView._backImage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = (Command)newValue
            });
        }

        private ContentView _mainView;
        private ActivityIndicator _loadingActivityIndicator;
        private BoxView _loadingBox;
        private Image _backImage;

        public MadreAppLogoView()
        {
            var mainLayout = new StackLayout();

            _backImage = new Image
            {
                Source = "arrow_back",
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Start,
                IsVisible = (BackCommand != null),
                HeightRequest = 30,
                Margin=new Thickness(10,10,0,0)
            };
            mainLayout.Children.Add(_backImage);

            var logo = new Image
            {
                Source = "logotipo_azul",
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0),
                HeightRequest=80
            };
            mainLayout.Children.Add(logo);

            _mainView = new ContentView
            {
                Content = Source,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0,0,0,10)
            };
            mainLayout.Children.Add(_mainView);

            _loadingBox = new BoxView
            {
                IsVisible = false,
                BackgroundColor = Color.Black,
                Opacity = .5
            };

            _loadingActivityIndicator = new ActivityIndicator
            {
                IsVisible = false,
                IsRunning = false,
                HeightRequest = 80,
                WidthRequest = 80,
            };

            Content = new AbsoluteLayout
            {
                Children = {
                    { mainLayout, new Rectangle(0,0,1,1), AbsoluteLayoutFlags.All },
                    { _loadingBox, new Rectangle(0,0,1,1), AbsoluteLayoutFlags.All },
                    { _loadingActivityIndicator, new Rectangle(.5,.5,80,80), AbsoluteLayoutFlags.PositionProportional },
                }
            };
        }

        public MadreAppLogoView(string teste)
        {
            var mainLayout = new RelativeLayout();

            _backImage = new Image
            {
                Source = "arrow_back",
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = (BackCommand != null),
                HeightRequest = 0
            };
            mainLayout.Children.Add(_backImage,
            xConstraint: Constraint.RelativeToParent((parent) => 10),
            yConstraint: Constraint.RelativeToParent((parent) => 10));

            var logo = new Image
            {
                Source = "logotipo_azul",
                Aspect = Aspect.AspectFit,
                Margin = new Thickness(10, 0),
                HorizontalOptions = LayoutOptions.Center
            };
            mainLayout.Children.Add(logo,
            yConstraint: Constraint.RelativeToView(_backImage, (parent, sb) => sb.Height),
            widthConstraint: Constraint.RelativeToParent((parent) => parent.Width),
            heightConstraint: Constraint.RelativeToParent((parent) => parent.Height * .2));

            _mainView = new ContentView
            {
                Content = Source,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            mainLayout.Children.Add(_mainView,
                yConstraint: Constraint.RelativeToView(logo, (parent, sb) => sb.Y + sb.Height),
                widthConstraint: Constraint.RelativeToParent((parent) => parent.Width),
                heightConstraint: Constraint.RelativeToView(logo, (parent, sb) => {
                    return _mainView.Height + parent.Height - sb.Y - sb.Height;
                }));

            _loadingBox = new BoxView
            {
                IsVisible = false,
                BackgroundColor = Color.Black,
                Opacity = .5
            };

            _loadingActivityIndicator = new ActivityIndicator
            {
                IsVisible = false,
                IsRunning = false,
                HeightRequest = 80,
                WidthRequest = 80,
            };

            Content = new AbsoluteLayout
            {
                Children = {
                    { mainLayout, new Rectangle(0,0,1,1), AbsoluteLayoutFlags.All },
                    { _loadingBox, new Rectangle(0,0,1,1), AbsoluteLayoutFlags.All },
                    { _loadingActivityIndicator, new Rectangle(.5,.5,80,80), AbsoluteLayoutFlags.PositionProportional },
                }
            };
        }
    }
}