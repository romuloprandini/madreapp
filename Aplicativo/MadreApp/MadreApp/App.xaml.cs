using System;
using MadreApp.Pages;
using Xamarin.Forms;
using MadreApp.Helpers;

namespace MadreApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ConfigureMessageService();

            if (CrossLogin.Instance.IsLogged)
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new PresentationPage();
            }
        }

        private void ConfigureMessageService()
        {
            MessagingCenter.Subscribe<MessagingCenterAlert>(this, MessageKeys.DisplayAlert, (info) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var task = Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);
                    if (task == null) return;

                    await task;
                    info.OnCompleted?.Invoke();
                });
            });

            MessagingCenter.Subscribe<MessagingCenterQuestion>(this, MessageKeys.DisplayQuestion, (info) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var task = Current?.MainPage?.DisplayAlert(info.Title, info.Question, info.Positive, info.Negative);
                    if (task == null) return;

                    var result = await task;
                    info.OnCompleted?.Invoke(result);
                });
            });

            MessagingCenter.Subscribe<MessagingCenterChoice>(this, MessageKeys.DisplayChoice, (info) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var task = Current?.MainPage?.DisplayActionSheet(info.Title, info.Cancel, info.Destruction, info.Items);
                    if (task == null) return;

                    var result = await task;
                    info.OnCompleted?.Invoke(result);
                });
            });
        }
    }
}