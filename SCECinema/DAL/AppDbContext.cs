using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCECinema.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SCECinema.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) { }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
        
        public DbSet<Movie> Movies { get; set; }
       
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Showing> Showings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Genre> Genres { get; set; }

    }

}