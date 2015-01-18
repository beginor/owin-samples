using System;
using System.Web.Mvc;
using System.Web;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace Owin04_OAuthServer.Controllers {

    [Authorize]
    public class AccountController : Controller {

        [AllowAnonymous]
        public ActionResult Login() {
            var authentication = HttpContext.GetOwinContext().Authentication;
            if (Request.HttpMethod == "POST") {
                var isPersistent = !string.IsNullOrEmpty(Request.Form.Get("isPersistent"));

                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Signin"))) {
                    authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = isPersistent },
                        new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, Request.Form["username"]) }, Startup.AuthenticationType)
                    );
                }
            }
            return View();
        }

        public ActionResult Logout() {
            return View();
        }

    }
}

