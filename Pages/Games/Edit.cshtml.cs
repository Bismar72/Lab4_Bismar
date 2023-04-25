using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rp_ef_maria.Models;

namespace rp_ef_maria.Pages.Games
{
	public class EditModel : PageModel
	{
		private readonly StoreContext _context;
		private readonly ILogger<EditModel> _logger;

		public EditModel(StoreContext context, ILogger<EditModel> logger)
		{
			_context = context;
			_logger = logger;
		}

		[BindProperty]
		public Game Game { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(uint? id)
		{
			if (id == null || _context.Game == null)
			{
				return NotFound();
			}

			var game = await _context.Game.FirstOrDefaultAsync(m => m.GameId == id);
			if (game == null)
			{
				return NotFound();
			}
			Game = game;
			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				_logger.LogWarning("ModelState is invalid");
				foreach (var val in ModelState.Values.SelectMany(v => v.Errors).Select(e => (e.ErrorMessage, e.Exception)))
				{

					_logger.LogWarning($"ModelState[{val.ErrorMessage}] : {val.Exception}");
				}


				return Page();
			}

			_context.Attach(Game).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GameExists(Game.GameId))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool GameExists(uint id)
		{
			return (_context.Game?.Any(e => e.GameId == id)).GetValueOrDefault();
		}
	}
}
