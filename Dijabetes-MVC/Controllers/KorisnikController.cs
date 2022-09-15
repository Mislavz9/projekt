using Dijabetes_MVC.Models;
using Dijabetes_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dijabetes_MVC.Controllers
{
    public class KorisnikController : Controller
    {

        DijabetEntities db = new DijabetEntities();
        KorisnikInfoVM tempVM;
        
        public ActionResult Index()
        {
            int userID = (int)System.Web.HttpContext.Current.Session["userID"];
            KorisnikInfo temp = new KorisnikInfo();
            try
            {
                temp = db.KorisnikInfo.Where(k => k.KorisnikID == userID).First();
                tempVM = new KorisnikInfoVM(temp);

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
        public ActionResult Index(KorisnikInfoVM korisnikInfoVM)
        {
            try
            {
                KorisnikInfo korisnikInfo = db.KorisnikInfo.Find(korisnikInfoVM.ID);
                korisnikInfo.Ime = korisnikInfoVM.Ime;
                korisnikInfo.Prezime = korisnikInfoVM.Prezime;
                korisnikInfo.RazinaFizAktivnostiID = korisnikInfoVM.RazinaFizAktivnostiID;
                korisnikInfo.SpolID = korisnikInfoVM.SpolID;
                korisnikInfo.Tezina = korisnikInfoVM.Tezina;
                korisnikInfo.TipDijabetesaID = korisnikInfoVM.TipDijabetesaID;
                korisnikInfo.Visina = korisnikInfoVM.Visina;
                db.Entry(korisnikInfo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
