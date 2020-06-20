using TC.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Connectors.Models
{
    public class PasePayValidator : BaseValidator<PasePayRequest>
    {
        protected override List<IValidation> RulesValidate => new List<IValidation>
        {
            //new StringValidation<Required>(Data.NUS, "El NUS es requerido"),
            //new StringValidation<Required>(Data.InvoiceNumber, "El número de factura es requerido")
        };
    }

}