using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_VSE_CSS475.Models;
using System.Security.Permissions;

namespace Final_VSE_CSS475.Controllers
{
    public class PerformancesController : Controller
    {
        private db_VSE_FinalEntities db = new db_VSE_FinalEntities();

        // GET: 
        public ActionResult Index(string name, string artist)
        {
            // rertieve all objects from database
            var perfomanceList = from m in db.Performances
                             select m;

            // check if  name was provided for filter
            if (!String.IsNullOrEmpty(name))
            {
                // retrieve all perfomance with given name
                perfomanceList = perfomanceList.Where(s => s.PerformanceName.Contains(name));
            }
            // check if  artist was provided for filter
            if (!String.IsNullOrEmpty(artist))
            {
                // retrieve all perfomance with given artist
                perfomanceList = perfomanceList.Where(s => s.Artist.Contains(artist));
            }

            return View(perfomanceList.ToList());
        }

        // GET: Performances/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // GET: Performances/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Artist = new SelectList(db.Artists, "ArtistName", "ArtistName");
            ViewBag.Venue = new SelectList(db.Venues, "VenueName", "VenueName");
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "PerformanceName,Artist,Venue,Description,Quantity,TimeStarts,TimeEnds")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Performances.Add(performance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Artist = new SelectList(db.Artists, "ArtistName", "ArtistName", performance.Artist);
            ViewBag.Venue = new SelectList(db.Venues, "VenueName", "VenueName", performance.Venue);
            return View(performance);
        }

        // GET: Performances/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            ViewBag.Artist = new SelectList(db.Artists, "ArtistName", "ArtistName", performance.Artist);
            ViewBag.Venue = new SelectList(db.Venues, "VenueName", "VenueName", performance.Venue);
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "PerformanceName,Artist,Venue,Description,Quantity,TimeStarts,TimeEnds")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Artist = new SelectList(db.Artists, "ArtistName", "ArtistName", performance.Artist);
            ViewBag.Venue = new SelectList(db.Venues, "VenueName", "VenueName", performance.Venue);
            return View(performance);
        }

        // GET: Performances/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(string id)
        {
            Performance performance = db.Performances.Find(id);
            db.Performances.Remove(performance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
        public ActionResult Purchase(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // retrieve the row

            var selectedPerf = db.Performances.Find(id);
            //first check if there are avalible tickets
            if (selectedPerf != null)
            {
                if (selectedPerf.Quantity > 0)
                {
                    return View(selectedPerf);
                }
                else
                {
                    ModelState.AddModelError("", "Sorry all ticket are sold.");
                }

            }
            return View(selectedPerf);
        }


        [HttpPost, ActionName("Purchase")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult PurchaseConFirmed(string id)
        {
            // update the ticket quanitiy
            Performance performance = db.Performances.Find(id);
            int? temp = performance.Quantity - 1;
            performance.Quantity = temp;
            db.Entry(performance).State = EntityState.Modified;
            db.SaveChanges();

            // create ticket
            Ticket new_ticket = new Ticket
            {
                PerformanceName = id,
                Price = 60,
                TicketNumber = 0
            };

            db.Tickets.Add(new_ticket);
            db.SaveChanges();

            // retrieve user
            var login = User.Identity.Name;
            User my_user = db.Users.FirstOrDefault(s => s.LoginName == login);

            // validate current user instance
            if (my_user != null)
            {
                UserHistory history = new UserHistory();
                history.TicketID = new_ticket.TicketId;
                history.PerformanceName = id;
                history.userID = my_user.UserId;

                db.UserHistories.Add(history);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}
