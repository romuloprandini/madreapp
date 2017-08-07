using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Facebook.LoginKit;
using Button = Xamarin.Forms.Button;
using MadreApp.Customs;
using MadreApp.iOS.Renderers;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginRenderer))]
namespace MadreApp.iOS.Renderers
{
    public class FacebookLoginRenderer : ButtonRenderer
    {
        /// <summary>
        /// The read permissions
        /// </summary>
        readonly string[] _readPermissions =
        {
            "public_profile","email"
        };

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
                loginManager.LogInWithReadPermissions(_readPermissions, Control.InputViewController, (result, error) =>
                {
                    if (error != null)
                    {
                        buttom.OnError?.Execute(null);
                        Console.WriteLine("HelloFacebook: Error: {0}", error.Description);
                    }
                    else if (result.IsCancelled)
                    {
                        buttom.OnCancel?.Execute(null);
                        Console.WriteLine("HelloFacebook: Canceled");
                    }
                    else
                    {
                        buttom.OnSuccess?.Execute(new FacebookResult { Token = result.Token.TokenString, ExpireTime = result.Token.ExpirationDate.ToDateTime().Ticks });
                    }
                });
            };
        }
    }
}
