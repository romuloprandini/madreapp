﻿using MadreApp.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class CallViewModel : BaseViewModel
    {

        private bool isPressed = false;

        private bool _isProgressVisible;
        public bool IsProgressVisible
        {
            get { return _isProgressVisible; }
            set
            {
                if (_isProgressVisible == value) return;
                SetProperty(ref _isProgressVisible, value);
            }
        }

        private double _progress;
        public double Progress
        {
            get { return _progress; }
            set
            {
                if (_progress == value) return;
                SetProperty(ref _progress, value);
            }
        }

        private ICommand _onTouchStartCommand;
        public ICommand OnTouchStartCommand => _onTouchStartCommand ?? (_onTouchStartCommand = new Command(() =>
        {
            isPressed = true;
            IsProgressVisible = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(150), () =>
            {
                Progress = _progress + .05;
                if(Progress >= 1)
                {
                    IsBusy = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var user = CrossLogin.Instance.LoggedUser;
                        var result = await HttpRequest.Instance.PostRequest<object>("/ligacao", user);
                        MessagingCenter.Send(new MessagingCenterAlert
                        {
                            Title = "Sucesso",
                            Message = "Aguarde que em alguns minutos ligaremos para você \n Seu número: " + user.Phone,
                            Cancel = "Ok"
                        }, MessageKeys.DisplayAlert);
                        IsBusy = false;
                    });
                }
                return isPressed;
            });
        }));

        private ICommand _onTouchEndCommand;
        public ICommand OnTouchEndCommand => _onTouchEndCommand ?? (_onTouchEndCommand = new Command(() =>
        {
            IsProgressVisible = false;
            isPressed = false;
            Progress = 0;
        }));
    }
}
