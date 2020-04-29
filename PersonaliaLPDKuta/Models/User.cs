using PersonaliaLPDKuta.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaliaLPDKuta.Models
{
    public class User : BaseModel
    {
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (value != username)
                {
                    RaisePropertyChanged(ref username, value);
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (value != password)
                {
                    RaisePropertyChanged(ref password, value);
                }
            }
        }

        public void InsertToSQLite()
        {
            SQLiteDBHelper.Database.Insert(this);
        }

        public override string ToString()
        {
            return Username.ToString();
        }

        public bool Equals(User p)
        {
            return (Username == p.Username);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is User otherObject))
                return false;
            else
                return Equals(otherObject);
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode();
        }
    }
}
