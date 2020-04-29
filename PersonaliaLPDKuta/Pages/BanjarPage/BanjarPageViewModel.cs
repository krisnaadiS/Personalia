using PersonaliaLPDKuta.Models;
using PersonaliaLPDKuta.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace PersonaliaLPDKuta.Pages.BanjarPage
{
    public class BanjarPageViewModel : BaseModel
    {
        public BanjarPageViewModel()
        {
            SideButtonCommand = new DelegateCommand(SideButtonAction);
            LoadCommand = new DelegateCommand(x => Load());
            selectedState = SideButton.Blank;
            IsSelected = false;
        }

        private Banjar banjar;
        public Banjar Banjar
        {
            get { return banjar; }
            set
            {
                RaisePropertyChanged(ref banjar, value);
            }
        }

        private Banjar oldBanjar;
        public Banjar OldBanjar
        {
            get { return oldBanjar; }
            set
            {
                if (selectedState == SideButton.Blank)
                {
                    Banjar = new Banjar();
                    Banjar.Copy(value);
                    IsSelected = true;
                    RaisePropertyChanged(ref oldBanjar, value);
                }
            }
        }

        private ObservableCollection<Banjar> banjars;
        public ObservableCollection<Banjar> Banjars
        {
            get { return banjars; }
            set
            {
                RaisePropertyChanged(ref banjars, value);
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
            Banjars = await SQLiteDBHelper.LoadBanjars();
            PageManager.BusyIndicator.IsBusy = false;
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
            Banjar = new Banjar();
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
                        Banjar.Delete();
                        Banjars.Remove(Banjar);
                    }
                }),
                Owner = Application.Current.MainWindow
            });
        }

        private void Save(SideButton sideButton)
        {
            if (!Banjar.HasErrors)
            {
                switch (sideButton)
                {
                    case SideButton.Add:
                        Banjar.Insert();
                        Banjars.Add(Banjar);
                        break;
                    case SideButton.Edit:
                        Banjar.Update();
                        OldBanjar.Copy(Banjar);
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
                    Banjar = null;
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
