using FluentValidation;

public static class FluentValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> IsDate<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new DateValidator());
    }

    public static IRuleBuilderOptions<T, string> IsChinese<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new ChineseValidator());
    }

    public static IRuleBuilderOptions<T, string> IsTWID<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new TWIDValidator());
    }

    public static IRuleBuilderOptions<T, string> IsMobile<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new TWMobileValidator());
    }
}