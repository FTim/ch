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
        private IList<Solvent> _SolventList;
        public SolventViewModel()
        {
            _SolventList = new List<Solvent>
            {
                new Solvent{Name="asd", CAS="rnd-01-102", VValue="42"},
                new Solvent{Name="saffe", CAS="lel-546-63", VValue="75"},

            };
        }
        public IList<Solvent> Solvents
        {
            get { return _SolventList; }
            set { _SolventList = value; }
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
