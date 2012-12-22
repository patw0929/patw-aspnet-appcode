using System.Collections.Generic;
using FluentValidation.Validators;

public class TWIDValidator : PropertyValidator
{

    public TWIDValidator()
        : base("{PropertyName} 必須是正確的台灣身分證字號。")
    {

    }

    protected override bool IsValid(PropertyValidatorContext context)
    {
        return ValidatorFuncs.IsValidTWID((string)context.PropertyValue);
    }
}