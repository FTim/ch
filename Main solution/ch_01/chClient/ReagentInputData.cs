using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chClient
{
    public class ReagentInputData : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if(value!=_name)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }
        private string _cas;
        public string CAS
        {
            get { return _cas; }
            set
            {
                if (value != _cas)
                {
                    _cas = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CAS)));
                }
            }
        }
        
        private double _ratio;
        public double Ratio
        {
            get { return _ratio; }
            set
            {
                if (value != _ratio)
                {
                    _ratio = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ratio)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
