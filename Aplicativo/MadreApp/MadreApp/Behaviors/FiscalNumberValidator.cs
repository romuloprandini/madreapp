using Xamarin.Forms;

namespace MadreApp.Behaviors
{
    public class FiscalNumberValidator : Behavior<Entry>
    {
        private Color defaultColor;

        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(FiscalNumberValidator), true);
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
            var old = e.OldTextValue;
            var text = e.NewTextValue;
            var mask = "XXX.XXX.XXX-XX";

            // if Entry text is longer then valid length
            if (text.Length > mask.Length)
            {
                var index = 0;
                while (text[index] == old[index] && index < old.Length - 1) index++;
                text = text.Remove(index + 1);
            }

            for (var i = 0; i < text.Length - 1; i++)
            {
                var maskChar = mask[i];
                var currChar = text[i];
                if (currChar == maskChar || maskChar == 'X') continue;
                var past = text.Substring(0, i);
                var next = text.Substring(i);
                text = past + maskChar + next;
            }

            IsValid = Validators.FiscalNumberValidator(e.NewTextValue);
            ((Entry)sender).Text = text;
            ((Entry)sender).TextColor = IsValid ? defaultColor : Color.Red;
        }
    }
}