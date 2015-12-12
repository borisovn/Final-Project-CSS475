using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_VSE_CSS475.Models;

namespace Final_VSE_CSS475.Controllers
{
    public class UserHistoriesController : Controller
    {
        private db_VSE_FinalEntities db = new db_VSE_FinalEntities();

        // GET: UserHistories
        
        public ActionResult Index()
        {
            var userHistories = db.UserHistories.Include(u => u.Performance).Include(u => u.Ticket).Include(u => u.User);
            return View(userHistories.ToList());
        }

        // GET: UserHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserHistory userHistory = db.UserHistories.Find(id);
            if (userHistory == null)
            {
                return HttpNotFound();
            }
            return View(userHistory);
        }

        // GET: UserHistories/Create
        public ActionResult Create()
        {
            ViewBag.PerformanceName = new SelectList(db.Performances, "PerformanceName", "PerformanceName");
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketId", "TicketNumber");
            ViewBag.userID = new SelectList(db.Users, "UserId", "LoginName");
            return View();
        }

        // POST: UserHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserHistroryId,userID,TicketID,PerformanceName")] UserHistory userHistory)
        {
            if (ModelState.IsValid)
            {
                db.UserHistories.Add(userHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PerformanceName = new SelectList(db.Performances, "PerformanceName", "PerformanceName", userHistory.PerformanceName);
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketId", "TicketNumber", userHistory.TicketID);
            ViewBag.userID = new SelectList(db.Users, "UserId", "LoginName", userHistory.userID);
            return View(userHistory);
        }

        // GET: UserHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserHistory userHistory = db.UserHistories.Find(id);
            if (userHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.PerformanceName = new SelectList(db.Performances, "PerformanceName", "PerformanceName", userHistory.PerformanceName);
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketId", "TicketNumber", userHistory.TicketID);
            ViewBag.userID = new SelectList(db.Users, "UserId", "LoginName", userHistory.userID);
            return View(userHistory);
        }

        // POST: UserHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserHistroryId,userID,TicketID,PerformanceName")] UserHistory userHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PerformanceName = new SelectList(db.Performances, "PerformanceName", "PerformanceName", userHistory.PerformanceName);
            ViewBag.TicketID = new SelectList(db.Tickets, "TicketId", "TicketNumber", userHistory.TicketID);
            ViewBag.userID = new SelectList(db.Users, "UserId", "LoginName", userHistory.userID);
            return View(userHistory);
        }

        // GET: UserHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserHistory userHistory = db.UserHistories.Find(id);
            if (userHistory == null)
            {
                return HttpNotFound();
            }
            return View(userHistory);
        }

        // POST: UserHistories/Delete/5
        // cancel ticket:
        // delete history for the ticket
        // as well as the ticket
        // and give back +1 quanitiy for performance
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserHistory userHistory = db.UserHistories.Find(id);
            // retrive perfomrance
            // and updated the quantity
            var performance = db.Performances.Find(userHistory.PerformanceName);
            int? temp = performance.Quantity + 1;
            performance.Quantity = temp;
            db.Entry(performance).State = EntityState.Modified;

            //remove ticket and user history
            db.Tickets.Remove(db.Tickets.Find(userHistory.TicketID));
            db.UserHistories.Remove(userHistory);

            //updated the db
            db.SaveChanges();
            return RedirectToAction("MyHistory");
        }


        /// <summary>
        ///  need to move to UserHistory contorller 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult MyHistory()
        {

            // get current user
            var login = User.Identity.Name;
            User my_user = db.Users.FirstOrDefault(s => s.LoginName == login);


            var userHistories = from m in db.UserHistories
                                select m;

            userHistories = userHistories.Where(s => s.userID.Equals(my_user.UserId));

            return View(userHistories.ToList());
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
