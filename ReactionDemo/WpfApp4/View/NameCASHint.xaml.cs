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
        ObservableCollection<MoleculeData> Searched;
        public NameCASHint(List<MoleculeData> _hintlist)
        {
            
            HintList = new ObservableCollection<MoleculeData>();
            Searched = new ObservableCollection<MoleculeData>();

            foreach (var item in _hintlist)
            {
                
                HintList.Add(item);
                Searched.Add(item);
            }
            DataContext = this;
            InitializeComponent();
            
            ExcelList.DataContext = this;
            ExcelList.ItemsSource = Searched;

        }
        
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            Searched.Clear();
            
            foreach (MoleculeData item in HintList)
            {
                if (item.Name.ToUpper().Contains(SearchTb.Text.ToString().ToUpper())) Searched.Add(item);
            }
            
        }

        private void SearchTb_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Searched.Clear();

            foreach (MoleculeData item in HintList)
            {
                if (item.Name.ToUpper().Contains(SearchTb.Text.ToString().ToUpper())) Searched.Add(item);
            }
        }
    }
}
