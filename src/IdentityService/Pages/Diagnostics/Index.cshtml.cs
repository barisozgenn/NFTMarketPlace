using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace IdentityService.Pages.Diagnostics;

[SecurityHeaders]
[Authorize]
public class Index : PageModel
{
    public ViewModel View { get; set; }
        
    public async Task<IActionResult> OnGet()
    {
        //NOTE: we need to debug the remote ip address in docker for identity server to add
        // we found ot that docker use this ::ffff:192.168.65.1 REMOTE IP for run our IdentityService app on that ip
        // and now we need to rebuild this image to see changes we did docker compose build identity-service
        var localAddresses = new string[] {"::ffff:192.168.65.1","127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress.ToString() };
        //We add here to find out what our LocalIpAddress is
        if (!localAddresses.Contains(HttpContext.Connection.RemoteIpAddress.ToString()))
        {
            return NotFound();
        }

        View = new ViewModel(await HttpContext.AuthenticateAsync());
            
        return Page();
    }
}