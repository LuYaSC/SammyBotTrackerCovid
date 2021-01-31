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

namespace TC.Functions.GetInformationPatient.Business
{
    public class GetInformationPatientBusiness : BaseBusiness<CasosAgenda, GetInformationPatientContext>, IGetInformationPatientBusiness
    {
        public GetInformationPatientBusiness(GetInformationPatientContext context, IPrincipal userInfo, IConfiguration configuration = null)
            : base(context, userInfo, configuration)
        {
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
                if(responseString.Contains("NO SE ENCONTRO AL AFILIADO"))
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
                .Replace("</h4>", string.Empty).Replace("<br>",string.Empty).Replace("\n", string.Empty).Trim();
            var au2 = aux.Replace("<strong>", "x").Split('x');
            result.Name = au2[2].Replace("</strong>", "x").Split('x')[1];
            result.DocumentNumber = au2[3].Replace("</strong>", "x").Split('x')[1];
            result.BornDate = au2[4].Replace("</strong>", "x").Split('x')[1];
            result.Insurance = au2[5].Replace("</strong>", "x").Split('x')[1];
            result.Departament = au2[6].Replace("</strong>", "x").Split('x')[1];
            result.Municipality = au2[7].Replace("</strong>", "x").Split('x')[1];
            return result;
        }
    }
}
