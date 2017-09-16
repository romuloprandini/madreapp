using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.Customs
{
    public class FacebookLoginButton : Button
    {
        public const string OnSuccessPropertyName = "OnSuccess";
        public const string OnErrorPropertyName = "OnError";
        public const string OnCancelPropertyName = "OnCancel";

        public static readonly BindableProperty OnSuccessProperty = BindableProperty.Create(
            propertyName: OnSuccessPropertyName,
            returnType: typeof(ICommand),
            declaringType: typeof(FacebookLoginButton),
            defaultValue: default(ICommand));

        public static readonly BindableProperty OnErrorProperty = BindableProperty.Create(
            propertyName: OnErrorPropertyName,
            returnType: typeof(ICommand),
            declaringType: typeof(FacebookLoginButton),
            defaultValue: default(ICommand));

        public static readonly BindableProperty OnCancelProperty = BindableProperty.Create(
            propertyName: OnCancelPropertyName,
            returnType: typeof(ICommand),
            declaringType: typeof(FacebookLoginButton),
            defaultValue: default(ICommand));

        public ICommand OnSuccess
        {
            get { return (ICommand)GetValue(OnSuccessProperty); }
            set { SetValue(OnSuccessProperty, value); }
        }

        public ICommand OnError
        {
            get { return (ICommand)GetValue(OnErrorProperty); }
            set { SetValue(OnErrorProperty, value); }
        }

        public ICommand OnCancel
        {
            get { return (ICommand)GetValue(OnCancelProperty); }
            set { SetValue(OnCancelProperty, value); }
        }
    }
}