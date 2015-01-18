using System.Web.Mvc;

namespace Owin04_OAuthClient.Controllers {

    public class ImplicitController : Controller {

        public ActionResult Index() {
            return View();
        }

        public ActionResult Login() {
            return View();
        }
    }
}