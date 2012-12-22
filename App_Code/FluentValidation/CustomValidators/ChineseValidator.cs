using System.Collections.Generic;
using FluentValidation.Validators;

public class ChineseValidator : PropertyValidator
{

    public ChineseValidator()
        : base("{PropertyName} 必須是中文。")
    {

    }

    protected override bool IsValid(PropertyValidatorContext context)
    {
        return ValidatorFuncs.IsChinese((string)context.PropertyValue);
    }
}