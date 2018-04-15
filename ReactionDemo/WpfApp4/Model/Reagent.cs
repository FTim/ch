using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfApp4.Model
{
    public class Reagent : INotifyPropertyChanged
    {
        private string _name;
        private string _cas;
        private string _ratio;
        

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
                return _ratio;
            }
            set
            {
                _ratio = value;
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
