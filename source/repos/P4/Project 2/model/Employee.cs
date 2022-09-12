using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.model
{
    [Table("Employees")]
    public class Employee : INotifyPropertyChanged, IDataErrorInfo
    {
        public Employee()
        {
            EmployeeTrips = new HashSet<Trip>();
        }

        [Key]
        [Required]
        public int EmployeeID { get; set; }
        [Required, MaxLength(11)]
        public long pesel;
        [Required, MaxLength(30)]
        public string firstName;
        [Required, MaxLength(30)]
        public string lastName;
        [StringLength(20)]
        public string? Gender { get; set; }
        [Required, MaxLength(30)]
        public string JobTitle { get; set; } = null!;
        public string City { get; set; }
        public string Street { get; set; }
        public string Phone { get; set; }

        [ForeignKey("employeeDlId")]
        public int employeeDlId;
        public virtual DriverLicense EmployeeDriverLicense { get; set; }
        public virtual ICollection<Trip> EmployeeTrips { get; set; }

        #region getter-setter NotifyPropertyChanged
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public long Pesel
        {
            get
            {
                return this.pesel;
            }
            set
            {
                if (this.pesel != value)
                {
                    this.pesel = value;
                    this.NotifyPropertyChanged(Pesel.ToString());
                }
            }
        }
        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    this.NotifyPropertyChanged(FirstName);
                }
            }
        }
        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    this.NotifyPropertyChanged(LastName);
                }
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        #endregion getter-setter NotifyPropertyChanged

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
                    case "Pesel":
                        if (string.IsNullOrEmpty(this.Pesel.ToString()) || this.Pesel < 10000000000 || this.Pesel < 99999999999)
                            return "Podaj numer PESEL, 11 cyfr";
                        break;
                    default:
                        break;
                }

                // If there's no error, null gets returned
                return null;
            }
        }
        #endregion IDataErrorInfo Members

    }
}