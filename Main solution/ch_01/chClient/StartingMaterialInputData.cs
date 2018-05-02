using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chClient
{
    public class StartingMaterialInputData : INotifyPropertyChanged
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
        private double? _m;
        public double? m
        {
            get { return _m; }
            set
            {
                if (value != _m)
                {
                    _m = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(m)));
                }
            }
        }
        private double? _v;
        public double? V
        {
            get { return _v; }
            set
            {
                if (value != _v)
                {
                    _v = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(V)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
