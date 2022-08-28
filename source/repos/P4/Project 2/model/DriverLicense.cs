using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.model
{
    [Table("DriverLicenses")]
    public class DriverLicense
    {
        [Key]
        [Required]
        public int Dl_Id { get; set; }
        [Required, MaxLength(7)]
        public string? Category1 { get; set; }
        [MaxLength(7)]
        public string? Category2 { get; set; }
        [MaxLength(160)]
        public string? Description { get; set; }
        [ForeignKey("EmployeeID")]
        public int EmployeeID { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
