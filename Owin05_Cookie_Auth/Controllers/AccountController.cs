using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.Owin.Security.Cookies;

namespace Owin05_Cookie_Auth {

    public class AccountController : ApiController {

        public IHttpActionResult Get() {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "beginor"));
            claims.Add(new Claim(ClaimTypes.Email, "beginor@local.com"));
            var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);
            Request.GetOwinContext().Authentication.SignIn(id);
            //
            var queryStrings = Request.GetQueryNameValuePairs();
            if (queryStrings.Any(s => s.Key == CookieAuthenticationDefaults.ReturnUrlParameter)) {
                var returnUrl = queryStrings.First(s => s.Key == CookieAuthenticationDefaults.ReturnUrlParameter).Value;
                return Redirect(returnUrl);
            }
            return Ok();
        }
    }
}