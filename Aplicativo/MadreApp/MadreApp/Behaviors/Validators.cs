using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadreApp.Behaviors
{
    public static class Validators
    {
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
            if (fiscalNumber == null) return false;

            var charArray = fiscalNumber.ToCharArray();
            var isValid = charArray.Length == 11 && charArray.All(x => char.IsDigit(x)) && !charArray.All(x => x == charArray.First());

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
            if (password == null) return false;

            var charArray = password.ToCharArray();
            return charArray.Length > 7 && !charArray.All(x => x == charArray.First());
        }

        public static bool PhoneNumberValidator(string phoneNumber)
        {
            if (phoneNumber == null) return false;

            var charArray = phoneNumber.ToCharArray();
            return charArray.Length == 10 && charArray.All(x => char.IsDigit(x)) && !charArray.All(x => x == charArray.First());
        }
    }
}
