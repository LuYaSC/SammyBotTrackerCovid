namespace TC.Core.Validation.Validations.String.Conditions
{
    using System.Text.RegularExpressions;

    public class Email : IStringCondition
    {
        private const string ACCOUNT_PATTERN = @"^(?("")("".+?(?<!\\)""@)|(([a-zA-Z\-0-9]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[a-zA-Z\-0-9])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([a-zA-Z\-0-9][a-zA-Z\-0-9]*[0-9a-z]*\.)+[a-zA-Z\-0-9][\a-zA-Z\-0-9]{0,22}[a-zA-Z\-0-9]))$";

        public bool Execute(string value)
        {
            return Regex.IsMatch(value, ACCOUNT_PATTERN);
        }
    }
}
