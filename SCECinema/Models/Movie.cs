using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using SCECinema.DAL;

namespace SCECinema.Models
{
    public enum MPAARating { G, PG, R, Unrated, PG13, NC17, None };

    public class Movie
    {
        private AppDbContext db = new AppDbContext();

        // Properties
        // MovieID
        public int Id { get; set; }

        // Title
        [Display(Name = "Movie Title")]
        public string Title { get; set; }

        

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        // Overview
        [Display(Name = "Movie Synopsis")]
        public string Synopsis { get; set; }


        // Runtime (in minutes)
        [Display(Name = "Duration in minutes")]
        public int Duration { get; set; }


        [Display(Name = "Age Limit")]
        public int AgeLimit { get; set; }

        [Display(Name = "Movie Poster")]
        [DataType(DataType.Url)]
        public string MovieImage { get; set; }

        // Showings
        public virtual List<Showing> Showing { get; set; }

      

    }
  
}