using Dijabetes_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dijabetes_MVC.ViewModels
{
	public class KorisnikInfoVM : KorisnikInfo
	{
		public List<SelectListItem> TipDijabetesaItems;
		public List<SelectListItem> RazinaAktivnostiItems;
		public List<SelectListItem> SpolItems;

		public KorisnikInfoVM()
		{
			TipDijabetesaItems = new List<SelectListItem>();
			RazinaAktivnostiItems = new List<SelectListItem>();
			SpolItems = new List<SelectListItem>();
		}

		public KorisnikInfoVM(KorisnikInfo k)
		{
			TipDijabetesaItems = new List<SelectListItem>();
			RazinaAktivnostiItems = new List<SelectListItem>();
			SpolItems = new List<SelectListItem>();

			this.ID = k.ID;
			this.Datum = k.Datum;
			this.Ime = k.Ime;
			this.KorisnikID = k.KorisnikID;
			this.Prezime = k.Prezime;
			this.RazinaFizAktivnostiID = k.RazinaFizAktivnostiID;
			this.SpolID = k.SpolID;
			this.Tezina = k.Tezina;
			this.Visina = k.Visina;
			this.TipDijabetesaID = k.TipDijabetesaID;
		}
	}
}