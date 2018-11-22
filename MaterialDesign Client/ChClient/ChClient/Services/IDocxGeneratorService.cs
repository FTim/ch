using ChClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Services
{
    public interface IDocxGeneratorService
    {
        void GenerateSingleReaction(ReactionInfo reaction);

        Task<string> UploadToDropboxAsync(string OSfilepath, string Cfolder, string filename);
    }
}
