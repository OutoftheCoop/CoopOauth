using System;
using System.Web.Mvc;

namespace CoopOauth.Controllers
{
    public class OauthSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
        [HttpPost]
        public ActionResult GoogleLogin(OauthRequest model, string returnUrl)
        {
            return Login(model, new GooglePlus.GooglePlusProvider(), returnUrl);
        }

        [HttpPost]
        public ActionResult FacebookLogin(OauthRequest model, string returnUrl)
        {
            return Login(model, new Facebook.FacebookProvider(), returnUrl);
        }

        public ActionResult Login(OauthRequest model, OauthProviderBase provider, string returnUrl)
        {
            try
            {
                var m = provider.Get(model);

                // Create/Update as needed
                OauthMemberRepository.Checkin(m);

                // Login
                System.Web.Security.FormsAuthentication.SetAuthCookie(m.Username, true);

                // Return
                return Content(GetLocalUrl(returnUrl));
            }
            catch (Exception ex)
            {
                return Content("An error occurred. " + ex.Message + ex.StackTrace + ex.InnerException);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }

        private String GetLocalUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }
            else
            {
                return "/";
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}
