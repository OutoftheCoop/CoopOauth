using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoopOauth
{
    public class OauthMemberRepository
    {
        public static OauthMember Current
        {
            get
            {
                return Get((int)System.Web.Security.Membership.GetUser().ProviderUserKey);
            }
        }

        public static void Checkin(OauthMember m)
        {
            if (!CoopRelay.Relay.Member.IsExist(m.Username))
            {
                Create(m);
            }
            m.Id = Save(m);
        }

        private static int Create(OauthMember m)
        {
            return CoopRelay.Relay.Member.Create(m.Username, "ey860vP8TSkDbtF0oQ1g", m.Email, m.Name, "OauthMember", GenerateNvc(m));
        }

        private static int Save(OauthMember m)
        {
            return CoopRelay.Relay.Member.Update(m.Username, m.Name, m.Email, GenerateNvc(m));
        }

        private static System.Collections.Specialized.NameValueCollection GenerateNvc(OauthMember m)
        {
            var nvc = new System.Collections.Specialized.NameValueCollection();
            nvc.Add("provider", m.Provider);
            nvc.Add("oauthId", m.OauthId);
            nvc.Add("accessToken", m.AccessToken);
            nvc.Add("firstName", m.FirstName);
            nvc.Add("lastName", m.LastName);
            nvc.Add("picture", m.Picture);
            return nvc;
        }

        public static OauthMember Get(int id)
        {
            var m = CoopRelay.Relay.Member.Get(id);
            return new OauthMember()
            {
                Id = m.Id,
                Username = m.Username,
                Email = m.Email,
                Name = m.Name,
                Provider = m.Properties.GetString("provider"),
                OauthId = m.Properties.GetString("oauthId"),
                AccessToken = m.Properties.GetString("accessToken"),
                FirstName = m.Properties.GetString("firstName"),
                LastName = m.Properties.GetString("lastName"),
                Picture = m.Properties.GetString("picture")
            };
        }
    }
}