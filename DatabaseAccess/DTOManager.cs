using DeathStar_new;
using DeathStar_new.Entiteti;
using Microsoft.Win32;
using NHibernate;
using NHibernate.Exceptions;
using Oracle.ManagedDataAccess.Client;
using Remotion.Linq.Clauses;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Collections.Specialized.BitVector32;

namespace DeathStar_new
{
    public static class DTOManager
    {

        
        #region Galaksija
        public static Result<List<GalaksijaPregled>, string> vratiSveGalaksije()
        {
            List<GalaksijaPregled> galaksije = new List<GalaksijaPregled>();
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();

                if (!(s?.IsConnected ?? false))
                {
                    return "Nemoguće otvoriti sesiju.";
                }

                IEnumerable<Galaksija> sveGalaksije = from o in s.Query<Galaksija>()
                                                      select o;

                foreach (Galaksija g in sveGalaksije)
                {
                    Console.WriteLine(g.Naziv);
                    galaksije.Add(new GalaksijaPregled(g.Naziv, g.ProcenjenBrojZvezda, g.ProcenjenBrojPlaneta, g.DominantnaRasa));
                }

            }
            catch (Exception)
            {
                return "Nemoguce vratiti sve galaksije.";
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return galaksije;
        }

        public async static Task<Result<GalaksijaPregled, string>>dodajGalaksijuAsync(GalaksijaPregled g)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();

                Galaksija o = new Galaksija();

                o.Naziv = g.naziv;
                o.ProcenjenBrojPlaneta = g.procenjenBrojPlaneta;
                o.ProcenjenBrojZvezda = g.procenjenBrojZvezda;
                o.DominantnaRasa = g.dominantnaRasa;

                await s.SaveOrUpdateAsync(o);
                await s.FlushAsync();
   
            }
            catch (Exception)
            {
                return "Nemoguce dodati galaksiju";
            }
            finally
            {
                s.Close();
                s.Dispose();
            }

            return g;
        }

        public async static Task<Result<bool, string>>obrisiGalaksijuAsync(string naziv)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Console.WriteLine(naziv);
                Galaksija o = await s.LoadAsync<Galaksija>(naziv);
                Console.WriteLine(o.Naziv);

                await s.DeleteAsync(o);
                await s.FlushAsync();
     
            }
            catch (Exception ec)
            {
                return ec.Message + ec.InnerException;
            }
            finally
            {
                s.Close();
                s.Dispose();
            }

            return true;
            
        }

        public static GalaksijaBasic vratiGalaksiju(string naziv)
        {
            GalaksijaBasic galaksijaBasic = new GalaksijaBasic();
            try
            {
                ISession s = DataLayer.GetSession();

                Galaksija galaksija = s.Load<Galaksija>(naziv);
                galaksijaBasic = new GalaksijaBasic(galaksija.Naziv,
                    galaksija.ProcenjenBrojZvezda,
                    galaksija.ProcenjenBrojPlaneta,
                    galaksija.DominantnaRasa);
            }
            catch (Exception ec)
            {
                new InnerExceptionHandler().handle(ec);
            }

            return galaksijaBasic; 
        }
        #endregion

        #region Planete
        public async static Task<Result<PlanetaPregled, string>> dodajPlanetuAsync(PlanetaPregled p, string nazivGalaksije)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Galaksija gal = await s.GetAsync<Galaksija>(nazivGalaksije);
                if (gal == null)
                    return "Ne postoji galaksija";

                Planeta o = new Planeta();

                o.Naziv = p.naziv;
                o.GlavniGrad = p.glavniGrad;
                o.DominantnaRasa = p.dominantnaRasa;
                o.DrustvenoUredjenje = p.drustvenoUredjenje;
                o.ImeZvezdanogSistema = p.imeZvezdanogSistema;
                o.TipZvezdanogSistema = p.tipZvezdanogSistema;
                o.X = p.x;
                o.Y = p.y;
                o.Z = p.z;
                o.Berilijum = p.berilijum;
                o.Trilijum = p.trilijum;
                o.Plutonijum = p.plutonijum;
                o.UGalaksiji = gal;

                await s.SaveOrUpdateAsync(o);
                await s.FlushAsync();

                p.idPlanete = o.Id;
                return p;
            }
            catch (Exception)
            {
                return "Nemoguće dodati planetu";
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
        }

        public async static Task<Result<int, string>> azurirajPlanetuAsync(PlanetaBasic p)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Galaksija gal = await s.GetAsync<Galaksija>(p.uGalaksiji.naziv);
                if (gal == null)
                    return "Ne postoji galaksija";

                Planeta o = await s.LoadAsync<Planeta>(p.idPlanete);
  
                o.Naziv = p.naziv;
                o.GlavniGrad = p.glavniGrad;
                o.DominantnaRasa = p.dominantnaRasa;
                o.DrustvenoUredjenje = p.drustvenoUredjenje;
                o.ImeZvezdanogSistema = p.imeZvezdanogSistema;
                o.TipZvezdanogSistema = p.tipZvezdanogSistema;
                o.X = p.x;
                o.Y = p.y;
                o.Z = p.z;
                o.Berilijum = p.berilijum;
                o.Trilijum = p.trilijum;
                o.Plutonijum = p.plutonijum;
                o.UGalaksiji = gal;

                await s.SaveOrUpdateAsync(o);
                await s.FlushAsync();

                return o.Id;

            }
            catch (Exception)
            {
                return "Nemoguće dodati planetu";
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

        }

        public static List<SpisakOruzjaPregled> vratiSvaOruzjaStanice(int idS)
        {
            List<SpisakOruzjaPregled> spisakOruzja= new List<SpisakOruzjaPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                VojnaStanica stanica = s.Load<VojnaStanica>(idS);

                IEnumerable<SpisakOruzja> spisakOruzjaSve = from p in s.Query<SpisakOruzja>()
                                                             where p.Stanica == stanica
                                                             select p;
                foreach (SpisakOruzja g in spisakOruzjaSve)
                {
                    spisakOruzja.Add(new SpisakOruzjaPregled { oruzje = g.Oruzje});
                }

                s.Close();
            }
            catch (Exception ec)
            {
                new InnerExceptionHandler().handle(ec);
            }
            return spisakOruzja;
        }

        public static void dodajOruzjeStanici(string naziv, int idStanice)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VojnaStanica stanica = s.Load<VojnaStanica>(idStanice);
                SpisakOruzja oruzje = new SpisakOruzja();
                oruzje.Stanica = stanica;
                oruzje.Oruzje = naziv;

                s.Save(oruzje);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                new InnerExceptionHandler().handle(ec);
            }
        }
        public async static Task<Result<bool, string>> dodajGradPlanetiAsync(string naziv, int idPlanete)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Planeta planeta = await s.GetAsync<Planeta>(idPlanete);
                GradoviPlanete grad = new GradoviPlanete();
                grad.GradPlaneta = planeta ?? throw new Exception("Planeta ne postoji");
                grad.Grad = naziv;

                await s.SaveAsync(grad);
                await s.FlushAsync();
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
            return true;
        }
        public static Result<List<GradoviPlanetePregled>, string> vratiSveGradovePlanete(int idPlanete)
        {
            List<GradoviPlanetePregled> gradovi = new List<GradoviPlanetePregled>();
            ISession? s = null;
            try
            {
                s = DataLayer.GetSession();

                Planeta planeta = s.Get<Planeta>(idPlanete);
                if (planeta == null)
                {
                    throw new Exception("Ne postoji planeta");
                }

                IEnumerable<GradoviPlanete> gradoviPlanete = from p in s.Query<GradoviPlanete>()
                                                  where p.GradPlaneta == planeta
                                                  select p;
                foreach (GradoviPlanete g in gradoviPlanete)
                {
                    gradovi.Add(new GradoviPlanetePregled { grad = g.Grad });
                }

            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return gradovi;
        }
        public static Result<List<PlanetaPregled>, string> vratiSvePlaneteGalaksije(string galaksijaNaziv)
        {
            ISession? s = null;
            List<PlanetaPregled> planete = new List<PlanetaPregled>();
            try
            {
                s = DataLayer.GetSession();

                Galaksija galaksija = s.Get<Galaksija>(galaksijaNaziv);
                if (galaksija == null) 
                {
                    throw new Exception("Ne postoji galaksija");
                }

                IEnumerable<Planeta> svePlanete = from p in s.Query<Planeta>()
                                                                      where p.UGalaksiji == galaksija
                                                                      select p;

                foreach (Planeta planeta in svePlanete)
                {
                    planete.Add(new PlanetaPregled
                    {
                        idPlanete = planeta.Id,
                        naziv = planeta.Naziv,
                        glavniGrad = planeta.GlavniGrad,
                        brojStanovnika = planeta.BrojStanovnika,
                        dominantnaRasa = planeta.DominantnaRasa,
                        drustvenoUredjenje = planeta.DrustvenoUredjenje,
                        imeZvezdanogSistema = planeta.ImeZvezdanogSistema,
                        tipZvezdanogSistema = planeta.TipZvezdanogSistema,
                        x = planeta.X,
                        y = planeta.Y,
                        z = planeta.Z,
                        berilijum = planeta.Berilijum,
                        trilijum = planeta.Trilijum,
                        plutonijum = planeta.Plutonijum,
                        datumKolonizacije = planeta.DatumKolonizacije
                    });
                }

                s.Close();
            }
            catch (Exception ec)
            {
                return ec.Message;
            }

            return planete;
        }

        /*public static PlanetaBasic vratiPlanetu(int id)
        {
            try
            {
                PlanetaBasic planetaBasic= new PlanetaBasic();
                try
                {
                    ISession s = DataLayer.GetSession();

                    Planeta planeta = s.Load<Planeta>(id);
                    planetaBasic = new PlanetaBasic(planeta.Id)
                    galaksijaBasic = new GalaksijaBasic(galaksija.Naziv,
                        galaksija.ProcenjenBrojZvezda,
                        galaksija.ProcenjenBrojPlaneta,
                        galaksija.DominantnaRasa);
                }
                catch (Exception ec)
                {
                    new InnerExceptionHandler().handle(ec);
                }

                return galaksijaBasic;
            }
            catch (Exception ec)
            {
                new InnerExceptionHandler().handle(ec);
            }
            return planeta;
        }*/

        public async static Task<Result<bool, string>> osvojiPlanetuAsync(int idPlanete, int PosadaId)
        {
            ISession? s = null;
            try
            {
                s = DataLayer.GetSession();
                Planeta planeta = await s.GetAsync<Planeta>(idPlanete);
                Posada posada = await s.GetAsync<Posada>(PosadaId);
                if (posada == null || planeta == null)
                {
                    throw new Exception("Ne postoji posada ili planeta");
                }
                Igrac prosliIgrac = planeta.IgracKojiJePoseduje;
                if (prosliIgrac == null)
                {
                    throw new Exception("Izaberite planetu sa nekim igracem");
                }

                Osvajanje osvajanje = new Osvajanje();
                osvajanje.PosadaOsvaja = posada;
                osvajanje.PlanetaOsvojena= planeta;
                osvajanje.PrethodniVlasnik = prosliIgrac;

                await s.SaveAsync(osvajanje);
                await s.FlushAsync();
  
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
            return true;
        }

        public async static Task<Result<bool, string>> obrisiPlanetuAsync(int id)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();

                Planeta p = await s.GetAsync<Planeta>(id);

                s.DeleteAsync(p);
                s.FlushAsync();
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
            return true;

        }


        #endregion
        #region Pojava
        public static async Task<Result<bool, string>> dodajPojavuAsync(PojavaPregled p, int idP)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Planeta planeta = await s.GetAsync<Planeta>(idP);
                Pojava pojava = new Pojava();
                if (planeta == null)
                {
                    throw new Exception("Ne postoji planeta");
                }
                pojava.Naziv = p.naziv;
                pojava.TipPojave = p.tipPojave;
                pojava.IzazivaLiOpasnost = p.izazivaLiOpasnost;
                pojava.PlanetaDeo = planeta;

                await s.SaveAsync(pojava);
                await s.FlushAsync();
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return true;

        }

        public async static Task<Result<PojavaPregled, string>> azurirajPojavuAsync(PojavaPregled p)
        {
            ISession? s = null;
            try
            {
                s = DataLayer.GetSession();
                Pojava pojava = await s.GetAsync<Pojava>(p.naziv);
                if (pojava == null)
                {
                    throw new Exception("Ne postoji pojava");
                }

                pojava.Naziv = p.naziv;
                pojava.TipPojave = p.tipPojave;
                pojava.IzazivaLiOpasnost = p.izazivaLiOpasnost;

                await s.UpdateAsync(pojava);
                await s.FlushAsync();
            
            }
            catch(Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
            return p;
        }
        public static Result<List<PojavaPregled>, string> vratiSvePojavePlanete(int idP)
        {
            List<PojavaPregled> pojave = new List<PojavaPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                Planeta planeta = s.Get<Planeta>(idP);
                if (planeta == null)
                {
                    throw new Exception("Ne postoji planeta");
                }

                IEnumerable<Pojava> svePojave = from p in s.Query<Pojava>()
                                                where p.PlanetaDeo == planeta
                                                select p;

                foreach (Pojava pojava in svePojave)
                {
                    pojave.Add(new PojavaPregled
                    {
                        naziv = pojava.Naziv,
                        tipPojave = pojava.TipPojave,                   
                        izazivaLiOpasnost = pojava.IzazivaLiOpasnost
                    });
                }

                s.Close();
            }
            catch (Exception ec)
            {
                return ec.Message;
            }

            return pojave;
        }

        public async static Task<Result<bool, string>> obrisiPojavuAsync(string naziv)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();

                Pojava o = await s.LoadAsync<Pojava>(naziv);

                await s.DeleteAsync(o);
                await s.FlushAsync();
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return true;
        }

        #endregion
        #region Kvadranti
        public static Result<List<KvadrantPregled>, string> vratiSveKvadranteGalaksije(string nazivG)
        {
            List<KvadrantPregled> kvadranti = new List<KvadrantPregled>();
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();

                if (!(s?.IsConnected ?? false))
                {
                    return "Nemoguće otvoriti sesiju.";
                }

                Galaksija galaksija = s.Get<Galaksija>(nazivG);
                if (galaksija == null)
                    return "Ne postoji galaksija";

                IEnumerable<Kvadrant> sviKvadranti = from k in s.Query<Kvadrant>()
                                                     where k.DeoGalaksije == galaksija
                                                     select k;

                foreach (Kvadrant kvadrant in sviKvadranti)
                {
                    kvadranti.Add(new KvadrantPregled
                    {
                        redniBroj = kvadrant.RedniBroj,
                        procenjeniPrecnik = kvadrant.ProcenjeniPrecnik
                    });
                }
            }
            catch (Exception)
            {
                return "Nemoguće vratiti kvadrante";
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return kvadranti;
        }


        public async static Task<Result<bool, string>>obrisiKvadrantAsync(int id)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();

                Kvadrant o = await s.LoadAsync<Kvadrant>(id);

                s.DeleteAsync(o);
                s.FlushAsync();
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
            return true;

        }

        public static async Task<Result<KvadrantPregled, string>> dodajKvadrantAsync(int precnik, string nazivG)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Galaksija galaksija = s.Load<Galaksija>(nazivG);
                Kvadrant kvadrant = new Kvadrant();
                kvadrant.ProcenjeniPrecnik = precnik;
                kvadrant.DeoGalaksije = galaksija;

                await s.SaveOrUpdateAsync(kvadrant);
                await s.FlushAsync();

                return new KvadrantPregled
                {
                    redniBroj = kvadrant.RedniBroj,
                    procenjeniPrecnik = kvadrant.ProcenjeniPrecnik
                };
            }
            catch (Exception)
            {
                return "Nemoguće dodati kvadrant.";
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
        }

        public async static Task<Result<KvadrantPregled, string>>azurirajKvadrantAsync(KvadrantPregled k)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Kvadrant kvadrant = await s.LoadAsync<Kvadrant>(k.redniBroj);
                kvadrant.ProcenjeniPrecnik = k.procenjeniPrecnik;

                s?.UpdateAsync(kvadrant);
                s?.FlushAsync();
       
            }
            catch (Exception ec)
            {
                return (ec.Message);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return k;
        }
        #endregion
        #region Stanice
        public static async Task<Result<VojnaStanicaPregled, string>> dodajVojnuStanicu(VojnaStanicaPregled vsp, int idp)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Planeta planeta = await s.LoadAsync<Planeta>(idp);

                VojnaStanica vojnaStanica = new VojnaStanica()
                {
                    Naziv = vsp.naziv,
                    BrojLjudi = vsp.brojLjudi,
                    DeoPlanete = planeta,
                    Udaljenost = vsp.udaljenost,
                    Velicina = vsp.velicina
                };
                await s.SaveOrUpdateAsync(vojnaStanica);
                await s.FlushAsync();
            }
            catch (Exception)
            {
                return "Nemoguce je dodati vojnu stanicu!";
            }
            finally
            {
                s.Close();
                s.Dispose();
            }

            return vsp;
        }
        public async static Task<Result<VojnaStanicaPregled, string>> azurirajVojnuStanicu(VojnaStanicaPregled v)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                VojnaStanica vojna = await s.LoadAsync<VojnaStanica>(v.id);
                vojna.Naziv = v.naziv;
                vojna.BrojLjudi = v.brojLjudi;
                vojna.Udaljenost = v.udaljenost;
                vojna.Velicina = v.velicina;

                s?.UpdateAsync(vojna);
                s?.FlushAsync();

            }
            catch (Exception ec)
            {
                return (ec.Message);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return v;
        }
        public async static Task<Result<CivilnaSvemirskaStanicaPregled, string>> azurirajCivilnuStanicu(CivilnaSvemirskaStanicaPregled c)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                CivilnaStanica civilna = await s.LoadAsync<CivilnaStanica>(c.id);
                civilna.Naziv = c.naziv;
                civilna.BrojLjudi = c.brojLjudi;
                civilna.Udaljenost = c.udaljenost;
                civilna.Velicina = c.velicina;
                civilna.Svrha = c.svrha;

                s?.UpdateAsync(civilna);
                s?.FlushAsync();

            }
            catch (Exception ec)
            {
                return (ec.Message);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return c;
        }
        public static async Task<Result<CivilnaSvemirskaStanicaPregled, string>> dodajCivilnuStanicu(CivilnaSvemirskaStanicaPregled csp, int idp)
        {
            ISession? s = null;
            try
            {
                s = DataLayer.GetSession();
                Planeta planeta = await s.LoadAsync<Planeta>(idp);

                CivilnaStanica civilnaStanica = new CivilnaStanica()
                {
                    Naziv = csp.naziv,
                    BrojLjudi = csp.brojLjudi,
                    DeoPlanete = planeta,
                    Svrha = csp.svrha,
                    Udaljenost = csp.udaljenost,
                    Velicina = csp.velicina
                };
                if (civilnaStanica.Svrha != "nauka" && civilnaStanica.Svrha != "trgovina")
                {
                    throw new Exception("Molimo Vas unesite za svrhu civilne stanice ili trgovina ili nauka.");
                }
                await s.SaveOrUpdateAsync(civilnaStanica);
                await s.FlushAsync();
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s.Close();
                s.Dispose();
            }

            return csp;
        }
        public static Result<List<SvemirskaStanicaPregled>, string> vratiSveStanicePlanete(int idPlanete, string tip)
        {
            List<SvemirskaStanicaPregled> stanice = new List<SvemirskaStanicaPregled>();
            try
            {
                ISession session = DataLayer.GetSession();
                Planeta planeta = session.Load<Planeta>(idPlanete);
                IEnumerable<SvemirskaStanica> sveStanice;
                if (tip == "vojna")
                {
                    sveStanice = from b in session.Query<VojnaStanica>()
                                 where b.DeoPlanete == planeta
                                 select b;
                    foreach (VojnaStanica s in sveStanice)
                    {
                        stanice.Add(new VojnaStanicaPregled(
                            s.Id,
                            s.Naziv,
                            s.Udaljenost,
                            s.BrojLjudi,
                            s.Velicina
                        ));
                    }
                }
                else if (tip == "civilna")
                {
                    sveStanice = from b in session.Query<CivilnaStanica>()
                                 where b.DeoPlanete == planeta
                                 select b;
                    foreach (CivilnaStanica s in sveStanice)
                    {
                        stanice.Add(new CivilnaSvemirskaStanicaPregled(
                            s.Id,
                            s.Naziv,
                            s.Udaljenost,
                            s.Svrha,
                            s.BrojLjudi,
                            s.Velicina
                            ));
                    }
                }
                else
                {
                    return "Niste uneli validan tip svemirske stanice, unesite malim slovima ili vojna ili civilna.";
                }
                session.Close();

            }
            catch (Exception ec)
            {
                new InnerExceptionHandler().handle(ec);
            }
            return stanice;
        }

        public async static Task<Result<bool, string>> obrisiStanicu(int id)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                SvemirskaStanica x = await s.LoadAsync<SvemirskaStanica>(id);

                await s.DeleteAsync(x);
                await s.FlushAsync();

            }
            catch (Exception ec)
            {
                return ec.Message + ec.InnerException;
            }
            finally
            {
                s.Close();
                s.Dispose();
            }

            return true;

        }
        #endregion
        #region PrirodniSatelit
        public static async Task<Result<PrirodniSatelitPregled, string>> dodajPrirodniSatelit(PrirodniSatelitPregled psp, int idp)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Planeta planeta = await s.LoadAsync<Planeta>(idp);

                PrirodniSatelit prirodniSatelit = new PrirodniSatelit()
                {
                    Naziv = psp.naziv,
                    Udaljenost = psp.udaljenost,
                    Precnik = psp.precnik,
                    Naseobine = psp.naseobine,
                    KruziOkoPlanete = planeta
                };

                await s.SaveOrUpdateAsync(prirodniSatelit);
                await s.FlushAsync();

            }
            catch (Exception)
            {
                return "Nemoguce dodati prirodni satelit";
            }
            finally
            {
                s.Close();
                s.Dispose();
            }

            return psp;
        }

        public static async Task<Result<PrirodniSatelitPregled, string>> azurirajPrirodniSatelit(PrirodniSatelitPregled psp)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                PrirodniSatelit satelit = await s.LoadAsync<PrirodniSatelit>(psp.naziv);
                satelit.Udaljenost = psp.udaljenost;
                satelit.Precnik = psp.precnik;
                satelit.Naseobine = psp.naseobine;
                             
                await s.SaveOrUpdateAsync(satelit);
                await s.FlushAsync();

            }
            catch (Exception)
            {
                return "Nemoguce dodati prirodni satelit";
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return psp;
        }
        public static Result<List<PrirodniSatelitPregled>, string> vratiSvePrirodneSatelite(int idP)
        {
            List<PrirodniSatelitPregled> prirodniSateliti = new List<PrirodniSatelitPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                Planeta planeta = s.Load<Planeta>(idP);

                IEnumerable<PrirodniSatelit> sviPS = from ps in s.Query<PrirodniSatelit>()
                                                     where ps.KruziOkoPlanete == planeta
                                                     select ps;

                foreach (PrirodniSatelit prirodniSatelit in sviPS)
                {
                    prirodniSateliti.Add(new PrirodniSatelitPregled
                    {
                        naziv = prirodniSatelit.Naziv,
                        udaljenost = prirodniSatelit.Udaljenost,
                        precnik = prirodniSatelit.Precnik,
                        naseobine = prirodniSatelit.Naseobine
                    });
                }

                s.Close();
            }
            catch (Exception ec)
            {
                return ec.Message;
            }

            return prirodniSateliti;
        }
        public async static Task<Result<bool, string>> obrisiPrirodniSatelitAsync(string naziv)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Console.WriteLine(naziv);
                PrirodniSatelit o = await s.LoadAsync<PrirodniSatelit>(naziv);
                Console.WriteLine(o.Naziv);

                await s.DeleteAsync(o);
                await s.FlushAsync();

            }
            catch (Exception ec)
            {
                return ec.Message + ec.InnerException;
            }
            finally
            {
                s.Close();
                s.Dispose();
            }

            return true;

        }
        #endregion
        #region Brodovi

        public static Result<List<BrodPregled>, string> vratiSveBrodovePlanete(int idPlanete, string tip)
        {
            List<BrodPregled> brodovi = new List<BrodPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                Planeta planeta = s.Load<Planeta>(idPlanete);
                IEnumerable<Brod> sviBrodovi;
                if (tip == "borbeni")
                {
                    sviBrodovi = from b in s.Query<BorbeniBrod>()
                                 where b.PlanetaKonstrukcije == planeta
                                 select b;
                    foreach (BorbeniBrod brod in sviBrodovi)
                    {
                        brodovi.Add(new BorbeniBrodPregled(
                            brod.JedinstveniBroj,
                            brod.Naziv,
                            brod.MaxWarpBrzina,
                            brod.FotonskoTorpedo,
                            brod.BrojLaserskihTopova,
                            brod.Tip
                        ));
                    }
                }
                else if(tip == "transportni")
                {
                    sviBrodovi = from b in s.Query<TransportniBrod>()
                                 where b.PlanetaKonstrukcije == planeta
                                 select b;
                    foreach (TransportniBrod brod in sviBrodovi)
                    {
                        brodovi.Add(new TransportniBrodPregled(
                                 brod.JedinstveniBroj,
                                 brod.Naziv,
                                 brod.MaxWarpBrzina,
                                 brod.ZastitnaOtplata,
                                 brod.Nosivost
                             ));
                    }
                }
                else
                {
                    throw new Exception("Brod mora biti 'borbeni' ili 'transportni'");
                }
                s.Close();

            }
            catch (Exception ec)
            {
                return ec.Message;
            }

            return brodovi;

        }
        public static async Task<Result<BorbeniBrodPregled, string>> dodajBorbeniBrod(BorbeniBrodPregled bbp, int idp, int idpos)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Planeta planeta = await s.GetAsync<Planeta>(idp);
                Posada posada = await s.GetAsync<Posada>(idpos);
                if (posada == null || planeta == null)
                {
                    throw new Exception("Ne postoji planeta ili posada");
                }
                if (bbp.tip != "Fregata" && bbp.tip != "Razarac" && bbp.tip != "Krstarica")
                    throw new Exception("Tip mora biti 'Fregata', 'Razarac' ili 'Krstarica'");

                BorbeniBrod borbeniBrod = new BorbeniBrod()
                {
                    BrojLaserskihTopova = bbp.brojLaserskihTopova,
                    FotonskoTorpedo = bbp.fotonskoTorpedo,
                    MaxWarpBrzina = bbp.maxWarpBrzina,
                    Tip = bbp.tip,
                    Naziv = bbp.naziv,
                    PlanetaKonstrukcije = planeta,
                    PosadaKojaPoseduje = posada
                };
                await s.SaveOrUpdateAsync(borbeniBrod);
                await s.FlushAsync();
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s.Close();
                s.Dispose();
            }
            return bbp;

        }
        public async static Task<Result<TransportniBrodPregled, string>> dodajTransportniBrod(TransportniBrodPregled tbp, int idplanete, int idposade)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Planeta planeta = await s.LoadAsync<Planeta>(idplanete);
                Posada posada = await s.LoadAsync<Posada>(idposade);

                TransportniBrod transportniBrod = new TransportniBrod()
                {
                    Naziv = tbp.naziv,
                    MaxWarpBrzina = tbp.maxWarpBrzina,
                    ZastitnaOtplata = tbp.zastitnaOtplata,
                    Nosivost = tbp.nosivost,
                    PlanetaKonstrukcije = planeta,
                    PosadaKojaPoseduje = posada
                };

                s?.SaveOrUpdateAsync(transportniBrod);
                s?.FlushAsync();

            }
            catch (Exception ec)
            {
                return (ec.Message);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return tbp;
        }
        public async static Task<Result<CivilnaSvemirskaStanicaPregled, string>> azurirajTransportniBrod(CivilnaSvemirskaStanicaPregled c)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                CivilnaStanica civilna = await s.LoadAsync<CivilnaStanica>(c.id);
                civilna.Naziv = c.naziv;
                civilna.BrojLjudi = c.brojLjudi;
                civilna.Udaljenost = c.udaljenost;
                civilna.Velicina = c.velicina;
                civilna.Svrha = c.svrha;

                s?.UpdateAsync(civilna);
                s?.FlushAsync();

            }
            catch (Exception ec)
            {
                return (ec.Message);
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return c;
        }

        public async static Task<Result<bool, string>> obrisiBrodAsync(int id)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Brod x = await s.LoadAsync<Brod>(id);

                await s.DeleteAsync(x);
                await s.FlushAsync();

            }
            catch (Exception ec)
            {
                return ec.Message + ec.InnerException;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return true;

        }
        #endregion
        #region Igrac
        public static Result<List<IgracPregled>, string> vratiSveIgrace()
        {
            List<IgracPregled> igraci = new List<IgracPregled>();
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();

                IEnumerable<Igrac> sviIgraci = from o in s.Query<Igrac>()
                                               select o;
  

                foreach (Igrac i in sviIgraci)
                {
                    igraci.Add(new IgracPregled(i.Username,
                        i.Ime,
                        i.Prezime, 
                        i.Pol, 
                        i.Drzava, 
                        i.DatumOtvaranjaNaloga, 
                        i.DatumRodjenja, 
                        i.Email, 
                        i.URLAvatara, 
                        i.Opis));
                }
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
            }

            return igraci;
        }

        public static async Task<Result<bool, string>> dodajIgracaAsync(IgracPregled i)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                bool isAdd = false;

                Igrac o = await s.GetAsync<Igrac>(i.username);
                if (o == null)
                {
                    isAdd = true;
                    Planeta planeta = s.Query<Planeta>()
                        .FirstOrDefault(p => !s.Query<Igrac>().Any(ig => ig.MaticnaPlaneta == p));
                    if (planeta == null)
                    {
                        throw new Exception("Ne postoji dovoljno planeta, kreirati novu planetu pre nego sto napravite igraca");
                    }
                    o = new Igrac();
                    o.MaticnaPlaneta = planeta;
                }

                if ((i.pol != "musko") && (i.pol != "zensko"))
                {
                    throw new Exception("Pol mora biti ili 'musko' ili 'zensko'");
                }

                o.Ime = i.ime;
                o.Prezime = i.prezime;
                o.Pol = i.pol;
                o.Email = i.email;
                o.Opis = i.opis;
                o.URLAvatara = i.urlAvatara;
                o.DatumRodjenja = i.datumRodjenja;
                o.DatumOtvaranjaNaloga = i.datumOtvaranjaNaloga;
                o.Drzava = i.drzava;
                o.Username = i.username;

                if (isAdd)
                    await s.SaveAsync(o);
                else
                    await s.UpdateAsync(o);

                await s.FlushAsync();
 
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
            return true;
        }
   

        public async static Task<Result<bool, string>> obrisiIgracaAsync(string naziv)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();

                Igrac o = await s.LoadAsync<Igrac>(naziv);

                await s.DeleteAsync(o);
                await s.FlushAsync();

            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
            return true;
        }

        public static async Task<Result<int, string>> dodajPosaduIgracuAsync(string username)
        {
            ISession? s = null;
            try
            {
                s = DataLayer.GetSession();
                Igrac igrac = await s.GetAsync<Igrac>(username);
                if (igrac == null)
                {
                    throw new Exception("Ne postoji igrac");
                }
                string sqlQuery = "INSERT INTO POSADA VALUES (DEFAULT)";
                var query = s.CreateSQLQuery(sqlQuery);

                query.ExecuteUpdate();

                Posada posada = s.QueryOver<Posada>()
                                            .OrderBy(p => p.Id).Desc
                                            .Take(1)
                                            .SingleOrDefault();

                igrac.DeoPosade = posada;

                await s.UpdateAsync(igrac);
                await s.FlushAsync();

                return posada.Id;

            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
        }

        public async static Task<Result<bool,string>> dodajPlanetuIgracuAsync(string username, int idp)
        {
            ISession? s = null;
            try
            {
                s = DataLayer.GetSession();

                Igrac igrac = await s.GetAsync<Igrac>(username);
                Planeta planeta = await s.GetAsync<Planeta>(idp);
                if (igrac == null || planeta == null)
                {
                    throw new Exception("planeta ili igrac ne postoje");
                }
                planeta.IgracKojiJePoseduje = igrac;

                await s.UpdateAsync(planeta);
                await s.FlushAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
            return true;
        }
        /*public static IgracBasic vratiIgraca(string naziv)
        {
            IgracBasic igracBasic = new IgracBasic();
            try
            {
                ISession s = DataLayer.GetSession();
                
                o.Ime = i.ime;
                o.Prezime = i.prezime;
                o.Pol = i.pol;
                o.Email = i.email;
                o.Opis = i.opis;
                o.URLAvatara = i.urlAvatara;
                o.DatumRodjenja = i.datumRodjenja;
                o.DatumOtvaranjaNaloga = i.datumOtvaranjaNaloga;
                o.Drzava = i.drzava;
                o.MaticnaPlaneta = p;
                o.Username = i.username;
                s.SaveOrUpdate(o);   
                
                Igrac igrac = s.Load<Igrac>(naziv);
                igracBasic = new IgracBasic(igrac.Username, igrac.Ime, igrac.Prezime, igrac.Pol, igrac.Drzava, igrac.DatumOtvaranjaNaloga, igrac.DatumRodjenja, igrac.Email, igrac.URLAvatara, igrac.Opis, igrac.DeoPosade, igrac.MaticnaPlaneta, igrac.DeoSaveza);
            }
            catch (Exception ec)
            {
                new InnerExceptionHandler().handle(ec);
            }

            return igracBasic;
        }
        */
        #endregion
        #region Savez
        public static Result<List<SavezPregled>, string> vratiSveSaveze()
        {
            List<SavezPregled> savezi = new List<SavezPregled>();
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();

                if (!(s?.IsConnected ?? false))
                {
                    return "Nemoguće otvoriti sesiju.";
                }

                IEnumerable<Savez> sviSavezi = from o in s.Query<Savez>()
                                               select o;

                foreach (Savez savez in sviSavezi)
                {
                    Console.WriteLine(savez.Naziv);
                    savezi.Add(new SavezPregled(savez.Naziv, savez.DatumFormiranja));
                }

            }
            catch (Exception)
            {
                return "Nemoguće vratiti sve saveze.";
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return savezi;
        }

        public async static Task<Result<bool, string>> dodajSavezAsync(string nazivS)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();

                Savez o = new Savez();

                o.Naziv = nazivS;

                await s.SaveOrUpdateAsync(o);
                await s.FlushAsync();

            }
            catch (Exception)
            {
                return "Nemoguće dodati savez.";
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return true;
        }

        public async static Task<Result<bool, string>> obrisiSavezAsync(string naziv)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Savez o = await s.LoadAsync<Savez>(naziv);

                await s.DeleteAsync(o);
                await s.FlushAsync();

            }
            catch (Exception ec)
            {
                return ec.Message + ec.InnerException;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }

            return true;

        }

        public async static Task<Result<bool, string>> pridruziSavezeAsync(string nazivS1, string nazivS2)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Savez savez1 = await s.GetAsync<Savez>(nazivS1);
                Savez savez2 = await s.GetAsync<Savez>(nazivS2);
                if (savez1 == null || savez2 == null)
                {
                    throw new Exception("Ne postoji savez");
                }

                savez2.DeoSaveza = savez1;
                await s.SaveOrUpdateAsync(savez2);
                await s.FlushAsync();

            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
            return true;
        }

        public async static Task<Result<bool, string>> dodajClanaAsync(string nazivS, string nazivI)
        {
            ISession? s = null;

            try
            {
                s = DataLayer.GetSession();
                Savez savez = await s.GetAsync<Savez>(nazivS);
                Igrac igrac = await s.GetAsync<Igrac>(nazivI);
                if (savez == null || igrac == null)
                {
                    throw new Exception("Ne postoji savez ili igrac");
                }

                igrac.DeoSaveza = savez;
                await s.SaveOrUpdateAsync(igrac);
                await s.FlushAsync();

            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
            return true;
        }

        public static async Task<Result<int, string>> dodajPosaduSavezuAsync(string naziv)
        {
            ISession? s = null;
            try
            {
                s = DataLayer.GetSession();
                Savez savez = await s.GetAsync<Savez>(naziv);
                if (savez == null)
                {
                    throw new Exception("Ne postoji savez");
                }

                string sqlQuery = "INSERT INTO POSADA VALUES (DEFAULT)";
                var query = s.CreateSQLQuery(sqlQuery);

                query.ExecuteUpdate();

                Posada posada = s.QueryOver<Posada>()
                                            .OrderBy(p => p.Id).Desc
                                            .Take(1)
                                            .SingleOrDefault();

                savez.DeoPosade = posada;

                await s.UpdateAsync(savez);
                await s.FlushAsync();

                return posada.Id;

            }
            catch (Exception ec)
            {
                return ec.Message;
            }
            finally
            {
                s?.Close();
                s?.Dispose();
            }
       
        }
        #endregion
    }
}
