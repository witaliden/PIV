using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.model
{
    [Table("CarOwners")]
    public class CarOwner
    {
        CarOwner()
        {
            OwnerCars = new HashSet<Car>();
        }

        [Key]
        [Required]
        public int OwnerId { get; set; }
        [Required, MaxLength(14)]
        public long OwnerTaxNumber { get; set; }
        [Required, StringLength(160)]
        public string OwnerName { get; set; }
        public virtual ICollection<Car> OwnerCars { get; set; } = null!;
    }
}
