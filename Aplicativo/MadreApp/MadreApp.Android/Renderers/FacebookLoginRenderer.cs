using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Facebook.Login;
using Xamarin.Forms.Platform.Android;
using MadreApp.Customs;
using MadreApp.Droid.Renderers;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginRenderer))]
namespace MadreApp.Droid.Renderers
{
    public class FacebookLoginRenderer : ButtonRenderer
    {
        public FacebookLoginRenderer()
        {
        }
        /// <summary>
        /// The read permissions
        /// </summary>
        readonly List<string> _readPermissions = new List<string>
        {
            "public_profile,email"
        };

        protected FacebookLoginButton buttom;
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                buttom = e.NewElement as FacebookLoginButton;
            }

            var activity = Xamarin.Forms.Forms.Context as MainActivity;

            buttom.Clicked += (sender, args) =>
            {
                var facebookCallback = new FacebookCallback<LoginResult>
                {

                    HandleSuccess = shareResult => {
                        buttom.OnSuccess?.Execute(new FacebookResult { Token = shareResult.AccessToken.Token, ExpireTime = shareResult.AccessToken.Expires.Time });
                    },
                    HandleCancel = () => {
                        buttom.OnCancel?.Execute(null);
                        Console.WriteLine("HelloFacebook: Canceled");
                    },
                    HandleError = shareError => {
                        buttom.OnError?.Execute(null);
                        Console.WriteLine("HelloFacebook: Error: {0}", shareError);
                    }
                };

                LoginManager.Instance.LogOut();
                LoginManager.Instance.RegisterCallback(MainActivity.CallbackManager, facebookCallback);
                LoginManager.Instance.LogInWithReadPermissions(activity, _readPermissions);
            };
        }
    }
}