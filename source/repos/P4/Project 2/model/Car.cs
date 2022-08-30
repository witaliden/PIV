using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.model
{
    [Table("Cars")]
    public class Car : INotifyPropertyChanged, IDataErrorInfo
    {
        [Key]
        [Required, StringLength(17)]
        public string VIN { get; set; } = null!;
        [Required]
        public string availability;


        [Required, MaxLength(9)]
        private string? regNum;

        [Required, MaxLength(30)]
        public string? brand;

        [Required, MaxLength(30)]
        public string? model;

        [Required, MaxLength(4)]
        public short engineCapacity;

        
        /*public int OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public virtual CarOwner CarOwner { get; set; } = null!;*/
        public virtual ICollection<Trip> CarTrip { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;


        #region construktors

        public string Model
        {
            get
            {
                return this.model;
            }
            set
            {
                if (this.model != value)
                {
                    this.model = value;
                    this.NotifyPropertyChanged(Model);
                }
            }
        }
        public string Brand
        {
            get
            {
                return this.brand;
            }
            set
            {
                if (this.brand != value)
                {
                    this.brand = value;
                    this.NotifyPropertyChanged(Brand);
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
                    this.NotifyPropertyChanged(RegNum);
                }
            }
        }

        public short EngineCapacity
        {
            get { return this.engineCapacity; }
            set
            {
                if (this.engineCapacity != value)
                {
                    this.engineCapacity = value;
                    this.NotifyPropertyChanged(EngineCapacity.ToString());
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
                    this.NotifyPropertyChanged(Availability);
                }
            }
        }

        #endregion construktors

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "VIN":
                        if (string.IsNullOrEmpty(this.VIN) || this.VIN.Length != 17)
                            return "VIN powinien zawierać 17 znaków";
                        break;
                    case "Brand":
                        if (string.IsNullOrEmpty(this.Brand))
                            return "Podaj markę";
                        break;
                    case "Model":
                        if (string.IsNullOrEmpty(this.Model))
                            return "Podaj model";
                        break;
                    case "RegNum":
                        if (string.IsNullOrEmpty(this.RegNum) || this.RegNum.Length < 3 || this.RegNum.Length > 7 )
                            return "Podaj numer rejestracyjny";
                        break;
                    case "EngineCapacity":
                        if (this.EngineCapacity == 0 || this.EngineCapacity < 800 || this.EngineCapacity > 8000)
                            return "Podaj pojemność silnika";
                        break;
                }

                // If there's no error, null gets returned
                return null;
            }
        }
        #endregion IDataErrorInfo Members
    }

}
