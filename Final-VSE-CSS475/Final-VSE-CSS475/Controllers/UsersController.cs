using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_VSE_CSS475.Models;
using System.Web.Security;

namespace Final_VSE_CSS475.Controllers
{
    public class UsersController : Controller
    {
        private db_VSE_FinalEntities db = new db_VSE_FinalEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "LastName,FirstName,LoginName,Password,email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "UserId,LastName,FirstName,LoginName,Password,email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {

            if (ModelState.IsValid)
                if (isValid(user.LoginName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.LoginName, false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Data is incorrect.");
                }
            return View(user);
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration([Bind(Include = "LastName,FirstName,LoginName,Password,email")] User user)
        {

            if (ModelState.IsValid)
            {

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(user);
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

        private bool isValid(string login, string password)
        {
            //var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;

            var myuser = db.Users.FirstOrDefault(u => u.LoginName == login);

            if (myuser != null)
            {
                if (myuser.Password == password)
                {
                    isValid = true;
                }
            }

            return isValid;
        }
    }
}
