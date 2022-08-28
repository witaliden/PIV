using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.model
{
    [Table("Cars")]
    public class Car
    {
        [Key]
        [Required, StringLength(17)]
        public string VIN { get; set; } = null!;
        [Required]
        public string Availability { get; set; }

        [Required, MaxLength(9)]
        public string RegNum { get; set; } = null!;

        [MaxLength(30)]
        public string? Brand { get; set; }

        [MaxLength(30)]
        public string? Model { get; set; }

        [Required, MaxLength(4)]
        public short? EngineCapacity { get; set; }

        //[ForeignKey("OwnerID")]
        //public int OwnerID { get; set; }
        //public virtual CarOwner CarOwner { get; set; } = null!;
        public virtual ICollection<Trip> CarTrip { get; set; }
    }
}
