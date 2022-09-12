using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.model
{
    [Table("DriverLicenses")]
    public class DriverLicense : INotifyPropertyChanged, IDataErrorInfo
    {
        [Key]
        [Required]
        public int DlId { get; set; }
        [Required, MaxLength(7)]
        private string? category1;
        [MaxLength(7)]
        private string? category2;
        [MaxLength(160)]
        public string? Description { get; set; }
        [ForeignKey("EmployeeID")]
        public int EmployeeID { get; set; }

        public virtual Employee? DLEmployee { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;


        public string? Category1
        {
            get
            {
                return category1;
            }
            set
            {
                if (category1 != value)
                {
                    category1 = value;
                    NotifyPropertyChanged(category1);
                }
            }
        }
        public string? Category2
        {
            get
            {
                return category2;
            }
            set
            {
                if (category2 != value)
                {
                    category2 = value;
                    NotifyPropertyChanged(category2);
                }
            }
        }


        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
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
                    case "Pesel":
                        if (string.IsNullOrEmpty(this.DlId.ToString()) || this.DlId < 0)
                            return "Jeszcze nie wymyśliłem jak zwalidować ID prawa jazdy";
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
