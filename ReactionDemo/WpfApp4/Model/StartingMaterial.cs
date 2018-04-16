﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfApp4.Model
{
    public class StartingMaterial : INotifyPropertyChanged
    {
        private string _name;
        private string _cas;
        private string _mValue;
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
        public string mValue
        {
            get
            {
                return _mValue;
            }
            set
            {
                _mValue = value;
                OnPropertyChanged("mValue");
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