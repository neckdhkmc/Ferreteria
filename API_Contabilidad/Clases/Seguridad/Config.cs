using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Contabilidad.Clases.Seguridad
{
    public static class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
        {
           new Client
        {
            ClientId = "XAXX010101000",
            ClientSecrets = { new Secret("123456".Sha256()) },
            AllowedGrantTypes = { GrantType.ClientCredentials }, // Permitir el flujo de Resource Owner Password Credentials
            AllowedScopes = { "api1" },
        }
        };

        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
    {
        new ApiResource("api1", "My API")
        {
            Scopes = { "api1" } // Asignar el alcance "api1" al recurso "api1"
        }
    };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
    {
        new ApiScope("api1", "My API")
    };
        }



        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
        }
    }
}
