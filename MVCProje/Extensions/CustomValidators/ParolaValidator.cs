using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GameStore.Extensions.CustomValidators
{
    public class ParolaValidator: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("Sifre boş olamaz");
            }

            string password = value.ToString();

            if (password.Length < 6 || !HasUpperCase(password) || !HasLowerCase(password) || !HasDigit(password) || !HasSpecialCharacter(password))
            {
                return new ValidationResult("Şifre yeterince güçlü değil. En az 6 karakter uzunluğunda olmalı, en az 1 büyük harf, 1 küçük harf, 1 rakam ve 1 özel karakter içermelidir.");
            }

            return ValidationResult.Success;
            }

         private bool HasUpperCase(string input)
         {
             return Regex.IsMatch(input, @"[A-Z]");
         }

         private bool HasLowerCase(string input)
         {
             return Regex.IsMatch(input, @"[a-z]");
         }

         private bool HasDigit(string input)
         {
             return Regex.IsMatch(input, @"\d");
         }

         private bool HasSpecialCharacter(string input)
         {
             return Regex.IsMatch(input, @"[^A-Za-z0-9]");
         }
    }
}
