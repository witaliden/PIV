using System;
using System.Collections.Generic;

namespace Project_1.Models
{
    public partial class Przejazdy
    {
        public int PrzejazdId { get; set; }
        public string Vin { get; set; } = null!;
        public int PracownikId { get; set; }
        public string? CelWyjazdu { get; set; }
        public DateTime DataCzasOdbioru { get; set; }
        public DateTime? DataCzasZwrotu { get; set; }
        public int StanLicznikaPrzed { get; set; }
        public int? StanLicznikaPo { get; set; }

        public virtual Pracownicy Pracownik { get; set; } = null!;
        public virtual Samochody VinNavigation { get; set; } = null!;

        public override string ToString()
        {
            return $"Przejazd o ID: {this.PrzejazdId} od {this.DataCzasOdbioru}" +
                $"\nID pracownika: {this.PracownikId}\n";
        }
    }
}
