using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoopOauth.Facebook
{
    public class FacebookProvider : OauthProviderBase
    {
        private string AppId;
        private string AppSecret;

        public FacebookProvider()
        {
            AppId = CoopRelay.Tools.AppSettings.Get<String>("CoopOauth_FacebookAppId");
            AppSecret = CoopRelay.Tools.AppSettings.Get<String>("CoopOauth_FacebookAppSecret");
        }

        override public string GetName()
        {
            return "Facebook";
        }

        override public OauthMemberData GetOauthMemberData(OauthRequest request)
        {
            // Get Long Term Access Token
            string longtermaccesstoken = FacebookGraphApi.GetLongTermAccessToken(request.AccessToken, AppId, AppSecret);

            // Use Long Term Access Token to Get FBUser
            var oauthmemberdata = FacebookGraphApi.Get(longtermaccesstoken);

            if (string.IsNullOrEmpty(oauthmemberdata.OauthId) || string.IsNullOrEmpty(oauthmemberdata.Picture))
            {
                throw new Exception("Unable to get profile information from Facebook.");
            }

            return oauthmemberdata;
        }
    }
}