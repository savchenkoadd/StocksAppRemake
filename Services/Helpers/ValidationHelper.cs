using ServiceContracts.DTO;
using System.ComponentModel.DataAnnotations;

namespace Services.Helpers
{
	internal static class ValidationHelper
	{ 
        internal static void Validate(object? obj)
		{
			if (obj is null)
			{
				throw new ArgumentNullException();
			}

			List<ValidationResult> results = new List<ValidationResult>();
			ValidationContext validationContext = new ValidationContext(obj);

			if (!Validator.TryValidateObject(obj, validationContext, validationResults: results, validateAllProperties: true))
			{
				throw new ArgumentException(results.First().ErrorMessage);
			}
		}
	}
}
