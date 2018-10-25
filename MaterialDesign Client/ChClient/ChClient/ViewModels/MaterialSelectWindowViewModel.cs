using ChClient.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.ViewModels
{
    public class MaterialSelectWindowViewModel : ViewModelBase
    {
        public MaterialSelectWindowViewModel()
        {
            //System.Windows.MessageBox.Show("Asd");
            AllMolecules = new List<SelectedMolecule>();
            FoundMolecules = new ObservableCollection<SelectedMolecule>();
            Selected = null;
        }
        private string _searchedtext;
        public string SearchedText { get { return _searchedtext; } set { Set(ref _searchedtext, value); RefreshList(); } }
        public List<SelectedMolecule> AllMolecules { get; set; }
        public ObservableCollection<SelectedMolecule> FoundMolecules { get; set; }

        private SelectedMolecule _selected;
        public SelectedMolecule Selected { get { return _selected; } set { Set(ref _selected, value); } }
        public void SetAllMolecules(List<SelectedMolecule> list)
        {
            foreach (var item in list)
            {
                AllMolecules.Add(item);
                FoundMolecules.Add(item);
            }
        }

        
        

        public void RefreshList()
        {
            FoundMolecules.Clear();
            foreach (var item in AllMolecules)
            {
                if (item.Name.ToUpper().Contains(_searchedtext.ToUpper()))
                {
                    FoundMolecules.Add(item);
                }
            }
        }
    }
}
