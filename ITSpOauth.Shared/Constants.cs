using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSpOAuth.Shared
{
    /// <summary>
    /// Konstanter för att enklare hålla reda på alla URIer
    /// </summary>
    /// 
    public static class Constants
    {
        //Authserver endpoints
        public const string IssuerURI = "https://userprofileSTS:44300/";
        public const string UserProfileSTSOrigin = "https://localhost:44300";
        public const string UserProfileSTS = UserProfileSTSOrigin+"/identity";
        public const string UserProfileSTSTokenEndpoint = UserProfileSTS + "/connect/token";
        public const string UserProfileSTSAuthorizeEndpoint = UserProfileSTS + "/connect/authorize";

        //Api endpoints
        public const string UserProfileAPI = "http://localhost:44719/";

        //MvcEndpoints
        public const string MvcAuthCodeCallback = "http://localhost:44744/Callback";
    }
}
