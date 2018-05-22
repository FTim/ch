using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Model
{
    public class Reagent : INotifyPropertyChanged
    {
        private string _name;
        private string _cas;
        private string _Ratio;


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
