using Xamarin.Forms;

namespace MadreApp.Behaviors
{
    public class ComparisonBehavior : Behavior<Entry>
    {
        private Entry _thisEntry;
        public static readonly BindableProperty CompareToEntryProperty = BindableProperty.Create("CompareToEntry", typeof(Entry), typeof(ComparisonBehavior), null);
        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(ComparisonBehavior), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
        
        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            private set { SetValue(IsValidPropertyKey, value); }
        }

        public Entry CompareToEntry
        {
            get { return (Entry)GetValue(CompareToEntryProperty); }
            set
            {
                SetValue(CompareToEntryProperty, value);
                if (CompareToEntry != null)
                    CompareToEntry.TextChanged -= Bindable_TextChanged;
                value.TextChanged += Bindable_TextChanged;
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            _thisEntry = bindable;

            if (CompareToEntry != null)
                CompareToEntry.TextChanged += Bindable_TextChanged;

            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            if (CompareToEntry != null)
                CompareToEntry.TextChanged -= Bindable_TextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = ((Entry)sender).Text.Equals(_thisEntry.Text);
            _thisEntry.TextColor = IsValid ? Color.Green : Color.Red;
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (bool)CompareToEntry.Text?.Equals(e.NewTextValue);
            ((Entry)sender).TextColor = IsValid ? Color.Green : Color.Red;
        }
    }
}