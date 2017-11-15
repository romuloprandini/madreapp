using System;
using System.Globalization;
using System.Linq;

namespace MadreApp.Behaviors
{
    public static class Validators
    {
        public static bool BirthdayValidator(string birthday)
        {
            if (birthday == null || birthday.Length != 10) return false;
            return DateTime.TryParseExact(birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);
        }

        public static bool EmailValidator(string email)
        {
            return email != null &&
                // Email contains @, but not at start nor end
                email.Contains("@") && !email.StartsWith("@") && !email.EndsWith("@") &&
                // Email contains @, but not at start nor end
                email.Contains(".") && !email.StartsWith(".") && !email.EndsWith(".");
        }

        public static bool FiscalNumberValidator(string fiscalNumber)
        {
            if (fiscalNumber == null || fiscalNumber.Length != 14) return false;

            var charArray = fiscalNumber.Replace(".", "").Replace("-", "").ToCharArray();
            var isValid = charArray.All(x => char.IsDigit(x)) && !charArray.All(x => x == charArray.First());

            if (isValid)
            {
                var intArray = charArray.Select(x => (int)char.GetNumericValue(x)).ToArray();
                var firstDigit = 0;
                var secondDigit = 0;

                for (var i = 0; i < 9; i++)
                {
                    firstDigit += intArray[i] * (10 - i);
                    secondDigit += intArray[i] * (11 - i);
                }
                firstDigit = 11 - firstDigit % 11;
                secondDigit = 11 - (secondDigit + firstDigit * 2) % 11;

                isValid = intArray[9] == firstDigit && intArray[10] == secondDigit;
            }

            return isValid;
        }

        public static bool PasswordValidator(string password)
        {
            if (password == null || password.Length < 8) return false;
            return !password.ToCharArray().All(x => x == password[0]);
        }

        public static bool PhoneNumberValidator(string phoneNumber)
        {
            if (phoneNumber == null || phoneNumber.Length != 19) return false;
            var charArray = phoneNumber.Substring(5).Replace(") ", "").Replace("-", "").Replace(" ", "").ToCharArray();

            return charArray[0] != '0' &&                      // Cannot start with zero
                charArray.All(x => char.IsDigit(x)) &&         // All must be digits
                !charArray.All(x => x == charArray.First());   // Cannot be all the same digit
        }
    }
}
