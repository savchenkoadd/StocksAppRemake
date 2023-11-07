using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.CustomValidationAttributes
{
	public class ValidDateAttribute : ValidationAttribute
	{
		private readonly DateTime _minDate;

		public ValidDateAttribute()
		{
			_minDate = new DateTime(2000, 1, 1);
		}

		public override bool IsValid(object? value)
		{
			if (value is DateTime date)
			{
				return date >= _minDate;
			}
			return false;
		}
	}
}
