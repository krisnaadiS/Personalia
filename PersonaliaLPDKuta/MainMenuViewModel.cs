using PersonaliaLPDKuta.Models;
using PersonaliaLPDKuta.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace PersonaliaLPDKuta
{
    public class MainMenuViewModel : BaseModel
    {
        public MainMenuViewModel()
        {
            ChangeCommand = new DelegateCommand(ChangePage);
        }

        private DelegateCommand changeCommand;
        public DelegateCommand ChangeCommand
        {
            get { return changeCommand; }
            set
            {
                if (changeCommand != value)
                {
                    RaisePropertyChanged(ref changeCommand, value);
                }
            }
        }

        private void ChangePage(object obj)
        {
            var page = (Page)obj;
            switch (page)
            {
                case Page.Dashboard:
                    
                    break;
                case Page.Employee:
                    PageManager.LoadEmployeePage();
                    break;
                case Page.Family:
                    break;
                case Page.Position:
                    PageManager.LoadPositionPage();
                    break;
                case Page.Banjar:
                    PageManager.LoadBanjarPage();
                    break;
            }
        }
    }

    public enum Page
    {
        Dashboard,
        Employee,
        Family,
        Position,
        Banjar
    }
}
