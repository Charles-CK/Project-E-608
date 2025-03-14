using System;
using E_CC;

namespace Audio_Transcription
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            scc_embedding embedder = new scc_embedding();
            embedder.EmbedSccIntoVideo();
        }
    }
}


