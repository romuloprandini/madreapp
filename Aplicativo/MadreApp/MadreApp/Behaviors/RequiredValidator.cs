﻿using System.Linq;
using Xamarin.Forms;

namespace MadreApp.Behaviors
{
    public class RequiredValidator : Behavior<Entry>
    {
        private Color defaultColor;

        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(RequiredValidator), true);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            private set { SetValue(IsValidPropertyKey, value); }
        }
        
        protected override void OnAttachedTo(Entry bindable)
        {
            IsValid = true;
            bindable.TextChanged += Bindable_TextChanged;

            defaultColor = bindable.TextColor;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = !string.IsNullOrWhiteSpace(e.NewTextValue);
            ((Entry)sender).TextColor = IsValid ? defaultColor : Color.Red;
        }
    }
}