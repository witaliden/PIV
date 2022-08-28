using System;
using System.Collections.Generic;

namespace Project_1.Models
{
    public partial class Samochody
    {
        public Samochody()
        {
            samochodPrzejazd = new HashSet<Przejazdy>();
        }

        public string Vin { get; set; } = null!;
        public string? Dostepnosc { get; set; }
        public string NumerRejestracyjny { get; set; } = null!;
        public string? Marka { get; set; }
        public string? Model { get; set; }
        public short? PojemnoscSilnika { get; set; }

        public virtual ICollection<Przejazdy> samochodPrzejazd { get; set; }
    }
}
