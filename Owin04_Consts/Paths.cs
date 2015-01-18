using System;

namespace Owin04_Consts {

    public static class Paths {

        public const string ImplicitGrantCallBackPath = "http://localhost:8080/Owin04_OAuthClient/Home/Login";

        public const string AuthorizeCodeCallBackPath = "http://localhost:8080/";

        public const string ResourceUserApiPath = "http://localhost:8080/Owin04_OAuthResource/api/user";

        public const string AuthorizationServerBaseAddress = "http://localhost:8080/OWin04_OAuthServer";

        public const string LoginPath = "/Account/Login";
        public const string LogoutPath = "/Account/Logout";

        public const string AuthorizePath = "/Authorize";
        public const string TokenPath = "/Token";

    }
}

