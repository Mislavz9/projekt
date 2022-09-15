using Dijabetes_MVC.Models;
using Dijabetes_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dijabetes_MVC.Controllers
{
    public class JelovnikController : Controller
    {
		DijabetEntities db = new DijabetEntities();
		JelovnikVM jelovnikVM;
		public ActionResult Index()
        {
			List<SelectListItem> brojeviObroka = new List<SelectListItem>();
			try
			{
				foreach (var jelovnik in db.Jelovnik.ToList())
				{
					SelectListItem item = new SelectListItem();
					item.Value = jelovnik.BrojObroka.ToString();
					item.Text = jelovnik.BrojObroka.ToString();
					brojeviObroka.Add(item);
				}
			}
			catch (Exception e)
			{
				throw e;
			}
            return View(brojeviObroka);
        }

		[HttpPost]
		public ActionResult Index(int BrojObroka)
		{
			return RedirectToAction("OdabirJelovnika", "Jelovnik", new { id = BrojObroka });
		}

		public ActionResult OdabirJelovnika(int id)
		{
			List<Jelovnik> jelovnici = new List<Jelovnik>();
			try
			{
				foreach (var jelovnik in db.Jelovnik.ToList())
				{
					if (jelovnik.BrojObroka == id)
					{
						jelovnici.Add(jelovnik);
					}
				}
			}
			catch (Exception e)
			{
				throw e;
			}
			return View(jelovnici);
		}

		public ActionResult Pregled (int id)
		{
			try
			{
				Jelovnik jelovnik = db.Jelovnik.Find(id);
				jelovnikVM = new JelovnikVM(jelovnik); 
			}
			catch (Exception)
			{
				throw;
			}
			return View(jelovnikVM);
		}

    }
}