using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;
//xceed.com/wp-content/documentation/xceed-words-for-net/webframe.html#rootWelcome.html
using System.Drawing;

namespace word_magic_lib
{   
    class Program
    {
        static void Main(string[] args)
        {
            //docx location at prefered location
            //DocX mydoc = DocX.Create(@"C:\Users\Timea\Desktop\test01.docx");

            //docx location by default: project dir/bin/Debug
            DocX mydoc = DocX.Create("test01.docx");

            //Header table
            Table HeaderTable = mydoc.AddTable(6, 4); //6 sor, 4 oszlop
   
            HeaderTable.Rows[0].Cells[0].Paragraphs[0].Append("Reactioncode:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[0].Cells[1].Paragraphs[0].Append("<reactioncode>");
            HeaderTable.Rows[0].Cells[2].Paragraphs[0].Append("Laboratory").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[0].Cells[3].Paragraphs[0].Append("<lab>");

            HeaderTable.Rows[1].Cells[0].Paragraphs[0].Append("Chemist:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[1].Cells[1].Paragraphs[0].Append("<chemist>");
            HeaderTable.Rows[1].Cells[2].Paragraphs[0].Append("Date of start:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[1].Cells[3].Paragraphs[0].Append("<date>");

            HeaderTable.Rows[2].Cells[0].Paragraphs[0].Append("Chiefchemist:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[2].Cells[1].Paragraphs[0].Append("<chiefchemist>");
            HeaderTable.Rows[2].Cells[2].Paragraphs[0].Append("Date of closure:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[2].Cells[3].Paragraphs[0].Append("<date>");

            HeaderTable.Rows[3].Cells[0].Paragraphs[0].Append("Project:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[3].Cells[1].Paragraphs[0].Append("<project>");

            HeaderTable.Rows[4].Cells[0].Paragraphs[0].Append("Previous step:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[4].Cells[1].Paragraphs[0].Append("<previous step>");

            HeaderTable.Rows[5].Cells[0].Paragraphs[0].Append("Literature:").Bold().Alignment = Alignment.right;
            HeaderTable.Rows[5].Cells[1].Paragraphs[0].Append("<literature>");

            Border nullBorder = new Border();
            nullBorder.Tcbs = BorderStyle.Tcbs_none;
            HeaderTable.SetBorder(TableBorderType.InsideH, nullBorder);
            HeaderTable.SetBorder(TableBorderType.InsideV, nullBorder);
            HeaderTable.SetBorder(TableBorderType.Bottom, nullBorder);
            HeaderTable.SetBorder(TableBorderType.Left, nullBorder);
            HeaderTable.SetBorder(TableBorderType.Right, nullBorder);
            HeaderTable.SetBorder(TableBorderType.Top, nullBorder);
            
            mydoc.InsertTable(HeaderTable);

            //Reaction
            mydoc.InsertParagraph("Reactions:\n").Bold().UnderlineStyle(UnderlineStyle.singleLine);
            
            //Source of the image
            //Xceed.Words.NET.Image img=mydoc.AddImage(@"C:\Users\Timea\Desktop\img.jpg");
            //Dunno the default for it...
            Xceed.Words.NET.Image img = mydoc.AddImage("asd.jpg");

            Picture pic = img.CreatePicture();
            mydoc.InsertParagraph().AppendPicture(pic);

            mydoc.InsertParagraph("Table of materials:\n").Bold().UnderlineStyle(UnderlineStyle.singleLine);

            //Material table
            Table MaterialTable = mydoc.AddTable(6, 10); //6 sor, 10 oszlop
            
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


            MaterialTable.Rows[1].Cells[0].Paragraphs[0].Append("[1,1′-Bis(diphenylphosphino)ferrocene]dichloropalladium(II), complex with dichloromethane");
            MaterialTable.Rows[2].Cells[0].Paragraphs[0].Append("Reagent1");
            MaterialTable.Rows[3].Cells[0].Paragraphs[0].Append("Reagent2");
            MaterialTable.Rows[4].Cells[0].Paragraphs[0].Append("Solvent");
            MaterialTable.Rows[5].Cells[0].Paragraphs[0].Append("Product");
            
            foreach (Cell item in MaterialTable.Rows[5].Cells)
            {
                item.FillColor = Color.LightGray;
            }
            
            mydoc.InsertTable(MaterialTable);

            //Procedure&Observtion
            mydoc.InsertParagraph("\nProcedure:").Bold().UnderlineStyle(UnderlineStyle.singleLine);
            mydoc.InsertParagraph("<procedure text goes here>");
            mydoc.InsertParagraph("\nObservation:").Bold().UnderlineStyle(UnderlineStyle.singleLine);
            //Paragraphs work with strings too
            String asd;
            asd = "<yield value goes here>\n";
            mydoc.InsertParagraph("Yield: "+asd);
            //mydoc.InsertParagraph("<yield value goes here>\n");
            mydoc.InsertParagraph("<observation text goes here>\n<observation imgs goes here>\n");

            //Image source again x2
            //Xceed.Words.NET.Image img2 = mydoc.AddImage(@"C:\Users\Timea\Desktop\img.jpg");
            Xceed.Words.NET.Image img2 = mydoc.AddImage("asd.jpg");
            Picture pic2 = img2.CreatePicture();
            mydoc.InsertParagraph().AppendPicture(pic2);

            //Xceed.Words.NET.Image img3 = mydoc.AddImage(@"C:\Users\Timea\Desktop\img.jpg");
            Xceed.Words.NET.Image img3 = mydoc.AddImage("asd.jpg");
            Picture pic3 = img3.CreatePicture();
            mydoc.InsertParagraph().AppendPicture(pic3);
            
            mydoc.Save();
            //Some kinda visible result + open the generated docx
            Console.WriteLine("done");
            Console.WriteLine("opening....");
            //System.Diagnostics.Process.Start(@"C:\Users\Timea\Desktop\test01.docx");
            System.Diagnostics.Process.Start("test01.docx");
            Console.ReadKey();
        }
    }
}
