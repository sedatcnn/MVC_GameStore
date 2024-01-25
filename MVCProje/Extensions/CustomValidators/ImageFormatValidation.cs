using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace MVCProje.Extensions.CustomValidators
{
    public class ImageFormatAttribute : ValidationAttribute
    {
        private readonly string[] allowedFormats = { "png", "jpg", "jpeg", "gif", "webp", "bmp" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string imageUrl = value.ToString();
            string extension = Path.GetExtension(imageUrl)?.TrimStart('.');

            if (!IsValidImageFormat(extension))
            {
                string allowedFormatsString = string.Join(", ", allowedFormats);
                return new ValidationResult($"Geçersiz resim formatı. Desteklenen formatlar: {allowedFormatsString}");
            }

            return ValidationResult.Success;
        }

        private bool IsValidImageFormat(string extension)
        {
            if (extension == null)
            {
                return false;
            }

            extension = extension.ToLower();

            foreach (var allowedFormat in allowedFormats)
            {
                if (extension == allowedFormat.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
