using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Connectors.Models
{
    public class Parameter
    {
        public Parameter(string name, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            this.Name = name;
            this.Value = value;
            this.Direction = direction;
        }

        public string Name { get; set; }

        public object Value { get; set; }

        public ParameterDirection Direction { get; set; }
    }
}
