using CalenderForFriends.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalenderForFriends.DatabaseContext
{
    public class CalenderContext : DbContext
    {
        public CalenderContext(DbContextOptions<CalenderContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<Invitations> Invitations { get; set; }
    }
}
