using System.Collections.Generic;
using FluentValidation.Validators;

public class DateValidator : PropertyValidator
{

    public DateValidator()
        : base("{PropertyName} 必須是一個正確的日期格式。")
    {

    }

    protected override bool IsValid(PropertyValidatorContext context)
    {
        try
        {
            System.DateTime.Parse((string)context.PropertyValue);
        }
        catch
        {
            return false;
        }

        return true;
    }
}