using System.Security.Claims;
using IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityService;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //NOTE: If we have user simply return
        if (userMgr.Users.Any()) return;
        var baris = userMgr.FindByNameAsync("baris").Result;
        if (baris == null)
        {
            baris = new ApplicationUser
            {
                UserName = "baris",
                Email = "barisozgen@test.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(baris, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(baris, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Baris Ozgen"),
                        }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("baris created");
        }
        else
        {
            Log.Debug("baris already exists");
        }

        var gokalp = userMgr.FindByNameAsync("gokalp").Result;
        if (gokalp == null)
        {
            gokalp = new ApplicationUser
            {
                UserName = "gokalp",
                Email = "GokalpSoycicek@test.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(gokalp, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(gokalp, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Gokalp Soycicek"),
                        }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("gokalp created");
        }
        else
        {
            Log.Debug("gokalp already exists");
        }
    }
}
