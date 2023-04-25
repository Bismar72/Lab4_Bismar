using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using rp_ef_maria.Models;

namespace rp_ef_maria.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly StoreContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(StoreContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var id = (ClaimsIdentity)User.Identity; // get the claims identity of the user
                var email = id.FindFirst(ClaimTypes.Email)?.Value;

                _logger.LogInformation($"user with email {email} is on the create game page");
            }

            return Page();
        }

        [BindProperty]
        public Game Game { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {


                if (!ModelState.IsValid || _context.Game == null || Game == null)
                {
                    return Page();
                }

                _context.Game.Add(Game);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("./Index");
            }
        }
    }
}
