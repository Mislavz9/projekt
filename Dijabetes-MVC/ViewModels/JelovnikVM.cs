using Dijabetes_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dijabetes_MVC.ViewModels
{
	public class JelovnikVM : Jelovnik
	{
		DijabetEntities db = new DijabetEntities();

		public List<MjernaJedinica> mjerneJedinice = new List<MjernaJedinica>();
		public List<MjernaJedinica_Namirnica_Kcal> mjerneJedinice_Namirnice_Kcal = new List<MjernaJedinica_Namirnica_Kcal>();
		public List<Namirnica> namirnice = new List<Namirnica>();
		public List<Jelovnik_Obrok> jelovnik_Obroci = new List<Jelovnik_Obrok>();

		public JelovnikVM(Jelovnik jelovnik)
		{
			mjerneJedinice = db.MjernaJedinica.ToList();
			mjerneJedinice_Namirnice_Kcal = db.MjernaJedinica_Namirnica_Kcal.ToList();
			namirnice = db.Namirnica.ToList();
			jelovnik_Obroci = db.Jelovnik_Obrok.Where(j => j.JelovnikID == jelovnik.ID).ToList();

			this.BrojObroka = jelovnik.BrojObroka;
			this.Naziv = jelovnik.Naziv;
			this.VrijediOD = jelovnik.VrijediOD;
			this.VrijediDO = jelovnik.VrijediDO;
		}
	}
}