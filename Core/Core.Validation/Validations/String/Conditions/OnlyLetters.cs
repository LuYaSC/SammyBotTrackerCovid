using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TC.Core.Validation.Validations.String.Conditions
{
    public class OnlyLetters : IStringCondition
    {
        private const string ACCOUNT_PATTERN = @"(^[a-zA-Z-,.ñÑ ]*$)";

        public bool Execute(string value)
        {
            return Regex.IsMatch(value, ACCOUNT_PATTERN);
        }
    }
}
