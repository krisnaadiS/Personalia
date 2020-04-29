using PersonaliaLPDKuta.Utilities;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PersonaliaLPDKuta.Models
{
    public class Family : ValidationBase, IEquatable<Family>
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

        private int idEmployee;
        public int IdEmployee
        {
            get { return idEmployee; }
            set
            {
                if (value != idEmployee)
                {
                    RaisePropertyChanged(ref idEmployee, value);
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

        private int familiStatus;
        public int FamilyStatus
        {
            get { return familiStatus; }
            set
            {
                if (value != familiStatus)
                {
                    RaisePropertyChanged(ref familiStatus, value);
                }
            }
        }

        private int employmentStatus;
        public int EmploymentStatus
        {
            get { return employmentStatus; }
            set
            {
                if (value != employmentStatus)
                {
                    RaisePropertyChanged(ref employmentStatus, value);
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

        public void Insert()
        {
            SQLiteDBHelper.Database.Insert(this);
        }
        public void Update()
        {
            SQLiteDBHelper.Database.Execute("update " +
                "Family set " +
                "Name = ?, " +
                "BirthDate = ?, " +
                "FamilyStatus = ?, " +
                "EmploymentStatus = ?  " +
                "where Id = ?",
                Name,
                BirthDate,
                FamilyStatus,
                EmploymentStatus,
                Id);
        }
        public void Delete()
        {
            Deleted = 1;
            SQLiteDBHelper.Database.Execute("update Family set Deleted = 1 where Id = ?", Id);
        }

        public void Copy(Family oFamily)
        {
            if (oFamily != null)
            {
                Name = oFamily.Name;
                BirthDate = oFamily.BirthDate;
                FamilyStatus = oFamily.FamilyStatus;
                EmploymentStatus = oFamily.EmploymentStatus;
                Id = oFamily.Id;
                IdEmployee = oFamily.IdEmployee;
            }
        }

        public override string ToString()
        {
            return Name == null ? String.Empty : Name.ToString();
        }

        public bool Equals(Family p)
        {
            return (Id == p.Id);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Family otherObject))
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

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(BirthDate))
            {
                if (!birthDate.HasValue)
                    yield return "Tanggal Lahir tidak boleh kosong";
            }
        }
    }
}
