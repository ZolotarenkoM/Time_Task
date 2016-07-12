using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;


namespace Wpf_TimeTask
{
    public class ViewModel : ViewModelBase
    {
        private string _rez;
        public static string Start { get; set; }
        public static string End { get; set; }
        static string TimeNow { get; set; }

        public ViewModel()
        {
            Start = DateTime.Now.ToShortTimeString();
            End = DateTime.Now.AddHours(2).ToShortTimeString();
            TimeNow = DateTime.Now.ToShortDateString();
        }

        public string Rez
        {
            get { return _rez; }
            set
            {
                _rez = value; 
                RaisePropertyChanged(() => Rez);
            }
        }

       private ICommand _time;
        
        public ICommand Time
        {
            get
            {
                return _time ?? (_time = new RelayCommand(() =>
                {
                    try
                    {
                        int kH1 = int.Parse(Start.Substring(0, 2));
                        int kM1 = int.Parse(Start.Substring(3, 2));
                        int kH2 = int.Parse(End.Substring(0, 2));
                        int kM2 = int.Parse(End.Substring(3, 2));
                        Rez = Time2(kM1, kM2, kH1, kH2);
                    }
                    catch (FormatException m)
                    {
                        MessageBox.Show(m.Message + "\nFormat must be - \"hh:mm\"");
                        Rez = "Wrong Format";
                    }
                    catch (Exception m)
                    {
                        MessageBox.Show(m.Message + "\nWrong value - \"hh:mm\"");
                        Rez = "Error";
                    }
                }));
            }
        }

        string Time2(int m1, int m2, int h1, int h2)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, h1, m1, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, h2, m2, 0);
            string k = end.Subtract(start).ToString();
            return k.Substring(0, 5);
        }

    }
}
