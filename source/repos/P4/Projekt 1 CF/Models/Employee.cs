using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_1_CF
{
    [Table("Employees")]
    public partial class Employee
    {
        public Employee()
        {
            EmployeeTrip = new HashSet<Trip>();
        }

        [Key]
        [Required]
        public int EmployeeID { get; set; }
        [Required, MaxLength(11)]
        public long PESEL { get; set; }
        [Required, MaxLength(30)]
        public string firstName { get; set; } = null!;
        [Required, MaxLength(30)]
        public string lastName { get; set; } = null!;
        [StringLength(20)]
        public string? gender { get; set; }
        [Required, MaxLength(30)]
        public string jobTitle { get; set; } = null!;
        //[ForeignKey("TripID")]
        public virtual ICollection<Trip> EmployeeTrip { get; set; }
    }
}
