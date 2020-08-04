﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Functions.CaseRecovery.Business.Models;

namespace CaseRecovery.Business
{
    public interface ICaseRecoveryBusiness
    {
        Result<List<CasesForRecoverResult>> GetCasesForRecovers(GetDataDto dto);

        Result<bool> RecoverCase(GetDataDto dto);

        Result<string> GenerateRoom(GetDataDto dto);

        Result<string> FinalizeCase(GetDataDto dto);
    }
}
