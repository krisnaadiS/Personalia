using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PersonaliaLPDKuta.Utilities
{
    public class Ticker : INotifyPropertyChanged
    {
        public Ticker()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public string DateNow
        {
            get { return DateTime.Now.ToString("dd MMMM yyyy"); }
        }

        public string TimeNow
        {
            get { return DateTime.Now.ToString("HH:mm"); }
        }

        public string DateTimeNow
        {
            get { return DateTime.Now.ToString("dddd, dd MMMM yyyy, HH:mm"); }
        }

        public string DateTimeNowShort
        {
            get { return DateTime.Now.ToString("ddd, dd MMM yyyy, HH:mm"); }
        }

        public string DayDateNow
        {
            get { return DateTime.Now.ToString("dddd, dd MMMM yyyy"); }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TimeNow"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateNow"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTimeNow"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTimeNowShort"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DayDateNow"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
