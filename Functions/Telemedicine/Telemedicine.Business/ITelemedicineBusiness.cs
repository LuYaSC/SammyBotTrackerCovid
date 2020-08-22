using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Functions.Telemedicine.Business.Models;

namespace TC.Functions.Telemedicine.Business
{
    public interface ITelemedicineBusiness
    {
        Result<List<PendingAppointmentsResult>> GetPendingAppointments(GetDataDto dto);

        Result<List<PendingAppointmentsResult>> GetPatientsAttended(GetDataDto dto);

        Result<AssingCaseResult> AssingCase(GetDataDto dto);

        Result<string> AddingDoctor(GetDataDto dto);

        Result<string> UpdateCase(GetDataDto dto);

        Result<List<DoctorResult>> GetListDoctor();
    }
}
