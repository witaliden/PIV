using System;
using System.Collections.Generic;

namespace Project_1.Models
{
    public partial class Pracownicy
    {
        public Pracownicy()
        {
            Przejazdies = new HashSet<Przejazdy>();
        }

        public int PracownikId { get; set; }
        public string Pesel { get; set; } = null!;
        public string Imie { get; set; } = null!;
        public string Nazwisko { get; set; } = null!;
        public string? Plec { get; set; }
        public string Stanowisko { get; set; } = null!;

        public virtual ICollection<Przejazdy> Przejazdies { get; set; }
    }
}
