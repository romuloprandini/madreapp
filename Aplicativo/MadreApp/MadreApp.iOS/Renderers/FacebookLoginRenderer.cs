using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Facebook.LoginKit;
using Button = Xamarin.Forms.Button;
using MadreApp.Customs;
using MadreApp.iOS.Renderers;
using Facebook.CoreKit;
using Foundation;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginRenderer))]
namespace MadreApp.iOS.Renderers
{
    public class FacebookLoginRenderer : ButtonRenderer
    {
        protected FacebookLoginButton buttom;
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                buttom = e.NewElement as FacebookLoginButton;
            }

            buttom.Clicked += (sender, args) =>
            {
                var loginManager = new LoginManager();
                loginManager.LogOut();
                loginManager.LogInWithReadPermissions("public_profile,email,user_birthday".Split(','), Control.InputViewController, (result, error) =>
                {
                    if (error != null)
                    {
                        buttom.OnError?.Execute(error.Description);
                    }
                    else if (result.IsCancelled)
                    {
                        buttom.OnCancel?.Execute(null);
                    }
                    else
                    {
                        var request = new GraphRequest("/" + result.Token.UserID, new NSDictionary("fields", "id,birthday,email,gender,name"), result.Token.TokenString, null, "GET");
                        request.Start((connection, obj, nsError) => buttom.OnSuccess?.Execute(NSJsonSerialization.Serialize(obj, NSJsonWritingOptions.PrettyPrinted, out nsError).ToString()));
                    }
                });
            };
        }
    }
}