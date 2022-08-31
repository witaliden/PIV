using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.model
{
    [Table("Trips")]
    public class Trip : INotifyPropertyChanged, IDataErrorInfo
    {
        [Key]
        [Required]
        public int TripID { get; set; }
        [Required, StringLength(17)]
        [ForeignKey("VIN")]
        public string vin;
        [ForeignKey("EmployeeID")]
        public int employeeID;
        [StringLength(160)]
        public string? tripPurpose;
        [Required]
        [DataType(DataType.Date)]
        public DateTime TakeDateTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime? returnDateTime;
        [Required, MaxLength(9)]
        public int CounterBefore { get; set; }
        [MaxLength(9)]
        public int? counterAfter;
        public virtual Employee Employee { get; set; } = null!;
        public virtual Car VinNavigation { get; set; } = null!;

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();

        public event PropertyChangedEventHandler? PropertyChanged;

        #region construktors

        public string VIN
        {
            get
            {
                return this.vin;
            }
            set
            {
                if (this.vin != value)
                {
                    this.vin = value;
                    this.NotifyPropertyChanged(VIN);
                }
            }
        }
        public int EmployeeID
        {
            get
            {
                return this.employeeID;
            }
            set
            {
                if (this.employeeID != value)
                {
                    this.employeeID = value;
                    this.NotifyPropertyChanged(EmployeeID.ToString());
                }
            }
        }
        public string TripPurpose
        {
            get { return this.tripPurpose; }
            set
            {
                if (this.tripPurpose != value)
                {
                    this.tripPurpose = value;
                    this.NotifyPropertyChanged(TripPurpose);
                }
            }
        }

        public DateTime? ReturnDateTime
        {
            get { return this.returnDateTime; }
            set
            {
                if (this.returnDateTime != value)
                {
                    this.returnDateTime = value;
                    this.NotifyPropertyChanged(ReturnDateTime.ToString());
                }
            }
        }

        public int? CounterAfter
        {
            get { return this.counterAfter; }
            set
            {
                if (this.counterAfter != value)
                {
                    this.counterAfter = value;
                    this.NotifyPropertyChanged(CounterAfter.ToString());
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
                    case "ReturnDateTime":
                        if (this.ReturnDateTime < this.TakeDateTime || this.ReturnDateTime != null)
                            return "Data zwrotu nie może nastąpić przed datą wypożyczenia";
                        break;
                    case "CounterAfter":
                        if (this.CounterAfter < this.CounterBefore)
                            return "Stan licznika po podróży nie może być niższy niż przed";
                        break;
                    default:
                        break;
                }

                // If there's no error, null gets returned
                return null;
            }
        }
        #endregion IDataErrorInfo Members

        public override string ToString()
        {
            return $"Przejazd o ID: {this.TripID} od {this.TakeDateTime}" +
                $"\nID pracownika: {this.Employee.EmployeeID}\n";
        }
    }
}
