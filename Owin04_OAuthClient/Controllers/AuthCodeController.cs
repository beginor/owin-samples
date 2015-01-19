using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OAuth2;
using Owin04_Consts;

namespace Owin04_OAuthClient.Controllers {

    public class AuthCodeController : Controller {

        private WebServerClient webServerClient;

        // GET: AuthCode
        public ActionResult Index() {
            ViewBag.AccessToken = Request.Form["AccessToken"] ?? "";
            ViewBag.RefreshToken = Request.Form["RefreshToken"] ?? "";
            ViewBag.Action = "";
            ViewBag.ApiResponse = "";

            InitializeWebServerClient();

            var accessToken = Request.Form["AccessToken"];
            if (string.IsNullOrEmpty(accessToken)) {
                var authorizationState = webServerClient.ProcessUserAuthorization(Request);
                if (authorizationState != null) {
                    ViewBag.AccessToken = authorizationState.AccessToken;
                    ViewBag.RefreshToken = authorizationState.RefreshToken;
                    ViewBag.Action = Request.Path;
                }
            }
            if (!string.IsNullOrEmpty(Request.Form.Get("submit.Authorize"))) {
                var userAuthorization = webServerClient.PrepareRequestUserAuthorization(new[] { "bio", "notes" });
                userAuthorization.Send(HttpContext);
                Response.End();
            }
            else if (!string.IsNullOrEmpty(Request.Form.Get("submit.Refresh"))) {
                var state = new AuthorizationState {
                    AccessToken = Request.Form["AccessToken"],
                    RefreshToken = Request.Form["RefreshToken"]
                };
                if (webServerClient.RefreshAuthorization(state)) {
                    ViewBag.AccessToken = state.AccessToken;
                    ViewBag.RefreshToken = state.RefreshToken;
                }
            }
            else if (!string.IsNullOrEmpty(Request.Form.Get("submit.CallApi"))) {
                var client = new HttpClient(webServerClient.CreateAuthorizingHandler(accessToken));
                var body = client.GetStringAsync(new Uri(Paths.ResourceUserApiPath)).Result;
                ViewBag.ApiResponse = body;
            }
            return View();
        }

        private void InitializeWebServerClient() {
            var authServer = new AuthorizationServerDescription {
                AuthorizationEndpoint = new Uri(Paths.AuthorizationServerBaseAddress + Paths.AuthorizePath),
                TokenEndpoint = new Uri(Paths.AuthorizationServerBaseAddress + Paths.TokenPath)
            };
            webServerClient = new WebServerClient(authServer, Clients.Client1.Id, Clients.Client1.Secret);
        }
    }
}