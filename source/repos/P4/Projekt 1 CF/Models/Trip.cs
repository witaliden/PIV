using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_1_CF
{
    [Table("Trips")]
    public partial class Trip
    {
        [Key]
        public int TripID { get; set; }
        [Required, StringLength(17)]
        [ForeignKey("VIN")]
        public string VIN { get; set; } = null!;
        [ForeignKey("EmployeeID")]
        public int EmployeeID { get; set; }
        [StringLength(160)]
        public string? TripPurpose { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime TakeDateTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReturnDateTime { get; set; }
        [Required, MaxLength(9)]
        public int CounterBefore { get; set; }
        [Required, MaxLength(9)]
        public int? CounterAfter { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual Car VinNavigation { get; set; } = null!;

        public override string ToString()
        {
            return $"Przejazd o ID: {this.TripID} od {this.TakeDateTime}" +
                $"\nID pracownika: {this.Employee.EmployeeID}\n";
        }
    }
}
