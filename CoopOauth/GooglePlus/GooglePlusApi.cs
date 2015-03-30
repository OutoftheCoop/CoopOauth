using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoopOauth.GooglePlus
{
    public class GooglePlusApi
    {
        //public static string GetRefreshToken(string code)
        //{
        //    string url = "https://www.googleapis.com/oauth2/v3/token";
        //    var r = new CoopRelay.Tools.HttpRequest(url,CoopRelay.Tools.HttpRequest.HttpMethod.POST);
        //    r.Add("code", code);
        //    r.Add("client_id",ClientId);
        //    r.Add("client_secret", ClientSecret);
        //    r.Add("redirect_uri", "https://outofthecoop.com/googleprovider");
        //    r.Add("grant_type", "authorization_code");

        //    var response = r.Send();
        //    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);

        //    return data["refresh_token"];
        //}

        //public static string GetAccessToken(string refresh_token)
        //{
        //    string url = "https://www.googleapis.com/oauth2/v3/token";
        //    var r = new CoopRelay.Tools.HttpRequest(url, CoopRelay.Tools.HttpRequest.HttpMethod.POST);
        //    r.Add("client_id", ClientId);
        //    r.Add("client_secret", ClientSecret);
        //    r.Add("refresh_token", refresh_token);
        //    r.Add("grant_type", "refresh_token");
        //    var response = r.Send();
        //    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
        //    return data["access_token"];
        //}

        public static OauthMemberData Get(string accesstoken)
        {
            var url = string.Format("https://www.googleapis.com/plus/v1/people/me?access_token={0}",
                accesstoken);
            var r = new CoopRelay.Tools.HttpRequest(url);
            var response = r.Send();
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);

            // OauthMemberData
            var oauthmemberdata = new OauthMemberData
            {
                OauthId = data["id"],
                AccessToken = accesstoken,
                FirstName = data["name"]["givenName"],
                LastName = data["name"]["familyName"],
                Picture = SetImageSize(data["image"]["url"].ToString())
            };

            return oauthmemberdata;
        }

        private static String SetImageSize(string src)
        {
            if (string.IsNullOrEmpty(src))
                return string.Empty;

            return src.Split('?')[0] + "?sz=150";
        }
    }
}