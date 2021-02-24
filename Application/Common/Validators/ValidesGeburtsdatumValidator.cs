using System;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Common.Validators
{
    public class ValidesGeburtsdatumValidator<T> : PropertyValidator
    {
        public ValidesGeburtsdatumValidator(string errorMessage) : base(errorMessage) { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            int jetzigesJahr = DateTime.Now.Year;
            int geburtsJahr = ((DateTime)context.PropertyValue).Year;

            if (geburtsJahr <= jetzigesJahr && geburtsJahr > (jetzigesJahr - 120))
                return true;
            return false;
        }
    }
    
    public static class MyValidatorExtensions {
        public static IRuleBuilderOptions<T, DateTime> ValidesGeburtsdatum<T>(this IRuleBuilder<T, DateTime> ruleBuilder, 
            string message) 
        {
            return ruleBuilder.SetValidator(new ValidesGeburtsdatumValidator<DateTime>(message));
        }
    }
}