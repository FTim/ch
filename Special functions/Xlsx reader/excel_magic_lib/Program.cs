using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://github.com/ExcelDataReader/ExcelDataReader

namespace excel_magic_lib
{
    class Program
    {
        static void Main(string[] args)
        {
            //Given usege template.... and it works
            //using (var stream = File.Open(@"C:\Users\Timea\Desktop\Inventory.xlsx", FileMode.Open, FileAccess.Read))
            using (var stream = File.Open("asd.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            var Linenumber = reader.GetValue(0);
                            var Names = reader.GetValue(1);
                            var Locations = reader.GetValue(2);
                            var CAS = reader.GetValue(3);
                            Console.WriteLine("Line: {0}\tName: {1}\tLocation: {2}\tCAS: {3}", Linenumber, Names, Locations, CAS);
                        }
                    } while (reader.NextResult());
                }
            }
            Console.WriteLine("im so done");
            Console.ReadKey();
        }
    }
}
