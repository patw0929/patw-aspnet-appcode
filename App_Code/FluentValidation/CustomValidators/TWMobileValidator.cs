using System.Collections.Generic;
using FluentValidation.Validators;

public class TWMobileValidator : PropertyValidator
{

    public TWMobileValidator()
        : base("{PropertyName} 必須是正確的台灣手機號碼格式。")
    {

    }

    protected override bool IsValid(PropertyValidatorContext context)
    {
        return ValidatorFuncs.IsMobile((string)context.PropertyValue);
    }
}