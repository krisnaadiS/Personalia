using PersonaliaLPDKuta.Pages.LoginPage;
using PersonaliaLPDKuta.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace PersonaliaLPDKuta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            FluentPalette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FF208A00");
            FluentPalette.Palette.AccentFocusedColor = (Color)ColorConverter.ConvertFromString("#FF084F00");
            FluentPalette.Palette.AccentMouseOverColor = (Color)ColorConverter.ConvertFromString("#FF00B294");
            FluentPalette.Palette.AccentPressedColor = (Color)ColorConverter.ConvertFromString("#FF017566");
            FluentPalette.Palette.AlternativeColor = (Color)ColorConverter.ConvertFromString("#FFF2F2F2");
            FluentPalette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#33000000");
            FluentPalette.Palette.BasicSolidColor = (Color)ColorConverter.ConvertFromString("#FFCDCDCD");
            FluentPalette.Palette.ComplementaryColor = (Color)ColorConverter.ConvertFromString("#FFCCCCCC");
            FluentPalette.Palette.IconColor = (Color)ColorConverter.ConvertFromString("#CC000000");
            FluentPalette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#1A000000");
            FluentPalette.Palette.MarkerColor = (Color)ColorConverter.ConvertFromString("#FF000000");
            FluentPalette.Palette.MarkerInvertedColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            FluentPalette.Palette.MarkerMouseOverColor = (Color)ColorConverter.ConvertFromString("#FF000000");
            FluentPalette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#33000000");
            FluentPalette.Palette.PressedColor = (Color)ColorConverter.ConvertFromString("#4C000000");
            FluentPalette.Palette.PrimaryBackgroundColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            FluentPalette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#66FFFFFF");
            FluentPalette.Palette.PrimaryMouseOverColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            FluentPalette.Palette.ReadOnlyBackgroundColor = (Color)ColorConverter.ConvertFromString("#00FFFFFF");
            FluentPalette.Palette.ReadOnlyBorderColor = (Color)ColorConverter.ConvertFromString("#FFCDCDCD");
            FluentPalette.Palette.ValidationColor = (Color)ColorConverter.ConvertFromString("#FFE81123");
            FluentPalette.Palette.DisabledOpacity = 0.3;
            FluentPalette.Palette.InputOpacity = 0.6;
            FluentPalette.Palette.ReadOnlyOpacity = 0.5;

            InitializeComponent();
            PageManager.GridContent = gridContent;
            PageManager.LoadMainTemplate();
            PageManager.BusyIndicator = busyIndicator;
            Prepare();
        }

        public async void Prepare()
        {
            PageManager.BusyIndicator.IsBusy = true;
            await SQLiteDBHelper.Initialize();
            PageManager.BusyIndicator.IsBusy = false;
        }
    }
}
