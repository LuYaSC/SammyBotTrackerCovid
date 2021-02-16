using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Connectors;
using TC.Core.Connectors.Models;

namespace TC.Connectors.HealthInsurance.GetInsurancePerson
{
    public class GetInsurancePersonConnector : BaseConnector<GetInsurancePersonRequest, GetInsurancePersonResponse>
    {
        string url = string.Empty;
        string requestUrl = string.Empty;
        HttpWebRequest service;
        byte[] data;

        public GetInsurancePersonConnector(GetInsurancePersonRequest request, string url)
            : base(request, new GetInsurancePersonValidator())
        {
            this.url = url;
            Execute();
        }

        protected override void BeforeProcess()
        {
            string date = request.BornDate.ToString("yyyy-mm-dd") == "0001-00-01" ? string.Empty : request.BornDate.ToString("yyyy-mm-dd");
            requestUrl = $"txtCedula={request.DocumentNumber}&txtComplemento={request.ComplementDocument}&fgdFechaNac={date}&txtNombres={request.Name}&txtPaterno={request.FirstLastName}&txtMaterno={request.SecondLastName}&btnBuscar=";
            data = Encoding.ASCII.GetBytes(requestUrl);
        }

        protected override bool OpenConnenction()
        {
            try
            {
                service = (HttpWebRequest)WebRequest.Create(url);
                service.Method = "POST";
                service.ContentType = "application/x-www-form-urlencoded";
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        protected override void Process()
        {
            service.ContentLength = data.Length;
            using (var stream = service.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var responseUrl = (HttpWebResponse)service.GetResponse();
            if (responseUrl.StatusCode == HttpStatusCode.OK)
            {
                var responseString = new StreamReader(responseUrl.GetResponseStream()).ReadToEnd();
                if (responseString.Contains("NO SE ENCONTRO AL AFILIADO"))
                {
                    Response = BaseResponse<GetInsurancePersonResponse>.SetOneError($"El Sr(a). NO SE ENCUENTRA AFILIADO a ninguna de las Cajas de Salud que pertenecen a la Seguridad Social a Corto Plazo (SSCP)");
                }
                if (responseString.Contains("EXITO"))
                {
                    Response = BaseResponse<GetInsurancePersonResponse>.SetOk(GetDates(responseString));
                }
                else
                {
                    Response = BaseResponse<GetInsurancePersonResponse>.SetOneError($"El numero de doc que ingreso, no se encuentra a ninguna de las Cajas de salud que pertenecen a la Seguridad Social a Corto Plazo(SSCP).");
                }
            }
            else
            {
                Response = BaseResponse<GetInsurancePersonResponse>.SetOneError($"El numero de doc que ingreso, no se encuentra a ninguna de las Cajas de salud que pertenecen a la Seguridad Social a Corto Plazo(SSCP).");
            }
        }

        private GetInsurancePersonResponse GetDates(string html)
        {
            var result = new GetInsurancePersonResponse();
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

        protected override void DisposeConnection()
        {
        }
    }

}
