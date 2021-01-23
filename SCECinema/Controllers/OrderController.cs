using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCECinema.DAL;
using SCECinema.Models;
using Microsoft.AspNet.Identity;

namespace SCECinema.Controllers
{
    public class OrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Orders
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                return View(db.Orders.ToList());
            else if (User.IsInRole("Register"))
            {
                string UserID = User.Identity.GetUserId();
                List<Order> Orders = db.Orders.Where(o => o.AppUser.Id == UserID).ToList();
                return View(Orders);
            }   
            
            else return Content("Only registered useres can watch their orders!");
        }

        // GET: Orders/Details/5
        [Authorize]
        public ActionResult Details(int OrderID)
        {
            Order order = db.Orders.Find(OrderID);
            if (order == null)
            {
                return HttpNotFound();
            }

            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                return View(order);
            else
            {
                if (order.AppUser.Id == User.Identity.GetUserId())
                    return View(order);
                else
                    return View("Error", new string[] { "This is not your Order!" });
            }
        }

        // GET: Orders/Create
        [Authorize]
        public ActionResult Create(int TicketID)
        {
           
            Order ord = new Order();
            Ticket ticket = db.Tickets.Find(TicketID);
            ord.Tickets.Add(ticket);
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,ConfirmationCode,Complete,Subtotal,TaxAmount,Total,OrderDate")] Order order, Int32 TicketID)
        {
            // Add first ticket to order
            Ticket ticket = db.Tickets.Find(TicketID);
            order.Tickets.Add(ticket);

            //Record date of order
            order.OrderDate = DateTime.Today;
            order.Status = OrderStatus.Pending;

          

            AppUser user = db.Users.Find(User.Identity.GetUserId());
            order.AppUser = user;

            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Details", "Orders", new { OrderID = order.OrderID });
            }
            
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                return View(order);
            else
            {
                if (order.AppUser.Id == User.Identity.GetUserId())
                    return View(order);
                else
                    return View("Error", new string[] { "This is not your Order!" });
            }

        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "OrderID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                return View(order);
            else
            {
                if (order.AppUser.Id == User.Identity.GetUserId())
                    return View(order);
                else
                    return View("Error", new string[] { "This is not your Order!" });
            }
        }

        // GET: Orders/Cancel/5
        [Authorize]
        public ActionResult Cancel(int OrderID)
        {
         
            Order order = db.Orders.Find(OrderID);
            if (order == null)
            {
                return HttpNotFound();
            }

            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                return View(order);
            else
            {
                if (order.AppUser.Id == User.Identity.GetUserId())
                    return View(order);
                else
                    return View("Error", new string[] { "This is not your Order!" });
            }
            
        }

        // POST: Orders/Cancel/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CancelConfirmed(int OrderID)
        {
            AppUser user = db.Users.Find(User.Identity.GetUserId());
            Order order = db.Orders.Find(OrderID);

            Boolean EligbleForCancellation = true;
            foreach (Ticket t in order.Tickets)
            {
                if (DateTime.Now + new TimeSpan(1, 0, 0) > t.Showing.StartTime)
                {
                    EligbleForCancellation = false;
                }
            }

            if (EligbleForCancellation)
            {
                order.Status = OrderStatus.Cancelled;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

     
                if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                    return RedirectToAction("Index", "Orders");
                else
                {
                    if (order.AppUser.Id == User.Identity.GetUserId())
                        return RedirectToAction("Index", "Orders");
                    else
                        return View("Error", new string[] { "This is not your Order!" });
                }
            }
            else
            {
                return View("Error", new string[] { "It is too late to cancel this order since it contains a showing that starts in the next hour." });
            }


        }

        // GET: Orders/Checkout/ID
        public ActionResult Checkout(int? OrderID)
        {
            if (OrderID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(OrderID);
            if (order == null)
            {
                
                return HttpNotFound();
            }
            else if (order.Tickets.Count() == 0)
            {
                return View("Error", new string[] { "You must have at least one ticket in your order to checkout." });
            }
            else
            {
                if (order.AppUser.Id == User.Identity.GetUserId())
                {
                   // ViewBag.AllCreditCards = GetCreditCards();
                    return View(order);
                }
                else
                {
                    return View("Error", new string[] { "This is not your Order!" });
                }
            }
        }
        
        // POST: Orders/Checkout/ID
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "OrderID,CreditCard")] String CreditCardInput, Int32 OrderID)
        {
            String UserID = User.Identity.GetUserId();

            Order order = db.Orders.Find(OrderID);


            
            if (CreditCardInput.Length == 16 || CreditCardInput.Length == 15)
            {
             

               if (ModelState.IsValid)
                {
                 
                    db.SaveChanges();
                    return RedirectToAction("Confirm", "Orders", new { OrderID = order.OrderID });
                }
            }
            else return View("Error", new string[] { "An error occurred with the credit card." });

           
     
            return View();
        }
        // GET: Orders/Confirm/ID
        public ActionResult Confirm(int? OrderID)
        {
            if (OrderID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(OrderID);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Confirm/ID
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm([Bind(Include = "OrderID")] Int32 OrderID)
        {
            Order order = db.Orders.Find(OrderID);
            order.Status = OrderStatus.Complete;
            AppUser user = db.Users.Find(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                Utilities.EmailMessaging.SendEmail(user.Email, "SCE-Cinema: Order Confirmation",
                      "Thanks for ordering with us!\n" +
                        "You invite: "   + order.Tickets.Count() + " tickets for a total of $" + order.Subtotal + " was completed. ");

                return RedirectToAction("Thanks", "Orders", new { OrderID = order.OrderID });
            }
            return View(order);
        }

        // GET: Orders/Thanks/ID
        public ActionResult Thanks(int? OrderID)
        {
            if (OrderID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(OrderID);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
