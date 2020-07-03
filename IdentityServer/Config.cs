using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config
    {
        /// <summary>
        /// 添加对OpenID Connect的支持
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), //必须要添加，否则报无效的scope错误
                new IdentityResources.Profile()
            };
        }
        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
           {
               new ApiResource("api1", "this is  api1"),
               new ApiResource("orderapi", "this is order api"),
               new ApiResource("productapi", "this is product api")
           };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
           {
                //自定义客户端
             new Client
                {
                    //客户端名称
                    ClientId = "client1",
                    //客户端访问方式：密码验证
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                     //AccessTokenLifetime=7200,//默认2小时
                    //用于认证的密码加密方式
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    //客户端有权访问的范围
                    AllowedScopes = {
                     "api1",IdentityServerConstants.StandardScopes.OpenId, //必须要添加，否则报403 forbidden错误
                  IdentityServerConstants.StandardScopes.Profile }
                },
            new Client
            {
                ClientId = "order",
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets =
                   {
                       new Secret("ordersecret".Sha256())
                   },

                AllowedScopes = { "orderapi" }
            },
                new Client
                {
                    ClientId = "product",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                   {
                       new Secret("productsecret".Sha256())
                   },

                    AllowedScopes = { "productapi" }
                }
           };
        }
    }
}
