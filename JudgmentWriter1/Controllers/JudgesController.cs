using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JudgmentWriter1.DB;
using JudgmentWriter1.Models;

namespace JudgmentWriter1.Controllers
{
    public class JudgesController : Controller
    {
        private JudgeDB db = new JudgeDB();

        // GET: Judges
        public ActionResult Index()
        {

            return View(db.Judges.ToList());
        }


        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Judge judge)
        {
            if(db.Judges.FirstOrDefault(x => x.Username == judge.Username && x.Password == judge.Password) == null)
            {
                ViewData["Error"] = "username or password is incorrect";
                return RedirectToAction("Login");
            }
            Session["user"] = judge.Username;
            return RedirectToAction("Index","Judgments");
        }

        // GET: Judges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Judge judge = db.Judges.Find(id);
            if (judge == null)
            {
                return HttpNotFound();
            }
            return View(judge);
        }

        // GET: Judges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Judges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Gender,Password")] Judge judge)
        {
            if (ModelState.IsValid)
            {
                db.Judges.Add(judge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(judge);
        }

        // GET: Judges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Judge judge = db.Judges.Find(id);
            if (judge == null)
            {
                return HttpNotFound();
            }
            return View(judge);
        }

        // POST: Judges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Gender,Password")] Judge judge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(judge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(judge);
        }

        // GET: Judges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Judge judge = db.Judges.Find(id);
            if (judge == null)
            {
                return HttpNotFound();
            }
            return View(judge);
        }

        // POST: Judges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Judge judge = db.Judges.Find(id);
            db.Judges.Remove(judge);
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
    }
}
