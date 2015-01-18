using System.Web.Http;

namespace Owin04_OAuthResource.Controllers {

    [Authorize]
    public class UserController : ApiController {

        public string Get() {
            return User.Identity.Name;
        }
    }
}