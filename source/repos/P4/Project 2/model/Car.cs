using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.model
{
    [Table("Cars")]
    public class Car : INotifyPropertyChanged
    {
        [Key]
        [Required, StringLength(17)]
        public string VIN { get; set; } = null!;
        [Required]
        public string availability;


        [Required, MaxLength(9)]
        private string regNum;

        [Required, MaxLength(30)]
        public string? Brand { get; set; }

        [Required, MaxLength(30)]
        public string? model;

        [Required, MaxLength(4)]
        public short? EngineCapacity { get; set; }

        //[ForeignKey("OwnerID")]
        //public int OwnerID { get; set; }
        //public virtual CarOwner CarOwner { get; set; } = null!;
        public virtual ICollection<Trip> CarTrip { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;


        public string Model
        {
            get
            {
                return this.model;
            }
            set
            {
                if ((this.model != value))
                {
                    this.model = value;
                    this.NotifyPropertyChanged("model");
                }
            }
        }
        public string RegNum
        {
            get { return this.regNum; }
            set
            {
                if (this.regNum != value)
                {
                    this.regNum = value;
                    this.NotifyPropertyChanged(nameof(RegNum));
                }
            }
        }

        public string Availability
        {
            get { return this.availability; }
            set
            {
                if (this.availability != value)
                {
                    this.availability = value;
                    this.NotifyPropertyChanged(nameof(availability));
                }
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
