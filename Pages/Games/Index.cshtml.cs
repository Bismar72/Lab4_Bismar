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
  public class IndexModel : PageModel
  {
    private readonly StoreContext _context;

    public IndexModel(StoreContext dbcontext)
    {
      _context = dbcontext;
    }

    public IList<Game> Game { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string Query { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public bool Enabled { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime SelectedDate { get; set; } = DateTime.Today;

    [BindProperty(SupportsGet = true)]
    public string Before { get; set; } = "before";

    public async Task OnGetAsync()
    {
      IQueryable<Game> games;
      if (Enabled)
      {// story games query
        if (Query != null)
        {
          // if title query is not empty, search for titles that contain the query
          games = _context.Game.Where(g => g.Title.Contains(Query));
        }
        else
        {
          // otherwise, get all games
          games = _context.Game;
        }

        // add to query (further filter) to get games released in the last 5 years
        if (Before == "before")
        {
          games = games.Where(g => g.ReleaseDate < SelectedDate);
        }
        else
        {
          games = games.Where(g => g.ReleaseDate >= SelectedDate);
        }
      }
      else
      {
        games = _context.Game;
      }
      // do the query, staore in a list (do it asynchronously, so other program segments can run)
      Game = await games.ToListAsync();
      // render the page
      Page();

    }

  }
}
