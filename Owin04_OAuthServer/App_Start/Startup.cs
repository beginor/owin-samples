using System;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using Owin04_Consts;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;

namespace Owin04_OAuthServer {

    partial class Startup {

        public void ConfigureAuth(IAppBuilder app) {
            // Enable the Application Sign in cookie.
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = "Application",
                AuthenticationMode = AuthenticationMode.Passive,
                LoginPath = new PathString(Paths.LoginPath),
                LogoutPath = new PathString(Paths.LogoutPath)
            });
            // Enable the external sign in cookie.
            app.SetDefaultSignInAsAuthenticationType("External");
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = "External",
                AuthenticationMode.Passive,
                CookieName = CookieAuthenticationDefaults.CookiePrefix + "External",
                ExpireTimeSpan = TimeSpan.FromMinutes(5)
            });
            // enable google auth
            app.UseGoogleAuthentication("", "");
            // setup auth server
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions {
                AuthorizeEndpointPath = new PathString(Paths.AuthorizePath),
                TokenEndpointPath = new PathString(Paths.TokenPath),
                ApplicationCanDisplayErrors = true,
                AllowInsecureHttp = true,
                Provider = new OAuthAuthorizationServerProvider {
                    OnValidateClientRedirectUri = ValidateClientRedirectUri,
                    OnValidateClientAuthentication = ValidateClientAuthentication,
                    OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials,
                    OnGrantClientCredentials = GrantClientCredentials
                },
                AuthorizationCodeProvider = new AuthenticationTokenProvider {
                    OnCreate = CreateAuthenticationCode,
                    OnReceive = ReceiveAuthenticationCode
                },
                RefreshTokenProvider = new AuthenticationTokenProvider {
                    OnCreate = CreateRefreshToken,
                    OnReceive = ReceiveRefreshToken
                }
            });
        }

        Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context) {
            if (context.ClientId == Clients.Client1.Id) {
                context.Validated(Clients.Client1.RedirectUrl);
            }
            else if (context.ClientId == Clients.Client2.Id) {
                context.Validated(Clients.Client2.RedirectUrl);
            }
            return Task.FromResult(0);
        }

        Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) {
            string clientId, clientSecret;
            if (context.TryGetBasicCredentials(out clientId, out clientSecret) ||
                context.TryGetFormCredentials(out clientId, out clientSecret)) {
                if (clientId == Clients.Client1 && clientSecret == Clients.Client1.Secret) {
                    context.Validated();
                }
                else if (clientId == Clients.Client2.Id && clientSecret == Clients.Client2.Secret) {
                    context.Validated();
                }
            }
            return Task.FromResult(0);
        }

        Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext arg) {

        }

        Task GrantClientCredentials(OAuthGrantClientCredentialsContext arg) {

        }

        void CreateAuthenticationCode(AuthenticationTokenCreateContext obj) {

        }

        void ReceiveAuthenticationCode(AuthenticationTokenReceiveContext obj) {

        }

        void CreateRefreshToken(AuthenticationTokenCreateContext obj) {

        }

        void ReceiveRefreshToken(AuthenticationTokenReceiveContext obj) {

        }
    }
}

