using Projektni_centar_proba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Projektni_centar_proba.Controllers
{
    public class RegisterController : Controller
    {
        
        BazaEntities3 db = new BazaEntities3();
        // GET: Register
        public ActionResult SetDataInDataBase()
        {
            return View();
        }
        
        public JsonResult SaveData(SiteUser model) //Čuva podatke u bazu
        {
            model.IsValid = false;
            db.SiteUsers.Add(model);
            db.SaveChanges();
            BuildEmailTemplate(model.ID);
            return Json("Registration Successfull", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Confirm(int regId) //Konfirmacija SignUp-a, vraća pogled
        {
            ViewBag.regID = regId;
            return View();
        }
        public JsonResult RegisterConfirm(int regId) //Konfirmacija registracije, menjanje korisnika u valid i postavljanje default tipa korisnika na korisnike
        {
            SiteUser Data = db.SiteUsers.Where(x => x.ID == regId).FirstOrDefault();
            Data.IsValid = true;
            Data.TipKorisnika = "Korisnik";
            db.SaveChanges();
            var msg = "Your Email Is Verified!";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public void BuildEmailTemplate(int regID) //Povezuje email sa pogledom
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = db.SiteUsers.Where(x => x.ID == regID).FirstOrDefault();
            var url = "http://localhost:50345/" + "Register/Confirm?regId=" + regID;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Your Account Is Successfully Created", body, regInfo.Email);
        }
        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo) //Pravi emali
        {
            string from, to, bcc, cc, subject, body;
            from = "obrazac.project.ITS@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }
        public static void SendEmail(MailMessage mail) //Loguje se na gmail acc sa koga se salje konfirmacioni mail
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("obrazac.project.ITS@gmail.com", "ProjectITS1");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult CheckValidUser(SiteUser model) //Proverava da li je registracija validna, otvara sesiju
        {
            string result = "Fail";
            var DataItem = db.SiteUsers.Where(x => x.Email == model.Email && x.Password == model.Password && x.Username == model.Username).SingleOrDefault();
            if (DataItem != null)
            {
                Session["UserID"] = DataItem.ID.ToString();
                Session["UserName"] = DataItem.Username.ToString();
                Session["Email"] = DataItem.Email.ToString();
                Session["TipKorisnika"] = DataItem.TipKorisnika.ToString();
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AfterLogin() //Vraca pogled ako je validan Korisnik, a ako nije vraca nas na Login stranicu
        {
            if (Session["UserID"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("SetDataInDataBase");
            }
        }

        public ActionResult Logout() //Vraca na login stranicu i cisti sesiju
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("SetDataInDataBase");
        }
        public ActionResult ShowDataBaseForUser() //Vraca listu korisnika
        {
            var item = db.SiteUsers.ToList();
            return View(item);
        }
        public ActionResult Delete(int id) //Brise sve informacije o korisniku
        {
            var item = db.SiteUsers.Where(x => x.ID == id).First();
            db.SiteUsers.Remove(item);
            db.SaveChanges();
            var item2 = db.SiteUsers.ToList();
            return View("ShowDataBaseForUser", item2);
        }

        public ActionResult Edit(int id) //Pronalazi korisnika za edit
        {
            var item = db.SiteUsers.Where(x => x.ID == id).First();
            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(SiteUser model) //Edituje informacije o korisniku
        {
            var item = db.SiteUsers.Where(x => x.ID == model.ID).First();
            item.Username = model.Username;
            item.Password = model.Password;
            item.TipKorisnika = model.TipKorisnika;
            db.SaveChanges();
            return View();
        }
        public ActionResult AddNewUser() //Vraca pogled AddNewUser
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewUser(SiteUser model) //Omogucava adminu da doda novog korisnika
        {
            SiteUser tbl = new SiteUser();
            tbl.Email = model.Email;
            tbl.Username = model.Username;
            tbl.Password = model.Password;
            tbl.TipKorisnika = model.TipKorisnika;
            tbl.IsValid = true;

            db.SiteUsers.Add(tbl);
            db.SaveChanges();
            return View();
        }
    }
}