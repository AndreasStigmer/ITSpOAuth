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
                    Claims=new List<Claim>
                    {
                        new Claim("given_name","Andreas"),
                        new Claim("family_name","Stigmer"),
                        new Claim("email","andreas@campusi12.se"),
                        new Claim("email","andreas.stigmer@outlook.com")

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
                new Client() {
                    ClientId="mvcopenid",
                    ClientSecrets=new List<Secret>() { new Secret("hemligt".Sha256())},
                    AllowAccessToAllScopes=true,
                    Flow=Flows.Hybrid,
                    RedirectUris=new List<string> {
                        Constants.MvcOpenIdCallback
                    }


                }

            };

        }

        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>()
            {
                IdentityServer3.Core.Models.StandardScopes.OpenId,
                IdentityServer3.Core.Models.StandardScopes.Profile,
                IdentityServer3.Core.Models.StandardScopes.Email,
                new Scope() {
                    Name="studentScope",
                    Description="Standard scope för studentoperationer",
                },
            };

        }
    }
}