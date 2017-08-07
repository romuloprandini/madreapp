// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MadreApp.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string NomeKey = "nome_key";
        private const string TelefoneKey = "telefone_key";
        private const string ShowPresentationKey = "show_presentation_key";

        #endregion

        public static bool ShowPresentation
        {
            get
            {
                return AppSettings.GetValueOrDefault(ShowPresentationKey, true);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ShowPresentationKey, value);
            }
        }

        public static string Nome
		{
			get
			{
				return AppSettings.GetValueOrDefault(NomeKey, string.Empty);
			}
			set
			{
				AppSettings.AddOrUpdateValue(NomeKey, value);
			}
		}

        public static string Telefone
        {
            get
            {
                return AppSettings.GetValueOrDefault(TelefoneKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(TelefoneKey, value);
            }
        }
    }
}