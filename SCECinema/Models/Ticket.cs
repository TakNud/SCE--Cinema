using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCECinema.Models
{
    public class Ticket
    {
        // Properties
        // TicketID
        public int TicketID { get; set; }
        // Seat
        [Required(ErrorMessage = "Seat is required.")]
        public string Seat { get; set; }

        // Price
        [Display(Name = "Ticket Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal TicketPrice { get; set; }

        [Display(Name = "OldPrice")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public Decimal OldPrice { get; set; }


        // Navigation Properties
        // Order
        public virtual Order Order { get; set; }
        // Showing
        public virtual Showing Showing { get; set; }


    }
}