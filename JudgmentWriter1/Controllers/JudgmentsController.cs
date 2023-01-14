using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JudgmentWriter1.DB;
using JudgmentWriter1.Models;

namespace JudgmentWriter1.Controllers
{
    public class JudgmentsController : Controller
    {
        private judgmentDB db = new judgmentDB();

        // GET: Judgments
        public ActionResult Index()
        {
            string user = Session["user"].ToString();
            return View(db.Judgments.Where(x => x.Judge == user).ToList());
        }

        public ActionResult SendEmails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Judgment judgment = db.Judgments.Find(id);
            if (judgment == null)
            {
                return HttpNotFound();
            }
            return View(judgment);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmails(int? id,string email1,string email2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Judgment judgment = db.Judgments.Find(id);
            if (judgment == null)
            {
                return HttpNotFound();
            }
            await SendJudgmentEmailTo(email1, judgment);
            await SendJudgmentEmailTo(email2, judgment);
            return View(judgment);

        }
        public async static Task SendJudgmentEmailTo(string email, Judgment judgment)
        {
            string subjectEmail = "TheJudgment";
            string bodyEmail = judgment.ToString();
            await Task.Run(() => CreateEmailItem(subjectEmail, email, bodyEmail));
        }

        private static void CreateEmailItem(string subjectEmail, string toEmail, string bodyEmail)
        {
            var senderEmail = new MailAddress("abedaa5@ac.sce.ac.il", "TheJudgment");
            var receiverEmail = new MailAddress(toEmail, "Receiver");
            var password = "Ab@1999sh";
            var sub = subjectEmail;
            var body = bodyEmail;
            var smtp = new SmtpClient
            {
                Host = "mail.ac.sce.ac.il",
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
            })
            {
                smtp.Send(mess);
            };

        }


        // GET: Judgments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Judgment judgment = db.Judgments.Find(id);
            if (judgment == null)
            {
                return HttpNotFound();
            }
            return View(judgment);
        }

        // GET: Judgments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Judgments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Court,Judge,SideA,SideB,facts,Decision,Thejudgment,Notes")] Judgment judgment)
        {
            if (ModelState.IsValid)
            {
                judgment.Judge = Session["user"].ToString();
                db.Judgments.Add(judgment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(judgment);
        }

        // GET: Judgments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Judgment judgment = db.Judgments.Find(id);
            if (judgment == null)
            {
                return HttpNotFound();
            }
            return View(judgment);
        }

        // POST: Judgments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Court,Judge,SideA,SideB,facts,Decision,Thejudgment,Notes")] Judgment judgment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(judgment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(judgment);
        }

        // GET: Judgments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Judgment judgment = db.Judgments.Find(id);
            if (judgment == null)
            {
                return HttpNotFound();
            }
            return View(judgment);
        }

        // POST: Judgments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Judgment judgment = db.Judgments.Find(id);
            db.Judgments.Remove(judgment);
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
