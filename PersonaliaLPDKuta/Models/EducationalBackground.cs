using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PersonaliaLPDKuta.Models
{
    public class EducationalBackground : BaseModel
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

        private int educationalStage;
        public int EducationalStage
        {
            get { return educationalStage; }
            set
            {
                if (value != educationalStage)
                {
                    RaisePropertyChanged(ref educationalStage, value);
                }
            }
        }

        private string knowledgeField;
        public string KnowledgeField
        {
            get { return knowledgeField; }
            set
            {
                if (value != knowledgeField)
                {
                    RaisePropertyChanged(ref knowledgeField, value);
                }
            }
        }

        private string instituteName;
        public string InstituteName
        {
            get { return instituteName; }
            set
            {
                if (value != instituteName)
                {
                    RaisePropertyChanged(ref instituteName, value);
                }
            }
        }

        private string instituteLocation;
        public string InstituteLocation
        {
            get { return instituteLocation; }
            set
            {
                if (value != instituteLocation)
                {
                    RaisePropertyChanged(ref instituteLocation, value);
                }
            }
        }

        private string certificateNumber;
        public string CertificateNumber
        {
            get { return certificateNumber; }
            set
            {
                if (value != certificateNumber)
                {
                    RaisePropertyChanged(ref certificateNumber, value);
                }
            }
        }

        private string degree;
        public string Degree
        {
            get { return degree; }
            set
            {
                if (value != degree)
                {
                    RaisePropertyChanged(ref degree, value);
                }
            }
        }

        private string certificateFilePath;
        public string CertificateFilePath
        {
            get { return certificateFilePath; }
            set
            {
                if (value != certificateFilePath)
                {
                    RaisePropertyChanged(ref certificateFilePath, value);
                }
            }
        }

        public override string ToString()
        {
            return KnowledgeField.ToString();
        }

        public bool Equals(EducationalBackground p)
        {
            return (Id == p.Id);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EducationalBackground otherObject))
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
