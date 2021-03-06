﻿using MadreApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MadreApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FullRegisterPage : ContentPage
	{
		public FullRegisterPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

			InitializeComponent ();

            BindingContext = new FullRegisterViewModel();

        }

        protected override void OnAppearing()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            base.OnAppearing();
        }
    }
}