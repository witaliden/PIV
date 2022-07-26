using System;
using System.Collections.Generic;

namespace Project_1.Models
{
    public partial class Samochody
    {
        public Samochody()
        {
            Przejazdies = new HashSet<Przejazdy>();
        }

        public string Vin { get; set; } = null!;
        public string? Dostepnosc { get; set; }
        public string NumerRejestracyjny { get; set; } = null!;
        public string? Marka { get; set; }
        public string? Model { get; set; }
        public short? PojemnoscSilnika { get; set; }

        public virtual ICollection<Przejazdy> Przejazdies { get; set; }
    }
}
