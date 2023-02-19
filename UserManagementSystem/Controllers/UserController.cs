using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;


namespace Assignment.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private Database1Entities2 db = new Database1Entities2();

        public ActionResult Index(int? page, string sortBy)
        {
            if (!checkSession())
            {
                return RedirectToAction("Index", "Login", new { multipleSession=true});
            }

            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Username desc" : "";
            ViewBag.SortFullNameParameter = sortBy =="FullName" ? "FullName desc" : "FullName";
            ViewBag.SortDateParameter = sortBy == "Date" ? "Date desc" : "Date";
            ViewBag.SortTeamParameter = sortBy == "Team" ? "Team desc" : "Team";
            ViewBag.SortPositionParameter = sortBy == "Position" ? "Position desc" : "Position";
            ViewBag.SortStatusParameter = sortBy == "Status" ? "Status desc" : "Status";

            var model = db.Users.AsQueryable();

            switch (sortBy)
            {
                case "Username desc":
                    model = model.OrderByDescending(x => x.Username);
                    break;

                case "FullName desc":
                    model = model.OrderByDescending(x => x.FullName);
                    break;

                case "FullName":
                    model = model.OrderBy(x => x.FullName);
                    break;

                case "Date desc":
                    model = model.OrderByDescending(x => x.JoinDate);
                    break;

                case "Date":
                    model = model.OrderBy(x => x.JoinDate);
                    break;

                case "Team desc":
                    model = model.OrderByDescending(x => x.Team.TeamName);
                    break;

                case "Team":
                    model = model.OrderBy(x => x.Team.TeamName);
                    break;

                case "Position desc":
                    model = model.OrderByDescending(x => x.Position.positionName);
                    break;

                case "Position":
                    model = model.OrderBy(x => x.Position.positionName);
                    break;

                case "Status desc":
                    model = model.OrderByDescending(x => x.Status.currentStatus);
                    break;

                case "Status":
                    model = model.OrderBy(x => x.Status.currentStatus);
                    break;

                default:
                    model = model.OrderBy(x => x.Username);
                    break;

            }
            

            return View(model.ToPagedList(page ?? 1,3));
        }

        public JsonResult IsUserNameExists(string UserName)
        {
            
            return Json(!db.Users.Any(x => x.Username.ToUpper() == UserName.ToUpper()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEmployeeIdExist(int employeeId)
        {
            return Json(!db.Users.Any(x => x.EmployeeId==employeeId), JsonRequestBehavior.AllowGet);
        }

        public bool checkSession()
        {
            if (User.Identity.Name != null)
            {
                if (DBManage.CompareSession(User.Identity.Name, System.Web.HttpContext.Current.Session.SessionID) == false)
                {
                    return false;
                }
            }
            return true;
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (!checkSession())
            {
                return RedirectToAction("Index", "Login", new { multipleSession = true });
            }
            
            ViewBag.teamName = new SelectList(db.Teams.ToList(), "Id", "TeamName");
            ViewBag.positionName = new SelectList(db.Positions.ToList(), "Id", "positionName");
            ViewBag.statusName = new SelectList(db.Status.ToList(), "Id", "currentStatus");

            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.StatusId = 1;
                DBManage.CreateUser(user);
                return RedirectToAction("Index");
            }

            ViewBag.teamName = new SelectList(db.Teams.ToList(), "Id", "TeamName");
            ViewBag.positionName = new SelectList(db.Positions.ToList(), "Id", "positionName");
            ViewBag.statusName = new SelectList(db.Status.ToList(), "Id", "currentStatus");


            return View(user);
        }

        [HttpGet] 
        public ActionResult Edit(int? id)
        {
            if (!checkSession())
            {
                return RedirectToAction("Index", "Login", new { multipleSession = true });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);

            if (user == null) {
                return View(user);
            }
                
            ViewBag.teamName = new SelectList(db.Teams.ToList(), "Id", "TeamName");
            ViewBag.positionName = new SelectList(db.Positions.ToList(), "Id", "positionName");
            ViewBag.statusName = new SelectList(db.Status.ToList(), "Id", "currentStatus");


            return View(user);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                DBManage.EditUser(user);
                return RedirectToAction("Index");
            }

            
            ViewBag.teamName = new SelectList(db.Teams.ToList(), "Id", "TeamName");  
            ViewBag.positionName = new SelectList(db.Positions.ToList(), "Id", "positionName");
            ViewBag.statusName = new SelectList(db.Status.ToList(), "Id", "currentStatus");

            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            User user = db.Users.Find(id);
            DBManage.DeleteUser(user);
           
            return RedirectToAction("Index");
        }
    }
}