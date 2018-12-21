using Projektni_centar_proba.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projektni_centar_proba.Controllers
{
    public class RegisterOController : Controller
    {
        BazaEntities1 ob = new BazaEntities1();
        // GET: RegisterO
        public ActionResult SetDataInDataBaseO()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SetDataInDataBaseO(Obrasac model, HttpPostedFileBase image1) //Postavlja unete podatke u bazu
        {
            Obrasac tbl = new Obrasac();
            tbl.Korisnik = (string)Session["UserName"];
            tbl.UserEmail = (string)Session["Email"];
            tbl.KorisnikTip = (string)Session["TipKorisnika"];
            tbl.NazivSkole = model.NazivSkole;
            tbl.AdresaReg = model.AdresaReg;
            tbl.Opstina = model.Opstina;
            tbl.PostaBr = model.PostaBr;
            tbl.MatBrSkole = model.MatBrSkole;
            tbl.PIB = model.PIB;
            tbl.BrojRacuna = model.BrojRacuna;
            tbl.Web = model.Web;
            tbl.Ime = model.Ime;
            tbl.Prezime = model.Prezime;
            tbl.RadnoMes = model.RadnoMes;
            tbl.VrstaTel = model.VrstaTel;
            tbl.BrTel = model.BrTel;
            tbl.Lokal = model.Lokal;
            tbl.VrstaEmail = model.VrstaEmail;
            tbl.Email = model.Email;
            tbl.Beleske = model.Beleske;
            if (image1 != null)
            {
                tbl.FotoPecat = new byte[image1.ContentLength];
                image1.InputStream.Read(tbl.FotoPecat, 0, image1.ContentLength);
            }
            ob.Obrasacs.Add(tbl);
            ob.SaveChanges();
            return View();
        }

        public ActionResult ShowDataBaseForUserO() //Vraca listu obrazaca za tip korisnika Admin
        {
            var item = ob.Obrasacs.ToList();
            return View(item);
        }
        public ActionResult ShowDataBaseForUserOK() //Vraca listu obrazaca za tip korisnika Korisnik
        {
            var item = ob.Obrasacs.ToList();
            return View(item);
        }
        public ActionResult ShowDataBaseForUserOP() //Vraca listu obrazaca za tip korisnika Pregledac
        {
            var item = ob.Obrasacs.ToList();
            return View(item);
        }

        public ActionResult Delete(int id) //Brise obrazac
        {
            var item = ob.Obrasacs.Where(x => x.ID == id).First();
            ob.Obrasacs.Remove(item);
            ob.SaveChanges();
            var item2 = ob.Obrasacs.ToList();
            return View("ShowDataBaseForUserO", item2);
        }

        public ActionResult EditO(int id) //Pronalazi obrazac za prepravljanje
        {
            var item = ob.Obrasacs.Where(x => x.ID == id).First();
            return View(item);
        }


        [HttpPost]
        public ActionResult EditO(Obrasac model, HttpPostedFileBase image2) //Prepravlja podatke u obrazcu
        {
            var item = ob.Obrasacs.Where(x => x.ID == model.ID).First();
            item.NazivSkole = model.NazivSkole;
            item.AdresaReg = model.AdresaReg;
            item.Opstina = model.Opstina;
            item.PostaBr = model.PostaBr;
            item.MatBrSkole = model.MatBrSkole;
            item.PIB = model.PIB;
            item.BrojRacuna = model.BrojRacuna;
            item.Web = model.Web;
            item.Ime = model.Ime;
            item.Prezime = model.Prezime;
            item.RadnoMes = model.RadnoMes;
            item.VrstaTel = model.VrstaTel;
            item.BrTel = model.BrTel;
            item.Lokal = model.Lokal;
            item.VrstaEmail = model.VrstaEmail;
            item.Email = model.Email;
            item.Beleske = model.Beleske;
            if (image2 != null)
            {
                item.FotoPecat = new byte[image2.ContentLength];
                image2.InputStream.Read(item.FotoPecat, 0, image2.ContentLength);
            }
            ob.SaveChanges();
            return View(item);
        }
        public ActionResult EditOP(int id) //Omogucava pregled obrazaca tipu korisnika Pregledac
        {
            var item = ob.Obrasacs.Where(x => x.ID == id).First();
            return View(item);
        }
    }
}