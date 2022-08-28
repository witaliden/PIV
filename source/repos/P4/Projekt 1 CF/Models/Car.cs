using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_1_CF
{
    [Table("Cars")]
    public partial class Car
    {
        [Key]
        [Required, MaxLength(17)]
        public string VIN { get; set; } = null!;
        [Required]
        public string Status { get; set; }

        [Required, MaxLength(9)]
        public string regNum { get; set; } = null!;
        
        [MaxLength(30)]
        public string? Brand { get; set; }
        
        [MaxLength(30)]
        public string? Model { get; set; }
        
        [Required, MaxLength(4)]
        public short? EngineCapacity { get; set; }
        public virtual ICollection<Trip> carTrip { get; set; }

    }

/*    public class Availability
    {
        enum available
        {
            Dostępny = 0,
            Wypożyczony = 1,
            Archiwalny = 2
        }
    }*/
}
