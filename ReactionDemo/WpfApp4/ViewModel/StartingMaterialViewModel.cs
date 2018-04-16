using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using WpfApp4.Model;

namespace WpfApp4.ViewModel
{
    public class StartingMaterialViewModel
    {
        private IList<StartingMaterial> _StartingMaterialList;
        public StartingMaterialViewModel()
        {
            _StartingMaterialList = new List<StartingMaterial>
            {
                new StartingMaterial{Name="asd", CAS="rnd-01-102", mValue="13", VValue=null},
                new StartingMaterial{Name="saffe", CAS="lel-546-63", mValue=null, VValue="34"},
                
            };
        }
        public IList<StartingMaterial> StartingMaterial
        {
            get { return _StartingMaterialList; }
            set { _StartingMaterialList = value; }
        }
        
    }
}
