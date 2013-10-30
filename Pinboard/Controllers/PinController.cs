using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinboard.Models;
using WebMatrix.WebData;
using Pinboard.Filters;

namespace Pinboard.Controllers
{
    [InitializeSimpleMembership]
    public class PinController : Controller
    {
        private PinboardEntities db = new PinboardEntities();

        //
        // GET: /Pin/

        public ActionResult Index(string username)
        {
            var pins = db.Pins.Include(p => p.Type).Include(p => p.User);
            if (WebSecurity.IsAuthenticated == true  || String.IsNullOrEmpty(username) == false)
            {
                if (String.IsNullOrEmpty(username)) { pins = pins.Where(p => p.User.UserName == WebSecurity.CurrentUserName); }
                else { pins = pins.Where(p => p.User.UserName == username); }
            }
            return View(pins.ToList());
        }

        //
        // GET: /Pin/Details/5

        public ActionResult Details(int id = 0)
        {
            var pins = db.Pins.Include(p => p.User).Include(p => p.Type);
            Pin pin = pins.Single(p => p.PinId == id);

            if (pin == null)
            {
                return HttpNotFound();
            }
            return View(pin);
        }

        //
        // GET: /Pin/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name");
            return View();
        }

        //
        // POST: /Pin/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Pin pin)
        {
            if (ModelState.IsValid)
            {
                pin.User = db.UserProfiles.Single(u => u.UserId == WebSecurity.CurrentUserId);
                db.Pins.Add(pin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", pin.TypeId);
            return View(pin);
        }

        //
        // GET: /Pin/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", pin.TypeId);
            return View(pin);
        }

        //
        // POST: /Pin/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Pin pin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", pin.TypeId);
            return View(pin);
        }

        //
        // GET: /Pin/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }
            return View(pin);
        }

        //
        // POST: /Pin/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pin pin = db.Pins.Find(id);
            db.Pins.Remove(pin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}