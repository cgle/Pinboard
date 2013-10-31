using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinboard.Models;
using WebMatrix.WebData;

namespace Pinboard.Controllers
{
    public class BoardController : Controller
    {
        private PinboardEntities db = new PinboardEntities();

        //
        // GET: /Board/

        public ActionResult Index()
        {
            return View(db.Boards.ToList());
        }

        //
        // GET: /Board/Details/5

        public ActionResult Details(int id = 0)
        {
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        //
        // GET: /Board/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Board/Create

        [HttpPost]
        public ActionResult Create(Board board)
        {
            if (ModelState.IsValid)
            {
                board.User = db.UserProfiles.Single(u => u.UserId == WebSecurity.CurrentUserId);
                db.Boards.Add(board);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(board);
        }

        //
        // GET: /Board/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        //
        // POST: /Board/Edit/5

        [HttpPost]
        public ActionResult Edit(Board board)
        {
            if (ModelState.IsValid)
            {
                db.Entry(board).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(board);
        }

        //
        // GET: /Board/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        //
        // POST: /Board/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Board board = db.Boards.Find(id);
            db.Boards.Remove(board);
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