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
    public class HomeController : Controller
    {
        private PinboardEntities db = new PinboardEntities();

        public ActionResult Index(int? board)
        {
            var pins = db.Pins.Include(p => p.Type).Include(p => p.User).Include(p => p.Board);
            pins = pins.Where(p => p.User.UserName == WebSecurity.CurrentUserName);
            if (board.HasValue == true) { pins = pins.Where(p => p.Board.BoardId == board); }
            ViewBag.Boards = db.Boards.Include(p => p.User).Where(b => b.User.UserId == WebSecurity.CurrentUserId).ToList();
            ViewBag.Logged = WebSecurity.IsAuthenticated;
            return View(pins.ToList());
        }
    }
}
