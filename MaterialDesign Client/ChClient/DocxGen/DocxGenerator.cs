using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;
using System.Drawing;

namespace DocxGen
{
    public class DocxGenerator
    {
        public string FilePath { get; set; }
        DocX report; //any better?

        public string ReactionCode { get; set; }
        public string Chemist { get; set; }
        public string Chiefchemist { get; set; }
        public string ProjectName { get; set; }
        public string PreviousStep { get; set; }
        public string Literature { get; set; }
        public string Laboratory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ClosureDate { get; set; }

        public string ReactionImgPath { get; set; }
        public MoleculeRow StartingMaterial { get; set; }
        public List<MoleculeRow> Reagents;
        public List<MoleculeRow> Solvents;
        public MoleculeRow Product { get; set; }

        public string ProcedureText { get; set; }

        public string Yield { get; set; }
        public string ObservationText { get; set; }
        public List<string> ObservationImg;

        public DocxGenerator()
        {
            //contsr->List init
            Reagents = new List<MoleculeRow>();
            Solvents = new List<MoleculeRow>();
            ObservationImg = new List<string>();
        }
        public void Create()
        {
            report = DocX.Create(FilePath);
        }
        public void Save()
        {
            report.Save();
        }
        public void GenerateReport(bool sketch)
        {
            //fill FilePath property first!
            report = DocX.Create(FilePath);
            //fill headerproperties first!
            GenerateHeader(!sketch);
            //fill startingmaterial properties first & add reagents+solvents!
            CalculateValues();
            GenerateReaction();
            if (!sketch)
            {
                //fill procedure properties first
                GenerateProcedure();
                //fill observation properties first
                GenerateObservation();
            }

            report.Save();
            //Console.WriteLine("Saved!/nOpening....");
            //System.Diagnostics.Process.Start(FilePath);
            //Console.WriteLine("Done");
            //Console.ReadKey();
        }


        public void AddReagent(MoleculeRow reagent)
        {
            Reagents.Add(reagent);
        }

        public void AddSolvent(MoleculeRow solvent)
        {
            Solvents.Add(solvent);
        }
        public void GenerateHeader(Boolean closed)
        {
            Table HeaderTable = report.AddTable(6, 4); //6 sor, 4 oszlop; fix méret

            HeaderTable.Rows[0].Cells[0].Paragraphs[0].Append("Reactioncode:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[0].Cells[1].Paragraphs[0].Append(ReactionCode);
            HeaderTable.Rows[0].Cells[2].Paragraphs[0].Append("Laboratory").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[0].Cells[3].Paragraphs[0].Append(Laboratory);

            HeaderTable.Rows[1].Cells[0].Paragraphs[0].Append("Chemist:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[1].Cells[1].Paragraphs[0].Append(Chemist);
            HeaderTable.Rows[1].Cells[2].Paragraphs[0].Append("Date of start:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[1].Cells[3].Paragraphs[0].Append(StartDate.ToShortDateString()); //Date->string!!!

            HeaderTable.Rows[2].Cells[0].Paragraphs[0].Append("Chiefchemist:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[2].Cells[1].Paragraphs[0].Append(Chiefchemist);
            HeaderTable.Rows[2].Cells[2].Paragraphs[0].Append("Date of closure:").Bold().Alignment = Alignment.right;
            if (closed) HeaderTable.Rows[2].Cells[3].Paragraphs[0].Append(ClosureDate.ToShortDateString()); //Date->string!!!

            HeaderTable.Rows[3].Cells[0].Paragraphs[0].Append("Project:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[3].Cells[1].Paragraphs[0].Append(ProjectName);

            HeaderTable.Rows[4].Cells[0].Paragraphs[0].Append("Previous step:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[4].Cells[1].Paragraphs[0].Append(PreviousStep);

            HeaderTable.Rows[5].Cells[0].Paragraphs[0].Append("Literature:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[5].Cells[1].Paragraphs[0].Append(Literature);

            Border nullBorder = new Border();
            nullBorder.Tcbs = BorderStyle.Tcbs_none;
            HeaderTable.SetBorder(TableBorderType.InsideH, nullBorder);
            HeaderTable.SetBorder(TableBorderType.InsideV, nullBorder);
            HeaderTable.SetBorder(TableBorderType.Bottom, nullBorder);
            HeaderTable.SetBorder(TableBorderType.Left, nullBorder);
            HeaderTable.SetBorder(TableBorderType.Right, nullBorder);
            HeaderTable.SetBorder(TableBorderType.Top, nullBorder);

            report.InsertTable(HeaderTable);
            Console.WriteLine("Header generated");
        }

        public void CalculateValues()
        {
            StartingMaterial.CalculateStartingMaterialValues();
            foreach (MoleculeRow item in Reagents)
            {
                item.CalculateReagentValues(StartingMaterial);
            }

            Product.CalculateProductValues();

            //product számolás pls!
        }

        public void GenerateReaction()
        {
            report.InsertParagraph("Reactions:\n").Bold().UnderlineStyle(UnderlineStyle.singleLine);

            Xceed.Words.NET.Image reactionimg = report.AddImage(ReactionImgPath);

            Picture reactionpic = reactionimg.CreatePicture();


            report.InsertParagraph().AppendPicture(reactionpic);

            report.InsertParagraph("Table of materials:\n").Bold().UnderlineStyle(UnderlineStyle.singleLine);

            //Material table
            int rowcnt = 2 + Reagents.Count + Solvents.Count + 1;
            //header -> x1; starting material-> 1x; reagent cnt; solvent cnt; product -> 1x
            Table MaterialTable = report.AddTable(rowcnt, 10);

            float[] widths = new float[10] { 50, 50, 50, 50, 60, 45, 40, 40, 40, 40 };
            MaterialTable.SetWidths(widths);
            //ugly but... meh...
            MaterialTable.Rows[0].Cells[0].Paragraphs[0].Append("Name").Bold();
            MaterialTable.Rows[0].Cells[1].Paragraphs[0].Append("Code").Bold();
            MaterialTable.Rows[0].Cells[2].Paragraphs[0].Append("MW").Bold();
            MaterialTable.Rows[0].Cells[3].Paragraphs[0].Append("Ratio").Bold();
            MaterialTable.Rows[0].Cells[4].Paragraphs[0].Append("n (mmol)").Bold();
            MaterialTable.Rows[0].Cells[5].Paragraphs[0].Append("m (mg)").Bold();
            MaterialTable.Rows[0].Cells[6].Paragraphs[0].Append("V (uL)").Bold();
            MaterialTable.Rows[0].Cells[7].Paragraphs[0].Append("Den.").Bold();
            MaterialTable.Rows[0].Cells[8].Paragraphs[0].Append("Mp.").Bold();
            MaterialTable.Rows[0].Cells[9].Paragraphs[0].Append("Bp.").Bold();

            //starting material
            InsertRow(MaterialTable, StartingMaterial, 1);

            int actualrow = 2;
            //reagents
            foreach (MoleculeRow item in Reagents)
            {
                InsertRow(MaterialTable, item, actualrow);
                actualrow++;
            }

            //solvents
            foreach (MoleculeRow item in Solvents)
            {
                InsertRow(MaterialTable, item, actualrow);
                actualrow++;
            }

            //product

            InsertRow(MaterialTable, Product, actualrow);

            //last row background
            foreach (Cell item in MaterialTable.Rows[rowcnt - 1].Cells)
            {
                item.FillColor = Color.LightGray;
            }

            report.InsertTable(MaterialTable);

            //Console.WriteLine("Material table generated");
        }

        public void InsertRow(Table tab, MoleculeRow molecule, int rowindex)
        {
            tab.Rows[rowindex].Cells[0].Paragraphs[0].Append(molecule.Name);
            tab.Rows[rowindex].Cells[1].Paragraphs[0].Append(molecule.CAS);
            tab.Rows[rowindex].Cells[2].Paragraphs[0].Append(molecule.MWvalue.ToString());
            tab.Rows[rowindex].Cells[3].Paragraphs[0].Append(molecule.Ratio.ToString());
            tab.Rows[rowindex].Cells[4].Paragraphs[0].Append(molecule.nvalue.ToString());
            tab.Rows[rowindex].Cells[5].Paragraphs[0].Append(molecule.mvalue.ToString());
            tab.Rows[rowindex].Cells[6].Paragraphs[0].Append(molecule.Vvalue.ToString());
            tab.Rows[rowindex].Cells[7].Paragraphs[0].Append(molecule.Denvalue.ToString());
            tab.Rows[rowindex].Cells[8].Paragraphs[0].Append(molecule.Mpvalue.ToString());
            tab.Rows[rowindex].Cells[9].Paragraphs[0].Append(molecule.Bpvalue.ToString());
        }

        public void GenerateProcedure()
        {
            report.InsertParagraph("Procedure:").Bold().UnderlineStyle(UnderlineStyle.singleLine);
            report.InsertParagraph(ProcedureText);

            Console.WriteLine("Procedure generated");
        }

        public void GenerateObservation()
        {
            report.InsertParagraph("Observation:").Bold().UnderlineStyle(UnderlineStyle.singleLine);
            report.InsertParagraph("Yield: " + Yield);
            report.InsertParagraph(ObservationText);

            foreach (string actualimg in ObservationImg)
            {
                Xceed.Words.NET.Image observtionimg = report.AddImage(actualimg);
                Picture observationpic = observtionimg.CreatePicture();
                report.InsertParagraph().AppendPicture(observationpic);
            }

            Console.WriteLine("Observation generated");
        }
    }

}
