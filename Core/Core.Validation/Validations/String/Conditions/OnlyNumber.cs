namespace TC.Core.Validation.Validations.String.Conditions
{
    using System.Text.RegularExpressions;

    public class OnlyNumber : IStringCondition
    {
        private const string ACCOUNT_PATTERN = @"(^[0-9]*$)";

        public bool Execute(string value)
        {
            return Regex.IsMatch(value, ACCOUNT_PATTERN);
        }
    }
}
