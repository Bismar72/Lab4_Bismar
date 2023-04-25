using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rp_ef_maria.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class StoreContext : IdentityDbContext<ApplicationUser>
{
    public StoreContext(DbContextOptions<StoreContext> options)
         : base(options)
    {
    }

    public DbSet<rp_ef_maria.Models.Game> Game { get; set; } = default!;
    public DbSet<rp_ef_maria.Models.Rating> Rating { get; set; } = default!;
}
