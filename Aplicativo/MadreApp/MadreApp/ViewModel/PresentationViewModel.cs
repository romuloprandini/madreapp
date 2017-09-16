﻿using MadreApp.Helpers;
using MadreApp.Pages;
using MvvmHelpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class PresentationViewModel : BaseViewModel
    {
        private ICommand _madreCardLoginCommand;
        private ICommand _newUserLoginCommand;
        private ICommand _facebookLoginCommand;
        private ICommand _onCancelFacebook;
        private ICommand _onErrorFacebook;
        private ICommand _onSucessFacebook;
        private IList<DataTemplate> _pages;

        public ICommand MadreCardLoginCommand => _madreCardLoginCommand ?? (_madreCardLoginCommand = new Command(() => Application.Current.MainPage = new MadreCardLoginPage()));

        public ICommand NewUserLoginCommand => _newUserLoginCommand ?? (_newUserLoginCommand = new Command(() => Application.Current.MainPage = new NewUserLoginPage()));

        public ICommand FacebookLoginCommand => _facebookLoginCommand ?? (_facebookLoginCommand = new Command(() => IsBusy = true));

        public ICommand OnCancelFacebook => _onCancelFacebook ?? (_onCancelFacebook = new Command(() => IsBusy = false));

        public ICommand OnErrorFacebook => _onErrorFacebook ?? (_onErrorFacebook = new Command(() =>
        {
            Application.Current.MainPage?.DisplayAlert("Erro", "Não foi possível logar pelo facebook", "Ok");
            IsBusy = false;
        }));

        public ICommand OnSucessFacebook => _onSucessFacebook ?? (_onSucessFacebook = new Command(result =>
        {
            if (result == null)
            {
                Application.Current.MainPage?.DisplayAlert("Erro", "Não foi possível logar pelo facebook", "Ok");
            }
            else
            {
                try
                {
                    var json = JObject.Parse((string)result);
                    Settings.Birthday = json["birthday"].ToString();
                    Settings.Email = json["email"].ToString();
                    Settings.Gender = json["gender"].ToString();
                    Settings.Name = json["name"].ToString();
                }
                catch (Exception)
                {
                    Application.Current.MainPage?.DisplayAlert("Erro", "Não foi possível logar pelo facebook", "Ok");
                }
            }
            IsBusy = false;
            Application.Current.MainPage = new NewUserLoginPage();
        }));

        public IList<DataTemplate> Pages
        {
            get { return _pages; }
            set
            {
                if (_pages == value) return;
                SetProperty(ref _pages, value);
            }
        }

        public PresentationViewModel()
        {
            _pages = new List<DataTemplate>
            {
                new DataTemplate(typeof(Presentation1View)),
                new DataTemplate(typeof(Presentation2View)),
                new DataTemplate(typeof(Presentation3View)),
                new DataTemplate(typeof(Presentation4View)),
                new DataTemplate(typeof(Presentation6View))
            };
        }
    }
}