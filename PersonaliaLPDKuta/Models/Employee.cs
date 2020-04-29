using PersonaliaLPDKuta.Utilities;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PersonaliaLPDKuta.Models
{
    public class Employee : ValidationBase, IEquatable<Employee>
    {
        private int id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    RaisePropertyChanged(ref id, value);
                }
            }
        }

        private string employeeNumber;
        public string EmployeeNumber
        {
            get { return employeeNumber; }
            set
            {
                if (value != employeeNumber)
                {
                    RaisePropertyChanged(ref employeeNumber, value);
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    RaisePropertyChanged(ref name, value);
                }
            }
        }

        private string birthPlace;
        public string BirthPlace
        {
            get { return birthPlace; }
            set
            {
                if (value != birthPlace)
                {
                    RaisePropertyChanged(ref birthPlace, value);
                }
            }
        }

        private DateTime? birthDate;
        public DateTime? BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value != birthDate)
                {
                    RaisePropertyChanged(ref birthDate, value);
                }
            }
        }

        private int gender;
        public int Gender
        {
            get { return gender; }
            set
            {
                if (value != gender)
                {
                    RaisePropertyChanged(ref gender, value);
                }
            }
        }
        private string homeAddress;
        public string HomeAddress
        {
            get { return homeAddress; }
            set
            {
                if (value != homeAddress)
                {
                    RaisePropertyChanged(ref homeAddress, value);
                }
            }
        }

        private string homePhone;
        public string HomePhone
        {
            get { return homePhone; }
            set
            {
                if (value != homePhone)
                {
                    RaisePropertyChanged(ref homePhone, value);
                }
            }
        }

        private string mobilePhone;
        public string MobilePhone
        {
            get { return mobilePhone; }
            set
            {
                if (value != mobilePhone)
                {
                    RaisePropertyChanged(ref mobilePhone, value);
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (value != email)
                {
                    RaisePropertyChanged(ref email, value);
                }
            }
        }

        private string npwpNumber;
        public string NpwpNumber
        {
            get { return npwpNumber; }
            set
            {
                if (value != npwpNumber)
                {
                    RaisePropertyChanged(ref npwpNumber, value);
                }
            }
        }

        private string identityNumber;
        public string IdentityNumber
        {
            get { return identityNumber; }
            set
            {
                if (value != identityNumber)
                {
                    RaisePropertyChanged(ref identityNumber, value);
                }
            }
        }

        private int status;
        public int Status
        {
            get { return status;  }
            set
            {
                if (value != status)
                {
                    RaisePropertyChanged(ref status, value);
                }
            }
        }

        private DateTime? startDate;
        public DateTime? StartDate
        {
            get { return startDate; }
            set
            {
                if (value != startDate)
                {
                    RaisePropertyChanged(ref startDate, value);
                }
            }
        }

        private int totalLiabilities;
        public int TotalLiabilities
        {
            get { return totalLiabilities; }
            set
            {
                if (value != totalLiabilities)
                {
                    RaisePropertyChanged(ref totalLiabilities, value);
                }
            }
        }

        private int cashReceiptLimit;
        public int CashReceiptLimit
        {
            get { return cashReceiptLimit; }
            set
            {
                if (value != cashReceiptLimit)
                {
                    RaisePropertyChanged(ref cashReceiptLimit, value);
                }
            }
        }

        private int salary;
        public int Salary
        {
            get { return salary; }
            set
            {
                if (value != salary)
                {
                    RaisePropertyChanged(ref salary, value);
                }
            }
        }

        private string imageName;
        public string ImageName
        {
            get { return imageName; }
            set
            {
                if (value != imageName)
                {
                    RaisePropertyChanged(ref imageName, value);
                }
            }
        }

        private string imagePathTemp;
        public string ImagePathTemp
        {
            get { return imagePathTemp; }
            set
            {
                if (value != imagePathTemp)
                {
                    RaisePropertyChanged(ref imagePathTemp, value);
                }
            }
        }

        private int deleted;
        public int Deleted
        {
            get { return deleted; }
            set
            {
                if (value != deleted)
                {
                    deleted = value;
                    RaisePropertyChanged(ref deleted, value);
                }
            }
        }
        public ImageSource Image
        {
            get
            {
                if (File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\variant\" + Id + ".jpg"))
                {
                    return BitmapFromUri(new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\variant\" + Id + ".jpg"));
                }
                else
                {
                    return new BitmapImage(new Uri("/iPos;component/Images/no-image-blue.png", UriKind.Relative));
                }
            }
        }

        public static ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

        public void Insert()
        {
            SQLiteDBHelper.Database.Insert(this);
        }
        public void Update()
        {
            SQLiteDBHelper.Database.Execute("update " +
                "Employee set " +
                "Name = ?, " +
                "BirthPlace = ?, " +
                "BirthDate = ?,  " +
                "Gender = ?,  " +
                "HomeAddress = ?,  " +
                "HomePhone = ?,  " +
                "MobilePhone = ?,  " +
                "Email = ?,  " +
                "NpwpNumber = ?,  " +
                "IdentityNumber = ?,  " +
                "Status = ?,  " +
                "StartDate = ?,  " +
                "TotalLiabilities = ?,  " +
                "CashReceiptLimit = ?,  " +
                "Salary = ?,  " +
                "ImageName = ?  " +
                "where Id = ?", 
                Name, 
                BirthPlace, 
                BirthDate, 
                Gender, 
                HomeAddress, 
                HomePhone, 
                MobilePhone, 
                Email, 
                NpwpNumber, 
                IdentityNumber, 
                Status, 
                StartDate, 
                TotalLiabilities, 
                CashReceiptLimit, 
                Salary,
                ImageName, 
                Id);
        }
        public void Delete()
        {
            Deleted = 1;
            SQLiteDBHelper.Database.Execute("update Employee set Deleted = 1 where Id = ?", Id);
        }

        public void Copy(Employee oEmployee)
        {
            if (oEmployee != null)
            {
                EmployeeNumber = oEmployee.EmployeeNumber;
                Name = oEmployee.Name;
                BirthPlace = oEmployee.BirthPlace;
                BirthDate = oEmployee.BirthDate;
                Gender = oEmployee.Gender;
                HomeAddress = oEmployee.HomeAddress;
                HomePhone = oEmployee.HomePhone;
                MobilePhone = oEmployee.MobilePhone;
                Email = oEmployee.Email;
                NpwpNumber = oEmployee.NpwpNumber;
                IdentityNumber = oEmployee.IdentityNumber;
                Status = oEmployee.Status;
                StartDate = oEmployee.StartDate;
                TotalLiabilities = oEmployee.TotalLiabilities;
                CashReceiptLimit = oEmployee.CashReceiptLimit;
                Salary = oEmployee.Salary;
                ImageName = oEmployee.ImageName;
                Id = oEmployee.Id;
            }
        }

        public override string ToString()
        {
            return Name == null ? String.Empty : Name.ToString();
        }

        public bool Equals(Employee p)
        {
            return (Id == p.Id);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Employee otherObject))
                return false;
            else
                return Equals(otherObject);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override IEnumerable GetErrors([CallerMemberName] string propertyName = null)
        {
            foreach (var obj in base.GetErrors(propertyName))
            {
                yield return obj;
            }

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(Name))
            {
                if (string.IsNullOrWhiteSpace(name))
                    yield return "Nama tidak boleh kosong";
            }
            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(EmployeeNumber))
            {
                if (string.IsNullOrWhiteSpace(employeeNumber))
                    yield return "No. Pegawai tidak boleh kosong";
            }
            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(BirthPlace))
            {
                if (string.IsNullOrWhiteSpace(birthPlace))
                    yield return "Tempat Lahir tidak boleh kosong";
            }
            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(BirthDate))
            {
                if (!birthDate.HasValue)
                    yield return "Tanggal Lahir tidak boleh kosong";
            }
            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(HomeAddress))
            {
                if (string.IsNullOrWhiteSpace(homeAddress))
                    yield return "Alamat Rumah tidak boleh kosong";
            }
            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(NpwpNumber))
            {
                if (string.IsNullOrWhiteSpace(npwpNumber))
                    yield return "NPWP tidak boleh kosong";
            }
            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(IdentityNumber))
            {
                if (string.IsNullOrWhiteSpace(identityNumber))
                    yield return "KTP tidak boleh kosong";
            }
            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(StartDate))
            {
                if (!startDate.HasValue)
                    yield return "Tanggang Mulai tidak boleh kosong";
            }
            //if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(TotalLiabilities))
            //{
            //    if (totalLiabilities == null)
            //        yield return "Jumlah Tanggungan tidak boleh kosong";
            //}
            //if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(CashReceiptLimit))
            //{
            //    if (cashReceiptLimit == null)
            //        yield return "Limit Kasbon tidak boleh kosong";
            //}
            //if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(Salary))
            //{
            //    if (salary == null)
            //        yield return "Gaji tidak boleh kosong";
            //}
        }
    }
}
