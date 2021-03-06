// Helpers/Settings.cs
using System;
using Newtonsoft.Json.Linq;
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
        #region Setting Constants
        
        private const string _phoneKey = "phone_key";
        private const string _nameKey = "name_key";
        private const string _emailKey = "email_key";
        private const string _genderKey = "gender_key";
        private const string _birthdayKey = "birthday_key";
        private const string _fiscalNumber = "fiscal_number_key";
        private const string _madreCardNumber = "madre_card_number_key";
        private const string _madreCardActive = "madre_card_active_key";
        private const string _madreCardPassword = "madre_card_password_key";
        private const string _madreCardBalance = "madre_card_balance_key";
        
        #endregion

        private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
        }
        
        public static string Phone
        {
            get
            {
                return AppSettings.GetValueOrDefault(_phoneKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_phoneKey, value);
            }
        }
        
        public static string Name
		{
			get
			{
				return AppSettings.GetValueOrDefault(_nameKey, string.Empty);
			}
			set
			{
				AppSettings.AddOrUpdateValue(_nameKey, value);
			}
		}
        
        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault(_emailKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_emailKey, value);
            }
        }

        public static string Gender
        {
            get
            {
                return AppSettings.GetValueOrDefault(_genderKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_genderKey, value);
            }
        }

        public static string Birthday
        {
            get
            {
                return AppSettings.GetValueOrDefault(_birthdayKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_birthdayKey, value);
            }
        }

        public static string FiscalNumber
        {
            get
            {
                return AppSettings.GetValueOrDefault(_fiscalNumber, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_fiscalNumber, value.Replace(".", "").Replace("-", ""));
            }
        }

        public static string MadreCardNumber
        {
            get
            {
                return AppSettings.GetValueOrDefault(_madreCardNumber, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_madreCardNumber, value);
            }
        }

        public static string MadreCardPassword
        {
            get
            {
                return AppSettings.GetValueOrDefault(_madreCardPassword, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_madreCardPassword, value);
            }
        }

        public static bool MadreCardActive
        {
            get
            {
                return AppSettings.GetValueOrDefault(_madreCardActive, false);
            }
            set
            {
                AppSettings.AddOrUpdateValue(_madreCardActive, value);
            }
        }

        public static string MadreCardBalance
        {
            get
            {
                return AppSettings.GetValueOrDefault(_madreCardBalance, "0");
            }
            set
            {
                AppSettings.AddOrUpdateValue(_madreCardBalance, value);
            }
        }

        public static void Update(JObject result)
        {
            if (result.TryGetValue("ativo", out JToken value)) MadreCardActive = bool.Parse(value.ToString());
            if (result.TryGetValue("numero", out value)) MadreCardNumber = value.ToString();
            if (result.TryGetValue("saldo", out value)) MadreCardBalance = value.ToString();
            if (result.TryGetValue("cpf", out value)) FiscalNumber = value.ToString();
            if (result.TryGetValue("nome", out value)) Name = value.ToString().Trim();

            if (result.TryGetValue("email", out value)) Email = value.ToString().Trim();
            if (result.TryGetValue("gender", out value)) Gender = value.ToString();
            if (result.TryGetValue("name", out value)) Name = value.ToString().Trim();

            if (result.TryGetValue("birthday", out value))
            {
                if (DateTime.TryParse(value.ToString(), out DateTime date))
                    Birthday = date.ToString("dd/MM/yyyy");
            }
        }

        public static object CreateNewUser(string phone, string name, string email, string gender, string birthday, string fiscalNumber, string madreCardPassword)
        {
            Phone = phone;
            Name = name;
            Email = email;
            Gender = gender;
            Birthday = birthday;
            FiscalNumber = fiscalNumber;
            MadreCardPassword = madreCardPassword;
            MadreCardActive = false;
            MadreCardNumber = string.Empty;
            MadreCardBalance = "0,00";

            return new { telefone = Phone, nome = Name, email = Email, genero = Gender, nascimento = Birthday, cpf = FiscalNumber, password = MadreCardPassword };
        }

        internal static string GetAccountInformation()
        {
            return (MadreCardActive ? string.Empty : $"Conta inativa! {Environment.NewLine}") +
                $"Telefone: {Phone}{Environment.NewLine}" +
                $"Nome: {Name}{Environment.NewLine}" +
                $"CPF: {FiscalNumber}{Environment.NewLine}" +
                $"MadreCard: {MadreCardNumber}{Environment.NewLine}" +
                $"Saldo: R$ {MadreCardBalance}{Environment.NewLine}";
        }
        
        public static object Call()
        {
            if (string.IsNullOrWhiteSpace(MadreCardNumber))
            {
                return new { nome = Name, telefone = Phone };
            }
            return new { nome = Name, telefone = Phone, madrecard = new { numero = MadreCardNumber, ativo = MadreCardActive } };
        }

        public static object MadreCardLogin()
        {
            return new { cpf = FiscalNumber, telefone = Phone, madrecard = new { password = MadreCardPassword } };
        }
    }
}