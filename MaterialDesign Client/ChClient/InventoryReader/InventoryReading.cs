using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InventoryReader
{
    public class InventoryReading
    {
        private List<MoleculeData> molecules;
        public string FilePath { get; set; }

        public List<MoleculeData> ReadTo()
        {
            molecules = new List<MoleculeData>();
            double? tmp_mvalue;
            double? tmp_Vvalue;
            double? tmp_dvalue;
            string tmp_mp;
            string tmp_bp;
            string tmp_purity;
            string tmp_Mvalue;


            using (var stream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration());
                    //header row - do nothing
                    reader.Read();

                    while (reader.Read())
                    {
                        try
                        {


                            //check for the last empty row
                            if (reader.GetValue(1) == null) break;

                            tmp_Mvalue = reader.GetValue(4).ToString();
                            tmp_Mvalue = tmp_Mvalue.TrimEnd();


                            if (reader.GetValue(5) == null) tmp_mvalue = null;
                            else tmp_mvalue = Double.Parse(reader.GetValue(5).ToString());

                            if (reader.GetValue(6) == null) tmp_Vvalue = null;
                            else tmp_Vvalue = Double.Parse(reader.GetValue(6).ToString());

                            if (reader.GetValue(7) == null) tmp_dvalue = null;
                            else tmp_dvalue = Double.Parse(reader.GetValue(7).ToString());

                            if (reader.GetValue(8) == null) tmp_mp = "";
                            else tmp_mp = reader.GetValue(8).ToString();

                            if (reader.GetValue(9) == null) tmp_bp = "";
                            else tmp_bp = reader.GetValue(9).ToString();

                            if (reader.GetValue(10) == null) tmp_purity = "";
                            else tmp_purity = reader.GetValue(10).ToString();

                            molecules.Add(new MoleculeData()
                            {
                                Name = reader.GetValue(1).ToString(),
                                Location = reader.GetValue(2).ToString(),
                                CAS = reader.GetValue(3).ToString(),
                                Mvalue = Double.Parse(tmp_Mvalue),
                                mvalue = tmp_mvalue,
                                Vvalue = tmp_Vvalue,
                                dvalue = tmp_dvalue,
                                mpvalue = tmp_mp,
                                bpvalue = tmp_bp,
                                purity = tmp_purity
                            });
                        }
                        catch (Exception e)
                        {

                        }
                    }

                }
            }

            return molecules;

        }

        public void InitDBFromExcel()
        {

        }
    }
}
