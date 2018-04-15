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
                new Reagent{Name="asd", CAS="rnd-01-102", Ratio="13"},
                new Reagent{Name="saffe", CAS="lel-546-63", Ratio="42"},

            };
        }
        public IList<Reagent> Reagents
        {
            get { return _ReagentList; }
            set { _ReagentList = value; }
        }
        
        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }


        private class Updater : ICommand
        {
            #region ICommand Members
            public bool CanExecute(object parameter)
            {
                return true;
            }
            public event EventHandler CanExecuteChanged;
            public void Execute(object parameter)
            {
            }
            #endregion
        }
    }
}
