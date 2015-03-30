using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoopOauth
{
    public class OauthMemberData
    {
        public String OauthId { get; set; }
        public String AccessToken { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Picture { get; set; }
    }
}