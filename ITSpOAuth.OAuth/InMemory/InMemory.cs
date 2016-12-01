using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using ITSpOAuth.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ITSpOAuth.OAuth
{
    public static class InMemory
    {
        public static List<InMemoryUser> GetUsers() {
            return new List<InMemoryUser>() {
                new InMemoryUser() {
                    Subject="andreas@campusi12.se",
                    Username="andreas",
                    Password="hemligt",
                    //Lista med userns Claims
                    Claims=new List<Claim>
                    {
                        //Claimsen nedan ingår i vissa förutbestämda
                        //Scopes och lämnas bara ut om klienten fått tillgång til dem
                        new Claim("given_name","Andreas"),
                        new Claim("family_name","Stigmer"),
                        new Claim("email","andreas@campusi12.se"),
                        new Claim("email","andreas.stigmer@outlook.com"),
                        new Claim("role","admin")  
                        

                    }
                }
            };

        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client() {
                    ClientId="mvcclientcredentials",
                    ClientSecrets=new List<Secret>() { new Secret("hemligt".Sha256())},
                    AllowAccessToAllScopes=true,
                    Flow=Flows.ClientCredentials
                },
                new Client() {
                    ClientId="mvcauthcode",
                    ClientSecrets=new List<Secret>() { new Secret("hemligt".Sha256())},
                    AllowAccessToAllScopes=true,
                    Flow=Flows.AuthorizationCode,
                    RedirectUris=new List<string> {
                        Constants.MvcAuthCodeCallback
                    }
                    

                },

                //OPenIdConnect klient
                new Client() {
                    ClientId="mvcopenid",
                    ClientSecrets=new List<Secret>() { new Secret("hemligt".Sha256())},
                    AllowAccessToAllScopes=true, //Klienten får begära tillgång till samtliga scopes i scopeslistan
                    Flow=Flows.Hybrid,
                    RedirectUris=new List<string> {
                        //Callback som används för att skicka authcode i början av flödet
                        Constants.MvcOpenIdCallback
                    }
                   
                    


                }

            };

        }

        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>()
            {
                //Följande scopes är Identityscopes och
                //kommer ingå i id_token. Claims tillhörande
                //dessa scopes begärs ut från identityServerns UserInfo endpoint
                IdentityServer3.Core.Models.StandardScopes.OpenId,
                IdentityServer3.Core.Models.StandardScopes.Profile,
                IdentityServer3.Core.Models.StandardScopes.Email,
                IdentityServer3.Core.Models.StandardScopes.Roles,

                //Detta är ett resource scope som bara kommer finnas med
                //i access_token (Type=ScopeType.Resource). id_token innehåller bara scopes
                //av typen Identity
                new Scope() {
                    Name="studentScope",
                    DisplayName="StudentScope",
                    Type =ScopeType.Resource,
                    Description="Standard scope för studentoperationer",
                    Claims=new List<ScopeClaim> {
                        new ScopeClaim("role",false)    //gör att alla roleclaims även ingår i access_token
                    }
                },
            };

        }
    }
}