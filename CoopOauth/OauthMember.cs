using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoopOauth
{
    public class OauthMember
    {
        public Int64 Id { get; set; }
        public String Name { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }

        public String Provider { get; set; }
        public String OauthId { get; set; }
        public String AccessToken { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Picture { get; set; }

        public String SafeName
        {
            get
            {
                if (!string.IsNullOrEmpty(LastName))
                {
                    return FirstName + " " + LastName.Substring(0, 1) + '.';
                }
                return FirstName;
            }

        }
    }
}