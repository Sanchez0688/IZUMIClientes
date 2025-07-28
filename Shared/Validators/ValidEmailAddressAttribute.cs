
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Shared.Validators
{
	public class ValidEmailAddressAttribute : ValidationAttribute
	{
		private static readonly Regex EmailRegex = new Regex(
		   @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
		   RegexOptions.Compiled | RegexOptions.IgnoreCase);

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
			{
				return new ValidationResult("El campo Email es obligatorio.");
			}

			var email = value.ToString();
			if (!EmailRegex.IsMatch(email))
			{
				return new ValidationResult("El formato del Email no es válido.");
			}

			return ValidationResult.Success;
		}
	}
}
