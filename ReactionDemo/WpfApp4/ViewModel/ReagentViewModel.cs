using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using WpfApp4.Model;

namespace WpfApp4.ViewModel
{
    public class ReagentViewModel
    {
        private IList<Reagent> _ReagentList;
        public ReagentViewModel()
        {
            _ReagentList = new List<Reagent>
            {
                new Reagent{Name="asd", CAS="rnd-01-102", Ratio="55"},
                new Reagent{Name="saffe", CAS="lel-546-63", Ratio="75"},

            };
        }
        public IList<Reagent> Reagents
        {
            get { return _ReagentList; }
            set { _ReagentList = value; }
        }

    }
}

