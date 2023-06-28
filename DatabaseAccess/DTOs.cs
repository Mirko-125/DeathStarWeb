using DeathStar_new.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeathStar_new
{
    #region Brod , Transportni brod, Borbeni brod // licni komentari
    public class BrodBasic
    {
        public int jedinstveniBroj;
        public string naziv;
        public double maxWarpBrzina;
        public PlanetaBasic planetaKonstrukcije;
        public PosadaBasic posadaKojuPoseduje;

        public BrodBasic() { }

        public BrodBasic(int jedinstveniBroj, string naziv, double maxWarpBrzina, PlanetaBasic planetaKonstrukcije, PosadaBasic posadaKojuPoseduje)
        {
            this.jedinstveniBroj = jedinstveniBroj;
            this.naziv = naziv;
            this.maxWarpBrzina = maxWarpBrzina;
            this.planetaKonstrukcije = planetaKonstrukcije;
            this.posadaKojuPoseduje = posadaKojuPoseduje;
        }
    }
    public class BorbeniBrodBasic : BrodBasic
    {
        public bool fotonskoTorpedo { get; set; }
        public int brojLaserskihTopova{get; set;}
        public string tip{get; set;}
        public BorbeniBrodBasic() { }
        // radim po nekom sablonu, trebalo bi dobro da je, proveri da li u ovaj pun konstruktor trebaju i atributi iz nadklase ili ne
        public BorbeniBrodBasic(int jedinstveniBroj, string naziv, double maxWarpBrzina, PlanetaBasic platenaKonstrukcije, PosadaBasic posadaKojuPoseduje, bool fotonskoTorpedo, int brojLaserskihTopova, string tip) : base(jedinstveniBroj, naziv, maxWarpBrzina, platenaKonstrukcije, posadaKojuPoseduje)
        {
            this.fotonskoTorpedo = fotonskoTorpedo;
            this.brojLaserskihTopova = brojLaserskihTopova;
            this.tip = tip;
        }
    }
    public class TransportniBrodBasic : BrodBasic
    {
        public bool zastitnaOtplata { get; set; } // ;D
        public double nosivost { get; set; }
        public TransportniBrodBasic() { }
        public TransportniBrodBasic(int jedinstveniBroj, string naziv, double maxWarpBrzina, PlanetaBasic platenaKonstrukcije, PosadaBasic posadaKojuPoseduje, bool zastitnaOtplata, double nosivost) : base(jedinstveniBroj, naziv, maxWarpBrzina, platenaKonstrukcije, posadaKojuPoseduje)
        {
            this.zastitnaOtplata = zastitnaOtplata;
            this.nosivost = nosivost;
        }
    }
    public class BrodPregled
    {
        public int jedinstveniBroj { get; set; }
        public string naziv{get; set;}
        public double maxWarpBrzina{get; set;}
        // koliko sam shvatio u pregled idu stvari koje su samo domace dok u basic ide i domace i strano
        public BrodPregled() { }
        public BrodPregled(int jedinstveniBroj, string naziv, double maxWarpBrzina)
        {
            this.jedinstveniBroj = jedinstveniBroj;
            this.naziv = naziv;
            this.maxWarpBrzina = maxWarpBrzina;
        }
    }
    public class BorbeniBrodPregled : BrodPregled
    {
        public bool fotonskoTorpedo { get; set; }
        public int brojLaserskihTopova{get; set;}
        public string tip{get; set;}
        public BorbeniBrodPregled() { }
        public BorbeniBrodPregled(int jedinstveniBroj, string naziv, double maxWarpBrzina, bool fotonskoTorpedo, int brojLaserskihTopova, string tip) : base(jedinstveniBroj, naziv, maxWarpBrzina)
        {
            this.fotonskoTorpedo = fotonskoTorpedo;
            this.brojLaserskihTopova = brojLaserskihTopova;
            this.tip = tip;
        }
    }
    public class TransportniBrodPregled : BrodPregled
    {
        public bool zastitnaOtplata;
        public double nosivost;
        public TransportniBrodPregled() { }
        public TransportniBrodPregled(int jedinstveniBroj, string naziv, double maxWarpBrzina, bool zastitnaOtplata, double nosivost) : base(jedinstveniBroj, naziv, maxWarpBrzina)
        {
            this.zastitnaOtplata = zastitnaOtplata;
            this.nosivost = nosivost;
        }
    }
    #endregion

    #region Stanice, svemirske, vojnce, sateliti... i spisak oruzja // proveri visevrednosni atribut
    public class SatelitBasic
    {
        public int id;
        public string naziv;
        public int udaljenost;
        public PlanetaBasic kruziOkoPlanete;
        public SatelitBasic() { }
        public SatelitBasic(int id, string naziv, int udaljenost, PlanetaBasic kruziOkoPlanete)
        {
            this.id = id;
            this.naziv = naziv;
            this.udaljenost = udaljenost;
            this.kruziOkoPlanete = kruziOkoPlanete;
        }
    }
    public class PrirodniSatelitBasic
    {
        public string naziv { get; set; }
        public int udaljenost{get; set;}
        public PlanetaBasic kruziOkoPlanete{get; set;}
        public int precnik{get; set;}
        public bool naseobine{get; set;}
        public PrirodniSatelitBasic() { }
        public PrirodniSatelitBasic(string naziv, int udaljenost, PlanetaBasic kruziOkoPlanete, int precnik, bool naseobine)
        {
            this.naziv = naziv;
            this.udaljenost = udaljenost;
            this.kruziOkoPlanete = kruziOkoPlanete;
            this.precnik = precnik;
            this.naseobine = naseobine;
        }
    }
    public class SvemirskaStanicaBasic : SatelitBasic
    {
        public int brojLjudi;
        public int velicina;
        public SvemirskaStanicaBasic() { }
        public SvemirskaStanicaBasic(int id, string naziv, int udaljenost, PlanetaBasic kruziOkoPlanete, int brojLjudi, int velicina) : base(id, naziv, udaljenost, kruziOkoPlanete)
        {
            this.brojLjudi = brojLjudi;
            this.velicina = velicina;
        }
    }
    public class CivilnaStanicaBasic : SvemirskaStanicaBasic
    {
        public string svrha;
        public CivilnaStanicaBasic() { }
        public CivilnaStanicaBasic(int id, string naziv, int udaljenost, PlanetaBasic kruziOkoPlanete, string svrha, int brojLjudi, int velicina) : base(id, naziv, udaljenost, kruziOkoPlanete, brojLjudi, velicina)
        {
            this.svrha = svrha;
        }
    }
    public class VojnaStanicaBasic : SvemirskaStanicaBasic
    {
        public IList<SpisakOruzjaBasic> oruzja;
        public VojnaStanicaBasic()
        {
            oruzja = new List<SpisakOruzjaBasic>();
        }
        public VojnaStanicaBasic(int id, string naziv, int udaljenost, PlanetaBasic kruziOkoPlanete, int brojLjudi, int velicina) : base(id, naziv, udaljenost, kruziOkoPlanete, brojLjudi, velicina)
        {
            // doublecheckuj ovo
        }
    }
    public class SpisakOruzjaBasic
    {
        public VojnaStanicaBasic oruzja;
        public string oruzje;
        public SpisakOruzjaBasic() { }
        public SpisakOruzjaBasic(VojnaStanicaBasic oruzja, string oruzje)
        {
            this.oruzja = oruzja;
            this.oruzje = oruzje;
        }
    }
    public class SatelitPregled
    {
        public string naziv { get; set; }
        public int udaljenost { get; set; }
        // ispravi me ako gresim ali trebalo bi da je ovako
        public SatelitPregled() { }
        public SatelitPregled(string naziv, int udaljenost)
        {
            this.naziv = naziv;
            this.udaljenost = udaljenost;
        }
    }
    public class PrirodniSatelitPregled : SatelitPregled
    {
        public int precnik { get; set; }
        public bool naseobine{get; set;}
        public PrirodniSatelitPregled() { }
        public PrirodniSatelitPregled(string naziv, int udaljenost, int precnik, bool naseobine) : base(naziv, udaljenost)
        {
            this.precnik = precnik;
            this.naseobine = naseobine;
        }
    }
    public class SvemirskaStanicaPregled : SatelitPregled
    {
        public int id { get; set; }
        public int brojLjudi{get; set; }
        public int velicina{get; set; }
        public SvemirskaStanicaPregled() { }
        public SvemirskaStanicaPregled(int id, string naziv, int udaljenost, int brojLjudi, int velicina) :base(naziv, udaljenost)
        {
            this.id = id;
            this.brojLjudi = brojLjudi;
            this.velicina = velicina;
        }
    }
    public class CivilnaSvemirskaStanicaPregled : SvemirskaStanicaPregled
    {
        public string svrha { get; set; }
        public CivilnaSvemirskaStanicaPregled() { }
        public CivilnaSvemirskaStanicaPregled(int id, string naziv, int udaljenost, string svrha, int brojLjudi, int velicina) : base(id, naziv, udaljenost, brojLjudi, velicina)
        {
            this.svrha = svrha;
        }
    }
    public class VojnaStanicaPregled : SvemirskaStanicaPregled
    {
        public VojnaStanicaPregled()
        {
            // nzm sad kako da prikaze spsak oruzja
        }
        public VojnaStanicaPregled(int id, string naziv, int udaljenost, int brojLjudi, int velicina) : base(id, naziv, udaljenost, brojLjudi, velicina)
        {
            // doublecheckuj ovo
        }
    }
    public class SpisakOruzjaPregled
    {
        public string oruzje;
        public SpisakOruzjaPregled() { }
        public SpisakOruzjaPregled(string oruzje)
        {
            this.oruzje = oruzje;
        }
    }
    #endregion

    #region Igrac, sumnjive liste
    public class IgracBasic
    {
        public string username { get; set; }
        public string ime{ get; set; }
        public string prezime{ get; set; }
        public string pol{ get; set; }
        public string drzava{ get; set; }
        public DateTime datumOtvaranjaNaloga{ get; set; }
        public DateTime datumRodjenja{ get; set; }
        public string email{ get; set; }
        public string urlAvatara{ get; set; }
        public string opis{ get; set; }
        //kljucevi...
        public PosadaBasic deoPosade{ get; set; }
        public PlanetaBasic maticnaPlaneta{ get; set; }
        public IList<PlanetaBasic> posedujePlanete;
        public SavezBasic savezKomePripada{ get; set; }
        public IgracBasic()
        {
            posedujePlanete = new List<PlanetaBasic>();
        }
        public IgracBasic(string username, string ime, string prezime, string pol, string drzava, DateTime datumOtvaranjaNaloga, DateTime datumRodjenja, string email, string urlAvatara, string opis, PosadaBasic deoPosade, PlanetaBasic maticnaPlaneta, SavezBasic savezKomePripada)
        {
            this.username = username;
            this.ime = ime;
            this.prezime = prezime;
            this.pol = pol;
            this.drzava = drzava;
            this.datumOtvaranjaNaloga = datumOtvaranjaNaloga;
            this.datumRodjenja = datumRodjenja;
            this.email = email;
            this.urlAvatara = urlAvatara;
            this.opis = opis;
            this.deoPosade = deoPosade;
            this.maticnaPlaneta = maticnaPlaneta;
            this.savezKomePripada = savezKomePripada;
        }
    }
    public class IgracPregled
    {
        public string username { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string pol { get; set; }
        public string drzava { get; set; }
        public DateTime datumOtvaranjaNaloga { get; set; }
        public DateTime datumRodjenja { get; set; }
        public string email { get; set; }
        public string urlAvatara { get; set; }
        public string opis { get; set; }
        public IgracPregled()
        {

        }
        public IgracPregled(string username, string ime, string prezime, string pol, string drzava, DateTime datumOtvaranjaNaloga, DateTime datumRodjenja, string email, string urlAvatara, string opis)
        {
            this.username = username;
            this.ime = ime;
            this.prezime = prezime;
            this.pol = pol;
            this.drzava = drzava;
            this.datumOtvaranjaNaloga = datumOtvaranjaNaloga;
            this.datumRodjenja = datumRodjenja;
            this.email = email;
            this.urlAvatara = urlAvatara;
            this.opis = opis;
        }
    }
    #endregion

    #region Planeta i njeni gradovi, proveriti liste!
    public class PlanetaBasic
    {
        public int idPlanete { get; set; }
        public string naziv{ get; set; }
        public string glavniGrad{ get; set; }
        public Int64 brojStanovnika{ get; set; }
        public string dominantnaRasa{ get; set; }
        public string drustvenoUredjenje{ get; set; }
        public string imeZvezdanogSistema{ get; set; }
        public string tipZvezdanogSistema{ get; set; }
        public int x{ get; set; }
        public int y{ get; set; }
        public int z{ get; set; }
        public int berilijum{ get; set; }
        public int trilijum{ get; set; }
        public int plutonijum{ get; set; }
        public DateTime datumKolonizacije{ get; set; }

        public IList<GradoviPlaneteBasic> gradovi;
        public PosadaBasic posadaOsvajaca;
        public PosadaBasic posadaKolonista;
        public IgracBasic igracMaticna;
        public IgracBasic igracKojiPoseduje;
        public GalaksijaBasic uGalaksiji { get; set; }
        public IList<SatelitBasic> sateliti;
        public IList<BrodBasic> brodovi;
        public IList<PojavaBasic> pojave;

        public PlanetaBasic()
        {
            gradovi = new List<GradoviPlaneteBasic>();
            sateliti = new List<SatelitBasic>();
            brodovi = new List<BrodBasic>();
            pojave = new List<PojavaBasic>();
        }
        public PlanetaBasic(int idPlanete, string naziv, string glavniGrad, Int64 brojStanovnika, string dominantnaRasa, string drustvenoUredjenje, string imeZvezdanogSistema, string tipZvezdanogSistema, int x, int y, int z, int berilijum, int trilijum, int plutonijum, DateTime datumKolonizacije, PosadaBasic posadaOsvajaca, PosadaBasic posadaKolonista, IgracBasic igracMaticna, IgracBasic igracKojiPoseduje, GalaksijaBasic uGalaksiji)
        {
            this.idPlanete = idPlanete;
            this.naziv = naziv;
            this.glavniGrad = glavniGrad;
            this.brojStanovnika = brojStanovnika;
            this.dominantnaRasa = dominantnaRasa;
            this.drustvenoUredjenje = drustvenoUredjenje;
            this.imeZvezdanogSistema = imeZvezdanogSistema;
            this.tipZvezdanogSistema = tipZvezdanogSistema;
            this.x = x;
            this.y = y;
            this.z = z;
            this.berilijum = berilijum;
            this.trilijum = trilijum;
            this.plutonijum = plutonijum;
            this.datumKolonizacije = datumKolonizacije;
            this.posadaOsvajaca = posadaOsvajaca;
            this.posadaKolonista = posadaKolonista;
            this.igracMaticna = igracMaticna;
            this.igracKojiPoseduje = igracKojiPoseduje;
            this.uGalaksiji = uGalaksiji;
        }
    }
    public class GradoviPlaneteBasic
    {
        public PlanetaBasic gradPlaneta;
        public string grad;
        public GradoviPlaneteBasic() { }
        public GradoviPlaneteBasic(PlanetaBasic gradPlaneta, string grad)
        {
            this.gradPlaneta = gradPlaneta;
            this.grad = grad;
        }
    }
    public class PlanetaPregled
    {
        public int idPlanete { get; set; }
        public string naziv {get; set;}
        public string glavniGrad {get; set;}
        public Int64 brojStanovnika {get; set;}
        public string dominantnaRasa {get; set;}
        public string drustvenoUredjenje {get; set;}
        public string imeZvezdanogSistema {get; set;}
        public string tipZvezdanogSistema {get; set;}
        public int x {get; set;}
        public int y {get; set;}
        public int z {get; set;}
        public int berilijum {get; set;}
        public int trilijum {get; set;}
        public int plutonijum {get; set;}
        public DateTime datumKolonizacije {get; set;}

        public PlanetaPregled() { }
        public PlanetaPregled(int idPlanete, string naziv, string glavniGrad, Int64 brojStanovnika, string dominantnaRasa, string drustvenoUredjenje, string imeZvezdanogSistema, string tipZvezdanogSistema, int x, int y, int z, int berilijum, int trilijum, int plutonijum, DateTime datumKolonizacije)
        {
            this.idPlanete = idPlanete;
            this.naziv = naziv;
            this.glavniGrad = glavniGrad;
            this.brojStanovnika = brojStanovnika;
            this.dominantnaRasa = dominantnaRasa;
            this.drustvenoUredjenje = drustvenoUredjenje;
            this.imeZvezdanogSistema = imeZvezdanogSistema;
            this.tipZvezdanogSistema = tipZvezdanogSistema;
            this.x = x;
            this.y = y;
            this.z = z;
            this.berilijum = berilijum;
            this.trilijum = trilijum;
            this.plutonijum = plutonijum;
            this.datumKolonizacije = datumKolonizacije;
        }
    }
    public class GradoviPlanetePregled
    {
        public string grad;
        public GradoviPlanetePregled() { }
        public GradoviPlanetePregled(string grad)
        {
            this.grad = grad;
        }
    }
    #endregion

    #region Savez
    public class SavezBasic
    {
        public string? naziv { get; set; }
        public DateTime datumFormiranja;

        public SavezBasic savezDeo;
        public IList<SavezBasic> savezi;
        public IList<IgracBasic> igraci;
        public PosadaBasic posadaDeo;

        public SavezBasic()
        {
            savezi = new List<SavezBasic>();
            igraci = new List<IgracBasic>();
        }
        public SavezBasic(string naziv, DateTime datumFormiranja, SavezBasic savezDeo, PosadaBasic posadaDeo)
        {
            this.naziv = naziv;
            this.datumFormiranja = datumFormiranja;
            this.savezDeo = savezDeo;
            this.posadaDeo = posadaDeo;
        }
    }
    public class SavezPregled
    {
        public string naziv { get; set; }
        public DateTime datumFormiranja { get; set; }
        public SavezPregled() { }
        public SavezPregled(string naziv, DateTime datumFormiranja)
        {
            this.naziv = naziv;
            this.datumFormiranja = datumFormiranja;
        }
    }
    #endregion

    #region Galaksija
    public class GalaksijaBasic
    {
        public string naziv { get; set; }
        public Int64 procenjenBrojZvezda{ get; set;}
        public Int64 procenjenBrojPlaneta{ get; set;}
        public string dominantnaRasa{ get; set;}

        public IList<PlanetaBasic> planete;
        public IList<KvadrantBasic> kvadranti;

        public GalaksijaBasic() 
        {
            planete = new List<PlanetaBasic>();
            kvadranti = new List<KvadrantBasic>();
        }
        public GalaksijaBasic(string naziv, Int64 procenjenBrojZvezda, Int64 procenjenBrojPlaneta, string dominantnaRasa)
        {
            this.naziv = naziv;
            this.procenjenBrojZvezda = procenjenBrojZvezda;
            this.procenjenBrojPlaneta = procenjenBrojPlaneta;
            this.dominantnaRasa = dominantnaRasa;
        }
    }
    public class GalaksijaPregled
    {
        public string naziv { get; set; }
        public Int64 procenjenBrojZvezda { get; set; }
        public Int64 procenjenBrojPlaneta { get; set; }
        public string dominantnaRasa { get; set; }

        public GalaksijaPregled() { }
        public GalaksijaPregled(string naziv, Int64 procenjenBrojZvezda, Int64 procenjenBrojPlaneta, string dominantnaRasa)
        {
            this.naziv = naziv;
            this.procenjenBrojZvezda = procenjenBrojZvezda;
            this.procenjenBrojPlaneta = procenjenBrojPlaneta;
            this.dominantnaRasa = dominantnaRasa;
        }
    }
    #endregion

    #region Pojava
    public class PojavaBasic
    {
        public string naziv;
        public string tipPojave;
        public int udaljenost;
        public bool izazivaLiOpasnost;
        public PlanetaBasic planetaDeo;
        public PojavaBasic() { }
        public PojavaBasic(string naziv, string tipPojave, bool izazivaLiOpasnost,int udaljenost, PlanetaBasic planetaDeo)
        {
            this.udaljenost = udaljenost;
            this.naziv = naziv;
            this.tipPojave = tipPojave;
            this.izazivaLiOpasnost = izazivaLiOpasnost;
            this.planetaDeo = planetaDeo;
        }
    }
    public class PojavaPregled
    {
        public string naziv { get; set; }
        public int udaljenost{ get; set; }
        public string tipPojave{ get; set; }
        public bool izazivaLiOpasnost{ get; set; }
        public PojavaPregled() { }
        public PojavaPregled(string naziv, string tipPojave, bool izazivaLiOpasnost, int udaljenost)
        {
            this.udaljenost=udaljenost;
            this.naziv = naziv;
            this.tipPojave = tipPojave;
            this.izazivaLiOpasnost = izazivaLiOpasnost;
        }
    }
    #endregion

    #region Posada i osvajanja
    public class PosadaBasic
    {
        public int idPosade { get; set; }
        public IgracBasic igrac;
        public SavezBasic savez;
        public IList<BrodBasic> brodovi;
        public IList<OsvajanjeBasic> osvajanja;
        public PosadaBasic()
        {
            brodovi = new List<BrodBasic>();
            osvajanja = new List<OsvajanjeBasic>();
        }

        public PosadaBasic(int idPosade, IgracBasic igrac, SavezBasic savez)
        {
            this.idPosade = idPosade;
            this.igrac = igrac;
            this.savez = savez;
        }
    }

    public class OsvajanjeBasic
    {
        public int idPosade;
        public DateTime datumOsvajanja;

        public PosadaBasic posadaOsvaja;
        public IgracBasic prethodniVlasnik;
        public PlanetaBasic planetaOsvojena;

        public OsvajanjeBasic() { }
        public OsvajanjeBasic(int idPosade, DateTime datumOsvajanja, PosadaBasic posadaOsvaja, IgracBasic prethodniVlasnik, PlanetaBasic planetaOsvojena)
        {
            this.idPosade = idPosade;
            this.datumOsvajanja = datumOsvajanja;
            this.posadaOsvaja = posadaOsvaja;
            this.prethodniVlasnik = prethodniVlasnik;
            this.planetaOsvojena = planetaOsvojena;
        }
    }
    public class PosadaPregled
    { 
        public int idPosade;
        public PosadaPregled() { }
        public PosadaPregled(int idPosade)
        {
            this.idPosade = idPosade;
        }
    }

    public class OsvajanjaPregled
    {
        public int idPosade;
        public DateTime datumOsvajanja;

        public OsvajanjaPregled() { }
        public OsvajanjaPregled(int idPosade, DateTime datumOsvajanja)
        {
            this.idPosade = idPosade;
            this.datumOsvajanja = datumOsvajanja;
        }
    }
    #endregion

    #region Kvadrant
    public class KvadrantBasic
    {
        public int redniBroj;
        public double procenjeniPrecnik;
        public GalaksijaBasic galaksijaDeo;
        public KvadrantBasic() { }
        public KvadrantBasic(int redniBroj, double procenjeniPrecnik, GalaksijaBasic galaksijaDeo)
        {
            this.redniBroj = redniBroj;
            this.procenjeniPrecnik = procenjeniPrecnik;
            this.galaksijaDeo = galaksijaDeo;
        }
    }
    public class KvadrantPregled
    {
        public int redniBroj { get; set; }
        public double procenjeniPrecnik { get; set; }
        public KvadrantPregled(){}
        public KvadrantPregled(int redniBroj, double procenjeniPrecnik)
        {
            this.redniBroj = redniBroj;
            this.procenjeniPrecnik = procenjeniPrecnik;
        }
    }
    #endregion
}
