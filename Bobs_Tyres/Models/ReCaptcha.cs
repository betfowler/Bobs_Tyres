using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Bobs_Tyres.Models
{
    public class ReCaptcha
    {
        private string m_Success;
        [JsonProperty("success")]
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }
        private List<string> m_ErrorCodes;
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }

        public static string Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();
            string PrivateKey = "6LdquhwUAAAAAEtuekGNj5eQqPPqs6IHVRm_G4Rb";
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));
            var captchaResponse = JsonConvert.DeserializeObject<ReCaptcha>(reply);
            return captchaResponse.Success;
        }
    }
}