using CustomerManagementSystem.Services.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Xml;

namespace CustomerManagementSystem.Services
{
    public class MernisValidationService : IMernisValidationService
    {
        private const string m_ValidationUrl = @"https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx?op=TCKimlikNoDogrula";
        private const string m_ValidationAction = @"http://tckimlik.nvi.gov.tr/WS/TCKimlikNoDogrula";

        public bool ValidateCustomer(long tckn, string name, string surname, int birthdate)
        {
            var request = CreateSOAPWebRequest();
            var xml = new XmlDocument();
            var body = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
                <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
                  <soap12:Body>
                    <TCKimlikNoDogrula xmlns=""http://tckimlik.nvi.gov.tr/WS"">
                      <TCKimlikNo> {tckn} </TCKimlikNo>
                      <Ad> {name} </Ad>
                      <Soyad> {surname} </Soyad>
                      <DogumYili> {birthdate} </DogumYili>
                    </TCKimlikNoDogrula>
                  </soap12:Body>
                </soap12:Envelope>";

            xml.LoadXml(body);

            using (var stream = request.GetRequestStream())
            {
                xml.Save(stream);
            }
            
            using var services = request.GetResponse();
            using var rd = new StreamReader(services.GetResponseStream());
            var response = rd.ReadToEnd();
            xml.LoadXml(response);
            return xml.InnerText == "true";
        }

        private static HttpWebRequest CreateSOAPWebRequest()
        {
            var req = (HttpWebRequest)WebRequest.Create(m_ValidationUrl);
            req.Headers.Add(m_ValidationAction);
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "text/xml";
            req.Method = "POST";
            return req;
        }
    }
}
