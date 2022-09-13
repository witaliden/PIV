using PropertyChanged;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.model
{
    [AddINotifyPropertyChangedInterface]
    [Table("Employees")]
    public class Employee : INotifyPropertyChanged, IDataErrorInfo
    {
        public Employee()
        {
            EmployeeTrip = new HashSet<Trip>();
        }

        [Key]
        [Required]
        public int EmployeeID { get; set; }
        [Required, MinLength(11), MaxLength(11)]
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

        [ForeignKey("Dl_Id")]
        public int Dl_Id;
        public virtual DriverLicense DriverLicense { get; set; }
        public virtual ICollection<Trip> EmployeeTrip { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;


        #region setters and getters

        public long Pesel
        {
            get
            {
                return pesel;
            }
            set
            {
                if (pesel != value)
                {
                    pesel = value;
                    NotifyPropertyChanged(Pesel.ToString());
                }
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    NotifyPropertyChanged(FirstName);
                }
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    NotifyPropertyChanged(lastName);
                }
            }
        }
        #endregion setters and getters

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
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
                    case "Pesel":
                        if (string.IsNullOrEmpty(Pesel.ToString()) || Pesel < 10000000000 || Pesel < 99999999999)
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