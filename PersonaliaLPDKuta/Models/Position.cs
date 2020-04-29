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
    public class Position : ValidationBase, IEquatable<Position>
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

        private string rankName;
        public string RankName
        {
            get { return rankName; }
            set
            {
                if (value != rankName)
                {
                    RaisePropertyChanged(ref rankName, value);
                }
            }
        }

        private string positionName;
        public string PositionName
        {
            get { return positionName; }
            set
            {
                if (value != positionName)
                {
                    RaisePropertyChanged(ref positionName, value);
                }
            }
        }

        private int nilaiTunjangan;
        public int NilaiTunjangan
        {
            get { return nilaiTunjangan; }
            set
            {
                if (value != nilaiTunjangan)
                {
                    RaisePropertyChanged(ref nilaiTunjangan, value);
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
                "Position set " +
                "RankName = ?, " +
                "PositionName = ?, " +
                "NilaiTunjangan = ? " +
                "where Id = ?",
                RankName,
                PositionName,
                NilaiTunjangan,
                Id);
        }
        public void Delete()
        {
            Deleted = 1;
            SQLiteDBHelper.Database.Execute("update Position set Deleted = 1 where Id = ?", Id);
        }

        public void Copy(Position oPosition)
        {
            if (oPosition != null)
            {
                RankName = oPosition.RankName;
                PositionName = oPosition.PositionName;
                NilaiTunjangan = oPosition.NilaiTunjangan;
                Id = oPosition.Id;
            }
        }

        public override string ToString()
        {
            return RankName == null ? String.Empty : RankName.ToString();
        }

        public bool Equals(Position p)
        {
            return (Id == p.Id);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Position otherObject))
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

            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(RankName))
            {
                if (string.IsNullOrWhiteSpace(rankName))
                    yield return "Kepangkatan tidak boleh kosong";
            }
            if (string.IsNullOrEmpty(propertyName) || propertyName == nameof(PositionName))
            {
                if (string.IsNullOrWhiteSpace(positionName))
                    yield return "Jabatan tidak boleh kosong";
            }
        }
    }
}
