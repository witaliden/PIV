using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.model
{
    [Table("Employees")]
    public class Employee
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
        public string FirstName { get; set; } = null!;
        [Required, MaxLength(30)]
        public string LastName { get; set; } = null!;
        [StringLength(20)]
        public string? Gender { get; set; }
        [Required, MaxLength(30)]
        public string JobTitle { get; set; } = null!;
        public string City { get; set; }
        public string Street { get; set; }
        public string Phone { get; set; }

        [ForeignKey("Dl_Id")]
        public int Dl_Id;
        public virtual DriverLicense DriverLicense { get; set; }
        public virtual ICollection<Trip> EmployeeTrip { get; set; }
    }
}