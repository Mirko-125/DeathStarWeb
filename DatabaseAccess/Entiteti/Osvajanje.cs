﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStar_new.Entiteti
{
    public class Osvajanje
    {
        public virtual int Id { get; protected set; }
        public virtual DateTime DatumOsvajanja { get; set; }
        public virtual Posada PosadaOsvaja{ get; set; }
        public virtual Igrac PrethodniVlasnik { get; set; }
        public virtual Planeta PlanetaOsvojena { get; set; }
    }
}
