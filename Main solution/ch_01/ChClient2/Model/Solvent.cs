using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient2.Model
{
    public class Solvent : INotifyPropertyChanged
    {
        private string _name;
        private string _cas;
        private string _VValue;


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string CAS
        {
            get
            {
                return _cas;
            }
            set
            {
                _cas = value;
                OnPropertyChanged("CAS");
            }
        }
        public string VValue
        {
            get
            {
                return _VValue;
            }
            set
            {
                _VValue = value;
                OnPropertyChanged("VValue");
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
