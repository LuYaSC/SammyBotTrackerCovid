using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Validation;

namespace TC.Connectors.IntHealthInsurance.GetHealthIn
{
    public class GetHealthInValidator : BaseValidator<GetHealthInRequest>
    {
        protected override List<IValidation> RulesValidate => new List<IValidation>
        {
        };
    }
}
