using System;
using System.Collections.Generic;
using SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PersonaliaLPDKuta.Models;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace PersonaliaLPDKuta.Utilities
{
    public static class SQLiteDBHelper
    {
        public static string ConnectionString = Path.Combine(UserDataFolder, "db.data");
        public static SQLiteConnection Database = new SQLiteConnection(ConnectionString);

        public static Task Initialize()
        {
            var task = Task.Run(() =>
            {
                Database.CreateTable<User>();
                Database.CreateTable<Employee>();
                Database.CreateTable<Position>();
                Database.CreateTable<Banjar>();
                AddDefaultUser();
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\employeeImg\");
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\certificateImg\");
            });
            return task;
        }

        private static Task AddDefaultUser()
        {
            Task task = Task.Run(() =>
            {
                try
                {
                    var users = Database.Query<User>("select * from User");
                    if (users.Count == 0)
                    {
                        var user = new User()
                        {
                            Username = "admin",
                            Password = "dbafeeec44ac174b81dd899f82a40adb"
                        };
                        user.InsertToSQLite();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            });
            return task;
        }

        public static Task<ObservableCollection<Employee>> LoadEmployees()
        {
            Task<ObservableCollection<Employee>> task = Task.Run(() =>
            {
                ObservableCollection<Employee> employees;
                try
                {
                    employees = new ObservableCollection<Employee>(Database.Query<Employee>("select * from Employee").Where(x => x.Deleted == 0));
                    if (employees == null)
                        employees = new ObservableCollection<Employee>();
                    return employees;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    employees = new ObservableCollection<Employee>();
                    return employees;
                }
            });
            return task;
        }

        public static Task<ObservableCollection<Position>> LoadPositions()
        {
            Task<ObservableCollection<Position>> task = Task.Run(() =>
            {
                ObservableCollection<Position> positions;
                try
                {
                    positions = new ObservableCollection<Position>(Database.Query<Position>("select * from Position").Where(x => x.Deleted == 0));
                    if (positions == null)
                        positions = new ObservableCollection<Position>();
                    return positions;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    positions = new ObservableCollection<Position>();
                    return positions;
                }
            });
            return task;
        }

        public static Task<ObservableCollection<Banjar>> LoadBanjars()
        {
            Task<ObservableCollection<Banjar>> task = Task.Run(() =>
            {
                ObservableCollection<Banjar> banjars;
                try
                {
                    banjars = new ObservableCollection<Banjar>(Database.Query<Banjar>("select * from Banjar").Where(x => x.Deleted == 0));
                    if (banjars == null)
                        banjars = new ObservableCollection<Banjar>();
                    return banjars;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    banjars = new ObservableCollection<Banjar>();
                    return banjars;
                }
            });
            return task;
        }

        public static Task<ObservableCollection<Family>> LoadFamilies(int idEmployee)
        {
            Task<ObservableCollection<Family>> task = Task.Run(() =>
            {
                ObservableCollection<Family> families;
                try
                {
                    families = new ObservableCollection<Family>(Database.Query<Family>("select * from Family").Where(x => x.Deleted == 0 && x.IdEmployee == idEmployee));
                    if (families == null)
                        families = new ObservableCollection<Family>();
                    return families;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    families = new ObservableCollection<Family>();
                    return families;
                }
            });
            return task;
        }

        #region data storage path
        /// <summary>
        /// Get the Application Guid
        /// </summary>
        public static Guid AppGuid
        {
            get
            {
                Assembly asm = Assembly.GetEntryAssembly();
                object[] attr = (asm.GetCustomAttributes(typeof(GuidAttribute), true));
                return new Guid((attr[0] as GuidAttribute).Value);
            }
        }
        /// <summary>
        /// Get the current assembly Guid.
        /// <remarks>
        /// Note that the Assembly Guid is not necessarily the same as the
        /// Application Guid - if this code is in a DLL, the Assembly Guid
        /// will be the Guid for the DLL, not the active EXE file.
        /// </remarks>
        /// </summary>
        public static Guid AssemblyGuid
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                object[] attr = (asm.GetCustomAttributes(typeof(GuidAttribute), true));
                return new Guid((attr[0] as GuidAttribute).Value);
            }
        }
        /// <summary>
        /// Get the current user data folder
        /// </summary>
        public static string UserDataFolder
        {
            get
            {
                Guid appGuid = AppGuid;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, appGuid.ToString("B").ToUpper());
                return CheckDir(dir);
            }
        }
        /// <summary>
        /// Get the current user roaming data folder
        /// </summary>
        public static string UserRoamingDataFolder
        {
            get
            {
                Guid appGuid = AppGuid;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, appGuid.ToString("B").ToUpper());
                return CheckDir(dir);
            }
        }
        /// <summary>
        /// Get all users data folder
        /// </summary>
        public static string AllUsersDataFolder
        {
            get
            {
                Guid appGuid = AppGuid;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, appGuid.ToString("B").ToUpper());
                return CheckDir(dir);
            }
        }
        /// <summary>
        /// Check the specified folder, and create if it doesn't exist.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static string CheckDir(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
        #endregion
    }
}
