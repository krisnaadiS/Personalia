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

namespace PersonaliaLPDKuta.Pages.PositionPage
{
    public class PositionPageViewModel : BaseModel
    {
        public PositionPageViewModel()
        {
            SideButtonCommand = new DelegateCommand(SideButtonAction);
            LoadCommand = new DelegateCommand(x => Load());
            selectedState = SideButton.Blank;
            IsSelected = false;
        }

        private Position position;
        public Position Position
        {
            get { return position; }
            set
            {
                RaisePropertyChanged(ref position, value);
            }
        }

        private Position oldPosition;
        public Position OldPosition
        {
            get { return oldPosition; }
            set
            {
                if (selectedState == SideButton.Blank)
                {
                    Position = new Position();
                    Position.Copy(value);
                    IsSelected = true;
                    RaisePropertyChanged(ref oldPosition, value);
                }
            }
        }

        private ObservableCollection<Position> positions;
        public ObservableCollection<Position> Positions
        {
            get { return positions; }
            set
            {
                RaisePropertyChanged(ref positions, value);
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
            Positions = await SQLiteDBHelper.LoadPositions();
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
            Position = new Position();
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
                        Position.Delete();
                        Positions.Remove(Position);
                    }
                }),
                Owner = Application.Current.MainWindow
            });
        }

        private void Save(SideButton sideButton)
        {
            if (!Position.HasErrors)
            {
                switch (sideButton)
                {
                    case SideButton.Add:
                        Position.Insert();
                        Positions.Add(Position);
                        break;
                    case SideButton.Edit:
                        Position.Update();
                        OldPosition.Copy(Position);
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
                    Position = null;
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