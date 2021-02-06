using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Core.Data;
using System.Text.RegularExpressions;
using TC.Functions.GetInformationPatient.Business.Models;
using TC.Connectors.RiskCovid;
using TC.Connectors.RiskCovid.CalculateRiskCovid;
using AutoMapper;

namespace TC.Functions.GetInformationPatient.Business
{
    public class GetInformationPatientBusiness : BaseBusiness<CasosAgenda, GetInformationPatientContext>, IGetInformationPatientBusiness
    {
        IRiskCovidManager riskService;
        IMapper mapper;

        public GetInformationPatientBusiness(GetInformationPatientContext context, IPrincipal userInfo, IConfiguration configuration, IRiskCovidManager riskService)
            : base(context, userInfo, configuration)
        {
            this.riskService = riskService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<string, string>()
                    .ConvertUsing(str => (str ?? "").Trim());

                //cfg.CreateMap<bool, int>()
                //  .ConvertUsing(str => (str ? 1 : 0));

                //cfg.CreateMap<User, GetUserResult>()
                //    .ForMember(d => d.TotalItems, o => o.MapFrom(s => totalItems))
                //    .ForMember(d => d.Name, o => o.MapFrom(s => s.UserDetail.Name))
                //    .ForMember(d => d.FirstLastName, o => o.MapFrom(s => s.UserDetail.FirstLastName))
                //    .ForMember(d => d.SecondLastName, o => o.MapFrom(s => s.UserDetail.SecondLastName))
                //    .ForMember(d => d.UserRoles, o => o.MapFrom(s => s.UserRoles));

                cfg.CreateMap<GetRiskCovidDto, CalculateRiskCovidRequest>()
                      .ForMember(d => d.UOMSYSTEM, o => o.MapFrom(s => false))
                      .ForMember(d => d.webLanguage, o => o.MapFrom(s => "english"))
                      .ForMember(d => d.age, o => o.MapFrom(s => GetAge(s.Age)))
                      .ForMember(d => d.xray, o => o.MapFrom(s =>  s.Xray ? 1 : 0))
                      .ForMember(d => d.hemo, o => o.MapFrom(s => s.Hemo ? 1 : 0))
                      .ForMember(d => d.dys, o => o.MapFrom(s => s.Dys ? 1 : 0))
                      .ForMember(d => d.uncon, o => o.MapFrom(s => s.Uncon ? 1 : 0))
                      .ForMember(d => d.cancer, o => o.MapFrom(s => s.Cancer ? 1 : 0))
                      .ForMember(d => d.ratio, o => o.MapFrom(s => s.Ratio))
                      .ForMember(d => d.bili, o => o.MapFrom(s => s.Bili))
                      .ForMember(d => d.comorbid, o => o.MapFrom(s => s.Comorbid))
                      .ForMember(d => d.lactate, o => o.MapFrom(s => s.Lactate));

                //cfg.CreateMap<GetUserDto, RegisterUserRequest>()
                //  .ForMember(d => d.User, o => o.MapFrom(s => "ADMIN"));
            });
            mapper = new Mapper(config);
        }

        public static int GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            return (a - b) / 10000;
        }

        public Result<GetInsurancePatientResult> GetInsurancePatient(GetInsurancePatientDto dto)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://seguros.minsalud.gob.bo/Asegurados%20SSCP/mdlVerificaAfiliado.php");
            string date = dto.BornDate.ToString("yyyy-mm-dd") == "0001-00-01" ? string.Empty : dto.BornDate.ToString("yyyy-mm-dd");
            var postData = $"txtCedula={dto.DocumentNumber}&txtComplemento={dto.ComplementDocument}&fgdFechaNac={date}&txtNombres={dto.Name}&txtPaterno={dto.FirstLastName}&txtMaterno={dto.SecondLastName}&btnBuscar=";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                if (responseString.Contains("NO SE ENCONTRO AL AFILIADO"))
                {
                    return Result<GetInsurancePatientResult>.SetError($"El Sr(a). NO SE ENCUENTRA AFILIADO a ninguna de las Cajas de Salud que pertenecen a la Seguridad Social a Corto Plazo (SSCP)");
                }
                if (responseString.Contains("EXITO"))
                {
                    return Result<GetInsurancePatientResult>.SetOk(GetDates(responseString));
                }
                else
                {
                    return Result<GetInsurancePatientResult>.SetError($"El numero de doc que ingreso, no se encuentra a ninguna de las Cajas de salud que pertenecen a la Seguridad Social a Corto Plazo(SSCP).");
                }
            }
            else
            {
                return Result<GetInsurancePatientResult>.SetError($"El numero de doc que ingreso, no se encuentra a ninguna de las Cajas de salud que pertenecen a la Seguridad Social a Corto Plazo(SSCP).");
            }
        }

        private GetInsurancePatientResult GetDates(string html)
        {
            var result = new GetInsurancePatientResult();
            string email = html;
            int startPos = email.LastIndexOf("<div class=\"content2 alert alert alert-success\">") + "<div class=\"content2 alert alert alert-success\">".Length + 1;
            int length = email.IndexOf("<div class=\"modal-footer\">") - startPos;
            string sub = email.Substring(startPos, length);
            var aux = sub.Replace("<h4>", string.Empty).Replace("<hr>", string.Empty).Replace("<br/>", string.Empty).Replace("<div>", string.Empty).Replace("</div>", string.Empty)
                .Replace("</h4>", string.Empty).Replace("<br>", string.Empty).Replace("\n", string.Empty).Trim();
            var au2 = aux.Replace("<strong>", "x").Split('x');
            result.Name = au2[2].Replace("</strong>", "x").Split('x')[1];
            result.DocumentNumber = au2[3].Replace("</strong>", "x").Split('x')[1];
            result.BornDate = au2[4].Replace("</strong>", "x").Split('x')[1];
            result.Insurance = au2[5].Replace("</strong>", "x").Split('x')[1];
            result.Departament = au2[6].Replace("</strong>", "x").Split('x')[1];
            result.Municipality = au2[7].Replace("</strong>", "x").Split('x')[1];
            return result;
        }

        public Result<GetRiskCovidResult> GetRiskCovid(GetRiskCovidDto dto)
        {
            var result = riskService.CalculateRiskCovid(mapper.Map<CalculateRiskCovidRequest>(dto));
            if (!result.Header.IsOk)
            {
                return Result<GetRiskCovidResult>.SetError("No fue posible realizar el cálculo, favor intentar nuevamente (c)");
            }
            if (result.Body == null)
            {
                return Result<GetRiskCovidResult>.SetError("No fue posible realizar el cálculo, favor intentar nuevamente (n)");
            }
            if(!result.Body.output.Any())
            {
                return Result<GetRiskCovidResult>.SetError("No fue posible realizar el cálculo, favor intentar nuevamente (a)");
            }
            return Result<GetRiskCovidResult>.SetOk(new GetRiskCovidResult { Points = $"{result.Body.output[1].value} puntos",
            Percent = $"{result.Body.output[2].value}{result.Body.output[2].value_text}",
            Risk = $"{result.Body.output[3].value} {result.Body.output[3].value_text}",
            Message = $"{result.Body.output[2].message}"
            });
        }
    }
}
