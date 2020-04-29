using PersonaliaLPDKuta.Models;
using PersonaliaLPDKuta.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace PersonaliaLPDKuta.Pages.LoginPage
{
    public class LoginPageViewModel : BaseModel
    {
        public LoginPageViewModel()
        {
            LoginCommand = new DelegateCommand(Login);
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    RaisePropertyChanged(ref username, value);
                }
            }
        }

        private DelegateCommand loginCommand;
        public DelegateCommand LoginCommand
        {
            get { return loginCommand; }
            set
            {
                if (loginCommand != value)
                {
                    loginCommand = value;
                    RaisePropertyChanged(ref loginCommand, value);
                }
            }
        }

        private async void Login(object obj)
        {
            if (!username.Equals(""))
            {
                
                PageManager.BusyIndicator.IsBusy = true;
                Task<User> task = Task.Run(() =>
                {
                    try
                    {
                        using (MD5 md5Hash = MD5.Create())
                        {
                            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + SQLiteDBHelper.ConnectionString))
                            {
                                var query = "select * from User where Username=@username and Password=@password";
                                connection.Open();
                                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                                {
                                    cmd.Parameters.AddWithValue("@username", username);
                                    var pass = obj as RadPasswordBox;
                                    cmd.Parameters.AddWithValue("@password", GetMd5Hash(md5Hash, pass.Password));
                                    using (SQLiteDataReader dataReader = cmd.ExecuteReader())
                                    {
                                        User userData = null;
                                        while (dataReader.Read())
                                        {
                                            userData = new User()
                                            {
                                                Username = dataReader["Username"].ToString(),
                                            };
                                        }
                                        return userData;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                        return null;
                    }
                });
                var user = await task;
                PageManager.BusyIndicator.IsBusy = false;
                if (user != null)
                {
                    PageManager.LoadMainTemplate();
                }
                else
                {
                    RadWindow.Alert(new DialogParameters
                    {
                        Content = "Username atau password salah",
                        Owner = Application.Current.MainWindow
                    });
                }
            }
            else
            {
                RadWindow.Alert(new DialogParameters
                {
                    Content = "Username harus diisi",
                    Owner = Application.Current.MainWindow
                });
            }
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
