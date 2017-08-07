using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.Customs
{
    public class TouchableImage : View
    {
        public const string SourcePropertyName = "Source";

        public const string OnTouchStartCommandPropertyName = "OnTouchStartCommand";
        public const string OnTouchEndCommandPropertyName = "OnTouchEndCommand";
        public const string OnLongClickPropertyName = "OnLongClick";

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            propertyName: SourcePropertyName,
            returnType: typeof(string),
            declaringType: typeof(TouchableImage),
            defaultValue: null);

        public static readonly BindableProperty OnTouchStartCommandProperty = BindableProperty.Create(
            propertyName: OnTouchStartCommandPropertyName,
            returnType: typeof(ICommand),
            declaringType: typeof(TouchableImage),
            defaultValue: default(ICommand));

        public static readonly BindableProperty OnTouchEndCommandProperty = BindableProperty.Create(
            propertyName: OnTouchEndCommandPropertyName,
            returnType: typeof(ICommand),
            declaringType: typeof(TouchableImage),
            defaultValue: default(ICommand));

        public static readonly BindableProperty OnLongClickProperty = BindableProperty.Create(
            propertyName: OnLongClickPropertyName,
            returnType: typeof(ICommand),
            declaringType: typeof(TouchableImage),
            defaultValue: default(ICommand));

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public ICommand OnTouchStartCommand
        {
            get { return (ICommand)GetValue(OnTouchStartCommandProperty); }
            set { SetValue(OnTouchStartCommandProperty, value); }
        }

        public ICommand OnTouchEndCommand
        {
            get { return (ICommand)GetValue(OnTouchEndCommandProperty); }
            set { SetValue(OnTouchEndCommandProperty, value); }
        }

        public ICommand OnLongClick
        {
            get { return (ICommand)GetValue(OnLongClickProperty); }
            set { SetValue(OnLongClickProperty, value); }
        }
    }
}
