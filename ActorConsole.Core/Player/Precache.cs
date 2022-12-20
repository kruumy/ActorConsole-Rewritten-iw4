using System.IO;
using System.Linq;

namespace ActorConsole.Core.Player
{
    /// <summary>
    /// A class to parse the _precache.gsc files contents into its different types.
    /// </summary>
    public class Precache
    {
        /// <summary>
        /// The path giving in the contructor.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The raw text from the file.
        /// </summary>
        public string RawText { get; }

        /// <summary>
        /// The raw text split by lines.
        /// </summary>
        public string[] RawText_Lines { get; }

        /// <summary>
        /// All the multiplayer animations.
        /// </summary>
        public string[] MP_Anims { get; }

        /// <summary>
        /// Only the idle multiplayer animations.
        /// </summary>
        public string[] MP_Anims_Idle { get; }

        /// <summary>
        /// Only the death multiplayer animations.
        /// </summary>
        public string[] MP_Anims_Death { get; }

        /// <summary>
        /// Only the singleplayer animations.
        /// </summary>
        public string[] SP_Anims { get; }

        /// <summary>
        /// All the singleplayer models.
        /// </summary>
        public string[] SP_Models { get; }

        /// <summary>
        /// Sets all propertys of the class with the parsed information.
        /// </summary>
        /// <param name="path_to_precache">path to the _precache.gsc file.</param>
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