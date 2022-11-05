namespace ActorConsole.Core
{
    public class Precache
    {
        public string RawText { get; private set; }
        public string[] RawText_Lines { get; private set; }
        public string[] MP_Anims { get; private set; }
        public string[] MP_Anims_Idle { get; private set; }
        public string[] MP_Anims_Death { get; private set; }

        public string[] SP_Anims { get; private set; }
        public string[] SP_Models_Head { get; private set; }
        public string[] SP_Models_Body { get; private set; }

        public Precache(string path_to_precache)
        {
            RawText = File.ReadAllText(path_to_precache);
            RawText_Lines = File.ReadAllLines(path_to_precache);

            MP_Anims = RawText.Split('"').Where(x => x.Contains("pb")).ToArray();
            MP_Anims_Idle = MP_Anims.Where(x => !x.Contains("death")).ToArray();
            MP_Anims_Death = MP_Anims.Where(x => x.Contains("death")).ToArray();
        }
    }



}
