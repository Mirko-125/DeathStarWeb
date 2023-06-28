using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStar_new.Entiteti
{

    //iskoristiti guid za insertovanje. 

    public abstract class Brod
    {
        public virtual int JedinstveniBroj { get; protected set; }
        public virtual string Naziv { get; set; }
        public virtual double MaxWarpBrzina { get; set; }
        public virtual Planeta PlanetaKonstrukcije { get; set; }
        public virtual Posada PosadaKojaPoseduje { get; set; }
        public Brod()
        {

        }
    }

    public class BorbeniBrod : Brod
    {
        public virtual bool FotonskoTorpedo { get; set; }
        public virtual int BrojLaserskihTopova { get; set; }
        public virtual string Tip { get; set; }
    }
    public class TransportniBrod : Brod
    {
        public virtual bool ZastitnaOtplata { get; set; }
        public virtual double Nosivost { get; set; }
    }
}