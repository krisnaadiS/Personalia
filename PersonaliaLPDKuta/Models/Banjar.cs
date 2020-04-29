using PersonaliaLPDKuta.Utilities;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PersonaliaLPDKuta.Models
{
    public class Banjar : ValidationBase, IEquatable<Banjar>
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
                "Banjar set " +
                "Name = ? " +
                "where Id = ?",
                Name,
                Id);
        }
        public void Delete()
        {
            Deleted = 1;
            SQLiteDBHelper.Database.Execute("update Banjar set Deleted = 1 where Id = ?", Id);
        }

        public void Copy(Banjar oBanjar)
        {
            if (oBanjar != null)
            {
                Name = oBanjar.Name;
                Id = oBanjar.Id;
            }
        }

        public override string ToString()
        {
            return Name == null ? String.Empty : Name.ToString();
        }

        public bool Equals(Banjar p)
        {
            return (Id == p.Id);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Banjar otherObject))
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
                    yield return "Nama Banjar tidak boleh kosong";
            }
        }
    }
}
