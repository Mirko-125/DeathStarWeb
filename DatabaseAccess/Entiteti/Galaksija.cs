﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DeathStar_new.Entiteti
{
    public class Galaksija
    {
        public virtual string Naziv { get; set; }
        public virtual Int64 ProcenjenBrojZvezda{ get; set; }
        public virtual Int64 ProcenjenBrojPlaneta{ get; set; }
        public virtual string DominantnaRasa { get; set; }
        public virtual IList<Planeta> Planete{ get; set; }
        public virtual IList<Kvadrant> Kvadranti{ get; set; }

        public Galaksija()
        {
            Planete = new List<Planeta>();
            Kvadranti = new List<Kvadrant>();
        }
    }
}


