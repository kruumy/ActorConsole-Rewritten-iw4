using System.IO;
using System.Linq;

namespace ActorConsole.Core.Player
{
    public class Precache
    {
        public string Path { get; set; }
        public string RawText { get; private set; }
        public string[] RawText_Lines { get; private set; }
        public string[] MP_Anims { get; private set; }
        public string[] MP_Anims_Idle { get; private set; }
        public string[] MP_Anims_Death { get; private set; }
        public string[] SP_Anims { get; private set; }
        public string[] SP_Models { get; private set; }

        public Precache(string path_to_precache)
        {
            // TODO: optimize using spans instead of arrays

            // reading from file
            Path = path_to_precache;
            RawText = File.ReadAllText(path_to_precache);
            RawText_Lines = RawText.Split('\n').Where(x => !x.Contains("//")).ToArray();

            // parsing all anims
            string[] Anims = RawText_Lines.Where(x => x.Contains("PrecacheMPAnim(")).ToArray();
            for (int i = 0; i < Anims.Length; i++)
            {
                int startOfAnim = Anims[i].IndexOf("PrecacheMPAnim(") + 16;
                Anims[i] = Anims[i].Substring(startOfAnim, Anims[i].LastIndexOf('"') - startOfAnim).Trim();
            }
            // parsing mp anims
            MP_Anims = Anims.Where(x => x.Contains("pb")).ToArray();
            MP_Anims_Idle = MP_Anims.Where(x => !x.Contains("death")).ToArray();
            MP_Anims_Death = MP_Anims.Where(x => x.Contains("death")).ToArray();

            // parsing sp anims
            SP_Anims = Anims.Where(x => !x.Contains("pb")).ToArray();

            // parsing sp_models
            SP_Models = RawText_Lines.Where(x => x.Contains("PrecacheModel(")).ToArray();
            for (int i = 0; i < SP_Models.Length; i++)
            {
                int startOfModel = SP_Models[i].IndexOf("PrecacheModel(") + 15;
                SP_Models[i] = SP_Models[i].Substring(startOfModel, SP_Models[i].LastIndexOf('"') - startOfModel).Trim();
            }
        }
    }
}