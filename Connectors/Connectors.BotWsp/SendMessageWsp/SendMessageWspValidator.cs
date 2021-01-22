using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Validation;

namespace TC.Connectors.BotWsp.SendMessageWsp
{
    public class SendMessageWspValidator : BaseValidator<SendMessageWspRequest>
    {
        protected override List<IValidation> RulesValidate => new List<IValidation>
        {

        };
    }
}