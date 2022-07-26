using System;
using System.Collections.Generic;

namespace Project_1.Models
{
    public partial class WTrkaciePrzejazdu
    {
        public string Pracownik { get; set; } = null!;
        public string? CelWyjazdu { get; set; }
        public string? Samochód { get; set; }
    }
}
