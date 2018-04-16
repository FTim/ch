using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Model
{
    public class Product : INotifyPropertyChanged
    {
        private string _MWValue;
        private string _Ratio;
        private string _nValue;

        public string MWValue
        {
            get
            {
                return _MWValue;
            }
            set
            {
                _MWValue = value;
                OnPropertyChanged("MWValue");
            }
        }

        public string Ratio
        {
            get
            {
                return _Ratio;
            }
            set
            {
                _Ratio = value;
                OnPropertyChanged("Ratio");
            }
        }

        public string nValue
        {
            get
            {
                return _nValue;
            }
            set
            {
                _nValue = value;
                OnPropertyChanged("nValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
