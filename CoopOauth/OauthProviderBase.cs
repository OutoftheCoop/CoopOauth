using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoopOauth
{
    abstract public class OauthProviderBase
    {
        abstract public OauthMemberData GetOauthMemberData(OauthRequest request);
        abstract public string GetName();

        public OauthMember Get(OauthRequest request)
        {
            var data = GetOauthMemberData(request);
            return new OauthMember()
            {
                Name = data.FirstName + " " + data.LastName,
                Username = GetName() + data.OauthId,
                Email = GetName() + data.OauthId + "@outofthecoop.com",
                
                Provider = GetName(),
                OauthId = data.OauthId,
                AccessToken = data.AccessToken,

                FirstName = data.FirstName,
                LastName = data.LastName,
                Picture = data.Picture
            };
        }
    }
}