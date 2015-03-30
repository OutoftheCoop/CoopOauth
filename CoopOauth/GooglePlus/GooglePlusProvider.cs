using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoopOauth.GooglePlus
{
    public class GooglePlusProvider : OauthProviderBase
    {
        //private string ClientId;
        //private string ClientSecret;

        public GooglePlusProvider()
        {
            //ClientId = CoopRelay.Tools.AppSettings.Get<String>("CoopOauth_GooglePlusClientId");
            //ClientSecret = CoopRelay.Tools.AppSettings.Get<String>("CoopOauth_GooglePlusClientSecret");
        }

        override public string GetName()
        {
            return "GooglePlus";
        }

        override public OauthMemberData GetOauthMemberData(OauthRequest request)
        {
            var oauthmemberdata = GooglePlusApi.Get(request.AccessToken);

            if (string.IsNullOrEmpty(oauthmemberdata.OauthId))
            {
                throw new Exception("Unable to get profile information from Google.");
            }

            // Store refresh token for long term access
            //oauthmemberdata.AccessToken = GooglePlusApi.GetRefreshToken(request.Code);

            return oauthmemberdata;
        }
    }
}