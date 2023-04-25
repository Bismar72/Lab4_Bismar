using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using rp_ef_maria.Models;

namespace rp_ef_maria.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private readonly StoreContext _context;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(StoreContext context, ILogger<DetailsModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Game Game { get; set; } = default!;



        [BindProperty(SupportsGet = true)]
        public uint? GameId { get; set; }

        [BindProperty(SupportsGet = true)]
        public uint? StarRating { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Comment { get; set; } = "";



        [BindProperty(SupportsGet = true, Name = "msg")]
        public string Message { get; set; } = default!;



        public async Task<IActionResult> OnGetAsync(uint? id)
        {

            if (id == null && GameId != null) // if the rating form is submitted
            {
                id = GameId;
            }

            if (id == null || _context.Game == null)
            {
                return NotFound();
            }


            var game = await _context.Game.FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }
            else
            {

                if (StarRating != null) // if rating form is submitted
                {
                    var Rating = new Rating
                    {
                        Game = game,
                        Comment = Comment,
                        StarRating = (uint)StarRating
                    }; // new rating object

                    _context.Rating.Add(Rating);
                    await _context.SaveChangesAsync();
                    Message = "Rating added";
                }

                GameId = game.GameId;
                Game = game;
            }
            return Page();
        }
    }
}
