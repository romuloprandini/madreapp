using Xamarin.Forms;

namespace MadreApp.Behaviors
{
    public class EmailValidator : Behavior<Entry>
    {
        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(PhoneNumberValidator), false);
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
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = Validators.EmailValidator(e.NewTextValue);
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }
    }
}