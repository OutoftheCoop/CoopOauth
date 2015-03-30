using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoopOauth
{
    public class OauthRequest
    {
        public String AccessToken { get; set; }
        public String Code { get; set; }
    }
}