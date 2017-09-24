using Xamarin.Forms;

namespace MadreApp.Behaviors
{
    public class BirthdayValidator : Behavior<Entry>
    {
        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(BirthdayValidator), false);
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
            var entry = (Entry)sender;
            var entryText = entry.Text;

            foreach (var index in new[] { 2, 5 })
            {
                if (entryText.Length == index + 1 && entryText[index] != '/')
                {
                    entryText = entryText.Substring(0, index) + '/' + entryText[index];
                }
            }

            // if Entry text is longer then valid length
            if (entry.Text.Length > 10)
            {
                entryText = entryText.Remove(entryText.Length - 1);
            }

            entry.Text = entryText;

            IsValid = Validators.BirthdayValidator(e.NewTextValue);
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }
    }
}