using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using ITSpOAuth.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
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
                   
                }
            };

        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                 
                new Client() {
                    ClientId="mvcauthcode",
                    ClientName="Mvc med Authcode",
                    ClientSecrets=new List<Secret>() { new Secret("hemligt".Sha256())},
                    AllowAccessToAllScopes=true,
                    Flow=Flows.AuthorizationCode,
                    RedirectUris=new List<string> {
                        Constants.MVCAPPSTSCallback
                    }
                },
                 new Client() {
                    ClientId="mvcauthcode2",
                    ClientName="Mvc med Authcode2",
                    ClientSecrets=new List<Secret>() { new Secret("hemligt".Sha256())},
                    AllowAccessToAllScopes=true,
                    Flow=Flows.AuthorizationCode,
                    RedirectUris=new List<string> {
                        Constants.MVCAPPSTSCallback
                    }
                },
                new Client() {
                    ClientId="mvcclient",
                    ClientSecrets=new List<Secret>() { new Secret("hemligt".Sha256())},
                    AllowAccessToAllScopes=true,
                    Flow=Flows.ClientCredentials
                }
               
            };

        }

        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>()
            {
                IdentityServer3.Core.Models.StandardScopes.OpenId,
                IdentityServer3.Core.Models.StandardScopes.Profile,
                new Scope() {
                    Name="studentScope",
                    DisplayName="Studentdata",
                    Description="Standard scope för studentoperationer",
                },
            };

        }
    }
}