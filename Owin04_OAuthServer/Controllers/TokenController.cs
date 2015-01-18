using System.Web.Mvc;

namespace Owin04_OAuthServer.Controllers {

    public class TokenController : Controller {

        public ActionResult Index() {
            return new EmptyResult();
        }

    }
}