using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Validation
{
    public interface IValidation
    {
        bool Validate();

        string ErrorMessage { get; }

        string Field { get; }
    }
}
