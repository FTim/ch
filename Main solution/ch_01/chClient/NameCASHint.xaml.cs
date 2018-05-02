using InventoryReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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



namespace chClient
{
    /// <summary>
    /// Interaction logic for NameCASHint.xaml
    /// </summary>
    public partial class NameCASHint : Window
    {
        private class NameCAS : INotifyPropertyChanged
        {
            private string _name;
            public string Name
            {
                get { return _name; }
                set
                {
                    if (value != _name)
                    {
                        _name = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                    }
                }
            }
            private string _cas;

            public event PropertyChangedEventHandler PropertyChanged;

            public string CAS
            {
                get { return _cas; }
                set
                {
                    if (value != _cas)
                    {
                        _cas = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CAS)));
                    }
                }
            }
        }
        private List<NameCAS> NameCASList;
        private InventoryReading readed;

        private ObservableCollection<NameCAS> ShownNameCAS;

        public NameCASHint(InventoryReading data)
        {
            NameCASList = new List<NameCAS>();
            readed = data;
            InitializeComponent();

            List<MoleculeData> tmp = new List<MoleculeData>();
            tmp=readed.ReadTo();

            foreach (MoleculeData item in tmp)
            {
                NameCASList.Add(new NameCAS { Name = item.Name, CAS = item.CAS });
            }
            ShownNameCAS = new ObservableCollection<NameCAS>();
        }

        private void MoleculeSearchTB_KeyDown(object sender, KeyEventArgs e)
        {
            ShownNameCAS.Clear();
            string s = MoleculeSearchTB.Text;

            foreach (NameCAS item in NameCASList)
            {
                if (item.Name.Contains(s)) ShownNameCAS.Add(item);
            }

            //NameCASHintLW.Items.Refresh();

        }
    }
}
