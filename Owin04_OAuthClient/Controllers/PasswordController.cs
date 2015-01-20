using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using DotNetOpenAuth.OAuth2;
using Owin04_Consts;

namespace Owin04_OAuthClient.Controllers {

    public class PasswordController : Controller {

        [HttpGet]
        public ActionResult Index() {
            ViewBag.Username = "";
            ViewBag.Password = "";
            ViewBag.ApiResult = "";
            ViewBag.AccessToken = "";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string username, string password) {
            ViewBag.Username = username;
            ViewBag.Password = password;
            ViewBag.ApiResult = "";
            ViewBag.AccessToken = "";

            if (!string.IsNullOrEmpty(username)) {
                var authServer = new AuthorizationServerDescription {
                    AuthorizationEndpoint = new Uri(Paths.AuthorizationServerBaseAddress + Paths.AuthorizePath),
                    TokenEndpoint = new Uri(Paths.AuthorizationServerBaseAddress + Paths.TokenPath)
                };
                var webServerClient = new WebServerClient(authServer, Clients.Client1.Id, Clients.Client1.Secret);

                var state = webServerClient.ExchangeUserCredentialForToken(username, password, new[] {"test1", "test2", "test3"});
                var token = state.AccessToken;
                ViewBag.AccessToken = token;

                var client = new HttpClient(webServerClient.CreateAuthorizingHandler(token));
                var apiResult = await client.GetStringAsync(Paths.ResourceUserApiPath);

                ViewBag.ApiResult = apiResult;
            }
            return View();
        }

    }
}