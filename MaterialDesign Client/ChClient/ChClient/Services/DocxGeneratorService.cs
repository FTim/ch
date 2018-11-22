using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChClient.Models;
using DocxDropboxUploader;
using DocxGen;

namespace ChClient.Services
{
    public class DocxGeneratorService : IDocxGeneratorService
    {
        public void GenerateSingleReaction(ReactionInfo reaction)
        {
            
            DocxGenerator docgen = new DocxGenerator();

            docgen.FilePath = reaction.SaveLocation;

            docgen.ReactionCode = reaction.Code;
            docgen.Chemist = reaction.Chemist;
            docgen.Chiefchemist = reaction.Chiefchemist;
            docgen.ProjectName = reaction.Project;
            docgen.Laboratory = reaction.Laboratory;
            docgen.StartDate =reaction.StartDate;
            docgen.ClosureDate = reaction.ClosureDate;
            docgen.PreviousStep = reaction.PreviousStep;
            docgen.Literature = reaction.Literature;

            docgen.ReactionImgPath = reaction.ReactionImgPath;
            
            docgen.StartingMaterial = new MoleculeRow
            { Name = reaction.StartingMaterial.Name,
                CAS = reaction.StartingMaterial.CAS,
                MWvalue = reaction.StartingMaterial.MW,
                Ratio = null,
                nvalue = reaction.StartingMaterial.nValue,
                mvalue = reaction.StartingMaterial.mValue,
                Vvalue = reaction.StartingMaterial.VValue,
                Denvalue = reaction.StartingMaterial.Den,
                Bpvalue = reaction.StartingMaterial.bpValue,
                Mpvalue = reaction.StartingMaterial.mpValue };

            
            foreach (var item in reaction.Reagents)
            {
                docgen.AddReagent(new MoleculeRow
                {
                    Name = item.Name,
                    CAS = item.CAS,
                    MWvalue = item.MW,
                    Ratio = item.Ratio,
                    nvalue = item.nValue,
                    mvalue = item.mValue,
                    Vvalue = item.VValue,
                    Denvalue = item.Den,
                    Bpvalue = item.bpValue,
                    Mpvalue = item.mpValue
                });
            }


            foreach (var item in reaction.Solvents)
            {
                docgen.AddSolvent(new MoleculeRow {
                    Name = item.Name,
                    CAS = item.CAS,
                    MWvalue = item.MW,
                    
                    Vvalue = item.VValue,
                    Denvalue = item.Den,
                    Bpvalue = item.bpValue,
                    Mpvalue = item.mpValue
                });
            }


                foreach (var item in reaction.Products)
                {
                    docgen.AddProduct(new MoleculeRow {
                        
                        MWvalue = item.MW,
                        Ratio = item.Ratio,
                        nvalue = item.nValue,
                        mvalue = item.mValue
                        
                    });
                }

                

            



            if (reaction.IsSketch)
            {
                docgen.GenerateReport(true);
            }
            else
            {
                docgen.ProcedureText = reaction.Procedure;
                docgen.Yield = reaction.Yield;
                docgen.ObservationText = reaction.Observation;
                foreach (var item in reaction.ObservationImgPaths)
                    docgen.ObservationImg.Add(item);
                {

                }
                docgen.GenerateReport(false);

            }
            
        }

        public async Task<string> UploadToDropboxAsync(string OSfilepath, string Cfolder, string filename)
        {
            string result;
            result=await Task.Run(() => Uploader.Upload(OSfilepath, Cfolder, filename));
            return result;
        }
    }
}
