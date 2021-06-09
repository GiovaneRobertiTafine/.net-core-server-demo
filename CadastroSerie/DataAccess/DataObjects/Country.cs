using System;
using System.Collections.Generic;

#nullable disable

namespace CadastroSerie.DataAccess.DataObjects
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Countrylanguages = new HashSet<Countrylanguage>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Region { get; set; }
        public float SurfaceArea { get; set; }
        public short? IndepYear { get; set; }
        public int Population { get; set; }
        public float? LifeExpectancy { get; set; }
        public float? Gnp { get; set; }
        public float? Gnpold { get; set; }
        public string LocalName { get; set; }
        public string GovernmentForm { get; set; }
        public string HeadOfState { get; set; }
        public int? Capital { get; set; }
        public string Code2 { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Countrylanguage> Countrylanguages { get; set; }
    }
}
