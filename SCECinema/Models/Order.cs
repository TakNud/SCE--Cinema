﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCECinema.Models
{
    public enum OrderStatus { Pending, Complete, Cancelled }

    public class Order
    {
        private const Decimal TAX_RATE = 0.0825m;

        // Properties
        // OrderID

        public Int32 OrderID { get; set; }

        public OrderStatus Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Subtotal
        {
            get
            {
                return Tickets.Sum(t => t.TicketPrice);
            }
        }

        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        // Tickets
        public virtual List<Ticket> Tickets { get; set; }
        // Credit Card
        //public virtual CreditCard CreditCard { get; set; }

        //AppUser
        public virtual AppUser AppUser { get; set; }

        public Order()
        {
            if (Tickets == null)
            {
                Tickets = new List<Ticket>();
            }
        }
    }
}