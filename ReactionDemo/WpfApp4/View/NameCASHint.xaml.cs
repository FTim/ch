using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using InventoryReader;

namespace WpfApp4.View
{
    /// <summary>
    /// Interaction logic for NameCASHint.xaml
    /// </summary>
    public partial class NameCASHint : Window
    {
        ObservableCollection<MoleculeData> HintList;
        public NameCASHint(List<MoleculeData> _hintlist)
        {
            
            HintList = new ObservableCollection<MoleculeData>();

            foreach (var item in _hintlist)
            {
                
                HintList.Add(item);
            }
            DataContext = this;
            InitializeComponent();
            
            ExcelList.DataContext = this;
            ExcelList.ItemsSource = HintList;

        }
    }
}
