using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using presidio.Models;

namespace presidio.Repository
{
    public class ContactCenterDbContext : DbContext
    {
        public DbSet<Agents> Agents { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamMembers> TeamMembers { get; set; }

        public ContactCenterDbContext(DbContextOptions<ContactCenterDbContext> options) : base(options) { }

    }
}
