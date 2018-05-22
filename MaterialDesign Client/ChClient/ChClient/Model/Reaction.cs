using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Model
{
    public class Reaction : INotifyPropertyChanged
    {
        private string _code;
        private string _chemist;
        private string _chiefchemist;
        private string _project;
        private string _laboratory;
        private string _startdate;
        private string _closuredate;
        private string _previousstep;
        private string _literature;
        private string _procedure;
        private string _yield;
        private string _observation;

        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }
        public string Chemist
        {
            get
            {
                return _chemist;
            }
            set
            {
                _chemist = value;
                OnPropertyChanged("Chemist");
            }
        }
        public string Chiefchemist
        {
            get
            {
                return _chiefchemist;
            }
            set
            {
                _chiefchemist = value;
                OnPropertyChanged("Chiefchemist");
            }
        }
        public string Project
        {
            get
            {
                return _project;
            }
            set
            {
                _project = value;
                OnPropertyChanged("Project");
            }
        }
        public string Laboratory
        {
            get
            {
                return _laboratory;
            }
            set
            {
                _laboratory = value;
                OnPropertyChanged("Laboratory");
            }
        }
        public string StartDate
        {
            get
            {
                return _startdate;
            }
            set
            {
                _startdate = value;
                OnPropertyChanged("StartDate");
            }
        }
        public string ClosureDate
        {
            get
            {
                return _closuredate;
            }
            set
            {
                _closuredate = value;
                OnPropertyChanged("ClosureDate");
            }
        }
        public string PreviousStep
        {
            get
            {
                return _previousstep;
            }
            set
            {
                _previousstep = value;
                OnPropertyChanged("PreviousStep");
            }
        }
        public string Literature
        {
            get
            {
                return _literature;
            }
            set
            {
                _literature = value;
                OnPropertyChanged("Literature");
            }
        }
        public string Procedure
        {
            get
            {
                return _procedure;
            }
            set
            {
                _literature = value;
                OnPropertyChanged("Procedure");
            }
        }
        public string Yield
        {
            get
            {
                return _yield;
            }
            set
            {
                _yield = value;
                OnPropertyChanged("Yield");
            }
        }
        public string Observation
        {
            get
            {
                return _observation;
            }
            set
            {
                _observation = value;
                OnPropertyChanged("Observation");
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
