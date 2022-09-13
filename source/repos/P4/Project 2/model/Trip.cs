using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

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

        #region setters and getters

        public string VIN
        {
            get
            {
                return vin;
            }
            set
            {
                if (vin != value)
                {
                    vin = value;
                    OnPropertyChanged();
                }
            }
        }
        public int EmployeeID
        {
            get
            {
                return employeeID;
            }
            set
            {
                if (employeeID != value)
                {
                    employeeID = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TripPurpose
        {
            get { return tripPurpose; }
            set
            {
                if (tripPurpose != value)
                {
                    tripPurpose = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? ReturnDateTime
        {
            get { return returnDateTime; }
            set
            {
                if (returnDateTime != value)
                {
                    returnDateTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? CounterAfter
        {
            get { return counterAfter; }
            set
            {
                if (counterAfter != value)
                {
                    counterAfter = value;
                    OnPropertyChanged(CounterAfter.ToString());
                }
            }
        }

        #endregion setters and getters

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
                        if (ReturnDateTime < TakeDateTime || ReturnDateTime != null)
                            return "Data zwrotu nie może nastąpić przed datą wypożyczenia";
                        break;
                    case "CounterAfter":
                        if (CounterAfter < CounterBefore)
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
            return $"Przejazd o ID: {TripID} od {TakeDateTime}" +
                $"\nID pracownika: {Employee.EmployeeID}\n";
        }
    }
}
