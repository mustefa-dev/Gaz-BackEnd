using Microsoft.OpenApi.Exceptions;
using System.Net;
using System.Text;

namespace Gaz_BackEnd.Helpers
{
    public class SendSms
    {
        public bool SendMessage(string to, string data)
        {


            to = (to).Trim().TrimStart('0').Replace(" ", "");

            var obj = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                body = data,
                phone = to
            });
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://sms.esite-iq.com/api/new-sms");
                request.Method = "POST";
                request.Headers.Add("ESITE-API-KEY", "bgY49bc88ec464a2bbv8fb049ba9F36918c14Jd9");
                request.ContentType = "application/json";

                var bytes = Encoding.UTF8.GetBytes(obj);
                request.ContentLength = bytes.Length;

                var stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
                var response = (HttpWebResponse)request.GetResponse();
                var rs = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return true;
            }
            catch (OpenApiException)
            {
                return false;
            }
        }
    }
}
