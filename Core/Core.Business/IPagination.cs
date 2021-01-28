using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Business
{
    public interface IPagination
    {
        int PageSize { get; set; }

        int CurPage { get; set; }
    }
}
