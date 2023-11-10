using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("nftAuctionApp","NFT Auction App full access"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            //after postman request you can see token detail in https://jwt.io
            // and then you see sub parameter basically we keep this parameter in our db to match with owner user
            // when you specify in CustomProfileService to see username and name you can see after that these details in json
            new Client
            {
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = {"openid", "profile", "nftAuctionApp"},
                RedirectUris = {"https://www.getpostman.com/oauth2/callback"},
                ClientSecrets = new[] {new Secret("NotASecretOnlyForDevBaris".Sha256())},
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
            },
            new Client
            {
                ClientId = "nextApp",//for our next JS app
                ClientName = "nextApp",
                ClientSecrets = {new Secret("secret".Sha256())},//simple secret defined, it is ok for development stage
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RequirePkce = false,//react apps we need to use Pkce because we would not be able to store a secret in a React Native app.
                RedirectUris = {"http://localhost:3000/api/auth/callback/id-server"},
                AllowOfflineAccess = true,//we can enable refresh token functionality
                AllowedScopes = {"openid", "profile", "nftAuctionApp"},
                AccessTokenLifetime = 3600*24*30//we defined for a month and longer than its default for development stage
            }
        };
}
