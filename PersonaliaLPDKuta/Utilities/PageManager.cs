using PersonaliaLPDKuta.Pages.BanjarPage;
using PersonaliaLPDKuta.Pages.EmployeePage;
using PersonaliaLPDKuta.Pages.LoginPage;
using PersonaliaLPDKuta.Pages.PositionPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace PersonaliaLPDKuta.Utilities
{
    public static class PageManager
    {
        private static readonly LoginPageView loginPageView = new LoginPageView();
        private static readonly MainTemplate mainTemplate = new MainTemplate();

        private static readonly EmployeePageView employeePageView = new EmployeePageView();
        private static readonly PositionPageView positionPageView = new PositionPageView();
        private static readonly BanjarPageView banjarPageView = new BanjarPageView();

        private static Grid gridContent;
        public static Grid GridContent
        {
            set { gridContent = value; }
        }

        public static RadBusyIndicator BusyIndicator { get; set; }

        private static void ChangeContent(UserControl ctrl)
        {
            if (gridContent.Children.Count > 0)
                gridContent.Children.Clear();
            gridContent.Children.Add(ctrl);
        }

        public static Grid GridSubContent { set; get; }
        private static void ChangeSubContent(UserControl ctrl)
        {
            if (GridSubContent.Children.Count > 0)
                GridSubContent.Children.Clear();
            GridSubContent.Children.Add(ctrl);
        }

        public static void LoadLoginPage()
        {
            ChangeContent(loginPageView);
        }

        public static void LoadMainTemplate()
        {
            ChangeContent(mainTemplate);
        }

        public static void LoadEmployeePage()
        {
            ChangeSubContent(employeePageView);
        }
        
        public static void LoadPositionPage()
        {
            ChangeSubContent(positionPageView);
        }

        public static void LoadBanjarPage()
        {
            ChangeSubContent(banjarPageView);
        }
    }
}
