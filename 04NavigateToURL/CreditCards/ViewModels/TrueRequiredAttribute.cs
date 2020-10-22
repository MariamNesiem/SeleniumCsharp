using System;
using System.ComponentModel.DataAnnotations;

namespace CreditCards.ViewModels
{
    internal sealed class TrueRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {            
            if (value is null)
            {
                return false;
            }

            if (value.GetType() != typeof(bool))
            {
                throw new InvalidOperationException("This validation can only be performed on boolean values.");
            }

            return (bool)value;
        }
    }
}
