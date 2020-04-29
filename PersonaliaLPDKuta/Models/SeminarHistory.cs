using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaliaLPDKuta.Models
{
    public class SeminarHistory : BaseModel
    {
        private int id;
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

        private string seminarTitle;
        public string SeminarTitle
        {
            get { return seminarTitle; }
            set
            {
                if (value != seminarTitle)
                {
                    RaisePropertyChanged(ref seminarTitle, value);
                }
            }
        }

        private DateTime startDate;
        public DateTime StartDate
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

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (value != endDate)
                {
                    RaisePropertyChanged(ref endDate, value);
                }
            }
        }

        private string seminarPlace;
        public string SeminarPlace
        {
            get { return seminarPlace; }
            set
            {
                if (value != seminarPlace)
                {
                    RaisePropertyChanged(ref seminarPlace, value);
                }
            }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set
            {
                if (value != remark)
                {
                    RaisePropertyChanged(ref remark, value);
                }
            }
        }

        private string seminarFilePath;
        public string SeminarFilePath
        {
            get { return seminarFilePath; }
            set
            {
                if (value != seminarFilePath)
                {
                    RaisePropertyChanged(ref seminarFilePath, value);
                }
            }
        }

        private string sertificateFilePath;
        public string SertificateFilePath
        {
            get { return sertificateFilePath; }
            set
            {
                if (value != sertificateFilePath)
                {
                    RaisePropertyChanged(ref sertificateFilePath, value);
                }
            }
        }

        public override string ToString()
        {
            return seminarTitle.ToString();
        }

        public bool Equals(SeminarHistory p)
        {
            return (Id == p.Id);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SeminarHistory otherObject))
                return false;
            else
                return Equals(otherObject);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
