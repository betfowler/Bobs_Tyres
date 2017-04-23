using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bobs_Tyres.Models
{
    public class ReCaptchaClass
    {
        public string success { get; set; }

        public string Validate(string EncodedResponse)
        {
            var client = new WebClient();
            string PrivateKey = "6LeHVh0UAAAAAFHTfzxLL8LSFhmT27ZawwG9Oxbb";
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));
            JObject jobject = JObject.Parse(reply);
            success = (string)jobject["success"];
            //var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaClass>(reply);
            return success;
        }

        
    }
}