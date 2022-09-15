using Dijabetes_MVC.Models;
using Dijabetes_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dijabetes_MVC.Controllers
{
    public class LoginController : Controller
    {
        DijabetEntities db = new DijabetEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Korisnik korisnik)
        {
            try
            {
                Korisnik temp = db.Korisnik.ToList().Where(k => (k.Email == korisnik.Email && k.Password == korisnik.Password)).First();
                Session["userID"] = temp.ID;
            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", "Username or password incorrect!");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration ()
        {
            KorisnikCreateVM tempVM = new KorisnikCreateVM();
            try
            {
                foreach (var spol in db.Spol.ToList())
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = spol.ID.ToString();
                    item.Text = spol.Naziv;
                    tempVM.SpolItems.Add(item);
                }

                foreach (var razina in db.RazinaFizAktivnosti.ToList())
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = razina.ID.ToString();
                    item.Text = razina.Razina;
                    tempVM.RazinaAktivnostiItems.Add(item);
                }

                foreach (var tip in db.TipDijabetesa.ToList())
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = tip.ID.ToString();
                    item.Text = tip.Tip.ToString();
                    tempVM.TipDijabetesaItems.Add(item);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return View(tempVM);
        }

        [HttpPost]
        public ActionResult Registration(KorisnikCreateVM korisnikCreateVM)
        {
            Korisnik korisnikToInsert = new Korisnik();
            KorisnikInfo infoToInsert = new KorisnikInfo();

            try
            {
                korisnikToInsert.Email = korisnikCreateVM.Korisnik.Email;
                korisnikToInsert.Password = korisnikCreateVM.Korisnik.Password;

                db.Korisnik.Add(korisnikToInsert);
                db.SaveChanges();

                infoToInsert.KorisnikID = korisnikToInsert.ID;
                infoToInsert.Ime = korisnikCreateVM.Ime;
                infoToInsert.Prezime = korisnikCreateVM.Ime;
                infoToInsert.RazinaFizAktivnostiID = korisnikCreateVM.RazinaFizAktivnostiID;
                infoToInsert.TipDijabetesaID = korisnikCreateVM.TipDijabetesaID;
                infoToInsert.SpolID = korisnikCreateVM.SpolID;
                infoToInsert.Datum = korisnikCreateVM.Datum;
                infoToInsert.Tezina = korisnikCreateVM.Tezina;
                infoToInsert.Visina = korisnikCreateVM.Visina;

                db.KorisnikInfo.Add(infoToInsert);
                db.SaveChanges();

                infoToInsert.DnevneKcal = (int)(((10 * infoToInsert.Tezina) + (6.25 * (double)infoToInsert.Visina) - (5 * (DateTime.Now.Year - infoToInsert.Datum.Year))
                    + GetFaktorSpola(infoToInsert)) * GetFaktorAktivnosti(infoToInsert) * GetFaktorDijabetes(infoToInsert));
                db.Entry(infoToInsert).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            Session["userID"] = korisnikToInsert.ID;
            return RedirectToAction("Index", "Home");
        }

        private int GetFaktorSpola (KorisnikInfo korisnikInfo)
        {
            if (db.Spol.Find(korisnikInfo.SpolID).Naziv == "Muški") return 5;
            else return -161;
        }

        private double GetFaktorAktivnosti (KorisnikInfo korisnikInfo)
        {
            if (db.RazinaFizAktivnosti.Find(korisnikInfo.RazinaFizAktivnostiID).Razina == "Niska") return 1.2;
            else if (db.RazinaFizAktivnosti.Find(korisnikInfo.RazinaFizAktivnostiID).Razina == "Umjerena") return 1.375;
            else return 1.5;
        }

        public double GetFaktorDijabetes (KorisnikInfo korisnikInfo)
        {
            if (db.TipDijabetesa.Find(korisnikInfo.TipDijabetesaID).Tip == 1) return 0.99;
            else return 0.98;
        }
    }
}