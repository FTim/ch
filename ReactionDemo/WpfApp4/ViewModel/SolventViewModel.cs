using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using WpfApp4.Model;

namespace WpfApp4.ViewModel
{
    public class SolventViewModel
    {
        private Solvent _Solvent;
        public SolventViewModel()
        {
            
        }
        public Solvent SolventData
        {
            get { return _Solvent; }
            set { _Solvent = value; }
        }
        
    }
}
