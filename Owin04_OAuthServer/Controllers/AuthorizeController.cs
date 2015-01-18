using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Owin04_OAuthServer.Controllers {

    public class AuthorizeController : Controller {

        public ActionResult Index() {
            if (Response.StatusCode != 200) {
                return View("Error");
            }
            var authentication = HttpContext.GetOwinContext().Authentication;
            var ticket = authentication.AuthenticateAsync(Startup.AuthenticationType).Result;
            var identity = ticket != null ? ticket.Identity : null;
            if (identity == null) {
                authentication.Challenge(Startup.AuthenticationType);
                return new HttpUnauthorizedResult();
            }

            var scopes = (Request.QueryString.Get("scope") ?? "").Split(' ');

            if (Request.HttpMethod == "POST") {
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Grant"))) {
                    identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType, identity.RoleClaimType);
                    foreach (var scope in scopes) {
                        identity.AddClaim(new Claim("urn:oauth:scope", scope));
                    }
                    authentication.SignIn(identity);
                }
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Login"))) {
                    authentication.SignOut(Startup.AuthenticationType);
                    authentication.Challenge(Startup.AuthenticationType);
                    return new HttpUnauthorizedResult();
                }
            }

            return View();
        }
    }
}