using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocxDropboxUploader
{
    public class Uploader
    {
        public static async Task<string> Upload(string OSfilepath, string Cfolder, string filename)
        {
            //ennek az asd kódnak benne kell lennie......
            using (var dbx = new DropboxClient("L-mMIbxtVFYAAAAAAAAAFCxFnMpOTfZzJzh-gtyvRWOqbZqG3JKIAmuA3yAcaUFx"))
            {
                string sourcelocation = OSfilepath;
                //string targetlocation = "/ReactionDocx/try7.docx";
                string targetlocation = "/"+Cfolder+"/"+filename;

                var mem = new MemoryStream(File.ReadAllBytes(sourcelocation));
                var upload = await dbx.Files.UploadAsync(targetlocation, WriteMode.Overwrite.Instance, body: mem);
                var tmp = await dbx.Files.SearchAsync("/"+Cfolder+"/", filename);

               

                if (tmp.Matches.Count == 0)
                {
                    return "Not uploaded...";
                }
                else
                {
                    return "Uploaded!";
                }
                
            }
        }
    }
}
