using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.IO;

namespace InventoryReader
{
    public class InventoryReader
    {
        private List<MoleculeData> molecules;
        public string FilePath { get; set; }

        public List<MoleculeData> ReadTo()
        {
            
            using (var stream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //header row - do nothing
                    reader.Read();

                    while (reader.Read())
                    {
                        //check for the last empty row
                        if (reader.GetValue(0).Equals(null) || (string)reader.GetValue(0) == "") break;
                        molecules.Add(new MoleculeData()
                        {
                            Name = (string)reader.GetValue(1),
                            Location = (string)reader.GetValue(2),
                            CAS= (string)reader.GetValue(3),
                            Mvalue=(double)reader.GetValue(4),
                            mvalue=(double)reader.GetValue(5),
                            Vvalue=(double)reader.GetValue(6),
                            dvalue=(double)reader.GetValue(7),
                            mpvalue=(double)reader.GetValue(8),
                            bpvalue=(double)reader.GetValue(9),
                            purity=(double)reader.GetValue(10)
                        });
                    }
                    
                }
            }
            Console.WriteLine("im so done");
            return molecules;
        }
    }
}
