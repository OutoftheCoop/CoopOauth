using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoopOauth.Facebook
{
    public class FacebookGraphApi
    {
        public static string GetLongTermAccessToken(string accesstoken, string appid, string appsecret)
        {
            string url = string.Format("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id={0}&client_secret={1}&fb_exchange_token={2}",
                appid,
                appsecret,
                accesstoken);
            var r = new CoopRelay.Tools.HttpRequest(url);
            var result = r.Send();
            var nvc = HttpUtility.ParseQueryString(result);
            return nvc.Get("access_token");
        }

        public static OauthMemberData Get(string accesstoken)
        {
            var url = string.Format("https://graph.facebook.com/me?access_token={0}",
                accesstoken);
            var r = new CoopRelay.Tools.HttpRequest(url);
            var response = r.Send();
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);

            // User
            var oauthmemberdata = new OauthMemberData
            {
                OauthId = data["id"],
                AccessToken = accesstoken,
                FirstName = data["first_name"],
                LastName = data["last_name"]
            };

            // Picture
            url = string.Format("https://graph.facebook.com/me/picture?redirect=false&height=200&width=200&access_token={0}",
                accesstoken);
            r = new CoopRelay.Tools.HttpRequest(url);
            response = r.Send();
            data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
            oauthmemberdata.Picture = data["data"]["url"];

            return oauthmemberdata;
        }
    }
}