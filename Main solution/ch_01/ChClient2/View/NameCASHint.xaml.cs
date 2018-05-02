using InventoryReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChClient2.View
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
