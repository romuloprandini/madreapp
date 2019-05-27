using CarouselView.FormsPlugin.iOS;
using Facebook.CoreKit;
using Firebase.Auth;
using Foundation;
using MadreApp.Helpers;
using System;
using UIKit;

namespace MadreApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        /// <summary>
        /// This method is invoked when the application has loaded and is ready to run. 
        /// In this method you should instantiate the window, 
        /// load the UI into it and then make the window visible.
        /// You have 17 seconds to return from this method, or iOS will terminate your application.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Profile.EnableUpdatesOnAccessTokenChange(true);
            Facebook.CoreKit.Settings.AppID = "XXX";
            Facebook.CoreKit.Settings.DisplayName = "MadreApp";

            Xamarin.Forms.Forms.Init();
            CarouselViewRenderer.Init();
            Firebase.Core.App.Configure();

            CrossLogin.Initialize();



            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            // Pass device token to auth
            Auth.DefaultInstance.SetApnsToken(deviceToken, AuthApnsTokenType.Sandbox); // Production if you are ready to release your app, otherwise, use Sandbox.
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // Pass notification to auth and check if they can handle it.
            if (Auth.DefaultInstance.CanHandleNotification(userInfo))
            {
                completionHandler(UIBackgroundFetchResult.NoData);
                return;
            }

            // This notification is not auth related, developer should handle it.
        }
    }
}