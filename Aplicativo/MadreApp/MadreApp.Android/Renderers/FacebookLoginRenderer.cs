using Android.OS;
using MadreApp.Customs;
using MadreApp.Droid.Helpers;
using MadreApp.Droid.Renderers;
using System.Collections.Generic;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginRenderer))]
namespace MadreApp.Droid.Renderers
{
    public class FacebookLoginRenderer : ButtonRenderer
    {
        protected FacebookLoginButton _buttom;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                _buttom = e.NewElement as FacebookLoginButton;
            }

            var activity = Forms.Context as MainActivity;

            _buttom.Clicked += (sender, args) =>
            {
                var facebookCallback = new FacebookCallback<LoginResult>
                {
                    HandleCancel = () => _buttom.OnCancel?.Execute(null),

                    HandleError = error => _buttom.OnError?.Execute(error.Message),

                    HandleSuccess = result => 
                    {
                        var parameters = new Bundle();
                        parameters.PutString("fields", "id,birthday,email,gender,name");
                        var graphCallback = new GraphCallback();
                        graphCallback.RequestCompleted += (s, r) => _buttom.OnSuccess?.Execute(r.Response.RawResponse);
                        new GraphRequest(result.AccessToken, "/" + result.AccessToken.UserId, parameters, HttpMethod.Get, graphCallback).ExecuteAsync();
                    }           
                };

                LoginManager.Instance.LogOut();
                LoginManager.Instance.RegisterCallback(MainActivity.CallbackManager, facebookCallback);
                LoginManager.Instance.LogInWithReadPermissions(activity, new List<string> { "public_profile,email,user_birthday" });
            };
        }
    }
}