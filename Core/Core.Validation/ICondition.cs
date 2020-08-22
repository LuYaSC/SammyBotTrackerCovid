using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Validation
{
    public interface ICondition<TYPE>
    {
        bool Execute(TYPE value);
    }
}
