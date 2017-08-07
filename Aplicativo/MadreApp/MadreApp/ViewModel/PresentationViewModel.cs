using MadreApp.Helpers;
using MadreApp.Pages;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class PresentationViewModel : BaseViewModel
    {
        public PresentationViewModel()
        {
            _position = 0;
            _showSkip = true;
            _pages = new List<DataTemplate>
            {
                new DataTemplate(() => new Presentation1View()),
                new DataTemplate(() => new Presentation2View()),
                new DataTemplate(() => new Presentation3View()),
                new DataTemplate(() => new Presentation4View())
            };
        }

        private int _position;
        public int Position
        {
            get { return _position; }
            set
            {
                if (_position == value) return;
                SetProperty(ref _position, value);

                ShowSkip = _position != _pages.Count - 1;
            }
        }

        private bool _showSkip;
        public bool ShowSkip
        {
            get { return _showSkip; }
            set
            {
                if (_showSkip == value) return;
                SetProperty(ref _showSkip, value);
            }
        }

        private IList<DataTemplate> _pages;
        public IList<DataTemplate> Pages
        {
            get { return _pages; }
            set
            {
                if (_pages == value) return;
                SetProperty(ref _pages, value);
            }
        }

        private ICommand _skipCommand;
        public ICommand SkipCommand => _skipCommand ?? (_skipCommand = new Command(() =>
        {
            Settings.ShowPresentation = false;
            Application.Current.MainPage = new LoginPage();
        }));
    }
}
