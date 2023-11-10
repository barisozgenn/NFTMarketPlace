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
            new Client
            {
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = {"openid", "profile", "nftAuctionApp"},
                RedirectUris = {"https://www.getpostman.com/oauth2/callback"},
                ClientSecrets = new[] {new Secret("NotASecretOnlyForDevBaris".Sha256())},
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
            }
            //after postman request you can see token detail in https://jwt.io
            // and then you see sub parameter basically we keep this parameter in our db to match with owner user
            // when you specify in CustomProfileService to see username and name you can see after that these details in json
        };
}
