using PersonaliaLPDKuta.Models;
using PersonaliaLPDKuta.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace PersonaliaLPDKuta.Pages.EmployeePage
{
    public class EmployeePageViewModel : BaseModel
    {
        public EmployeePageViewModel()
        {
            SideButtonCommand = new DelegateCommand(SideButtonAction);
            LoadCommand = new DelegateCommand(x => Load());
            selectedState = SideButton.Blank;
            IsSelected = false;
        }

        private Employee employee;
        public Employee Employee
        {
            get { return employee; }
            set
            {
                RaisePropertyChanged(ref employee, value);
            }
        }

        private Employee oldEmployee;
        public Employee OldEmployee
        {
            get { return oldEmployee; }
            set
            {
                if (selectedState == SideButton.Blank)
                {
                    Employee = new Employee();
                    Employee.Copy(value);
                    IsSelected = true;
                    RaisePropertyChanged(ref oldEmployee, value);
                }
            }
        }

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set
            {
                RaisePropertyChanged(ref employees, value);
            }
        }

        private bool isChanging;
        public bool IsChanging
        {
            get { return isChanging; }
            set
            {
                RaisePropertyChanged(ref isChanging, value);
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                RaisePropertyChanged(ref isSelected, value);
            }
        }

        private DelegateCommand loadCommand;
        public DelegateCommand LoadCommand
        {
            get { return loadCommand; }
            set
            {
                RaisePropertyChanged(ref loadCommand, value);
            }
        }

        private DelegateCommand sideButtonCommand;
        public DelegateCommand SideButtonCommand
        {
            get { return sideButtonCommand; }
            set
            {
                RaisePropertyChanged(ref sideButtonCommand, value);
            }
        }

        private async void Load()
        {
            PageManager.BusyIndicator.IsBusy = true;
            Employees = await SQLiteDBHelper.LoadEmployees();
            PageManager.BusyIndicator.IsBusy = false;
            Debug.WriteLine(Employees.Count);
        }

        private SideButton selectedState;
        private void SideButtonAction(Object obj)
        {
            var sideButton = (SideButton)obj;
            switch (sideButton)
            {
                case SideButton.Add:
                    Add();
                    selectedState = sideButton;
                    break;
                case SideButton.Edit:
                    Edit();
                    selectedState = sideButton;
                    break;
                case SideButton.Delete:
                    Delete();
                    break;
                case SideButton.Save:
                    Save(selectedState);
                    break;
                case SideButton.Cancel:
                    ReturnState(selectedState);
                    break;
            }
        }

        private void Add()
        {
            IsChanging = true;
            IsSelected = false;
            Employee = new Employee();
        }

        private void Edit()
        {
            IsChanging = true;
        }

        private void Delete()
        {
            string confirmText = "yakin menghapus pegawai ini?";
            RadWindow.Confirm(new DialogParameters
            {
                Content = confirmText,
                Closed = new EventHandler<WindowClosedEventArgs>((object senderDialog, WindowClosedEventArgs eDialog) =>
                {
                    if (eDialog.DialogResult == true)
                    {
                        Employee.Delete();
                        Employees.Remove(Employee);
                    }
                }),
                Owner = Application.Current.MainWindow
            });
        }

        private void Save(SideButton sideButton)
        {
            if (!Employee.HasErrors)
            {
                switch (sideButton)
                {
                    case SideButton.Add:
                        Employee.Insert();
                        Employees.Add(Employee);
                        break;
                    case SideButton.Edit:
                        Employee.Update();
                        OldEmployee.Copy(Employee);
                        break;
                }
                ReturnState(sideButton);
            }
            else
            {
                RadWindow.Alert(new DialogParameters
                {
                    Content = "Ada data yg masih kosong",
                    Owner = Application.Current.MainWindow
                });
            }
        }

        private void ReturnState(SideButton sideButton)
        {
            switch (sideButton)
            {
                case SideButton.Add:
                    Employee = null;
                    IsChanging = false;
                    IsSelected = false;
                    break;
                case SideButton.Edit:
                    IsChanging = false;
                    break;
            }
            selectedState = SideButton.Blank;
        }
    }

    public enum SideButton
    {
        Add, Edit, Delete, Save, Cancel, Blank
    }
}
