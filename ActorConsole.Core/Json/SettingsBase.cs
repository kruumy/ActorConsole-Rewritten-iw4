using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;

namespace ActorConsole.Core.Json
{
    public abstract class SettingsBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged( string propertyname )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
            if ( PushOnPropertyChanged )
            {
                Push();
            }
        }
        [IgnoreDataMember]
        public FileInfo File { get; }
        [IgnoreDataMember]
        public bool PushOnPropertyChanged { get; set; }
        public SettingsBase( FileInfo File, bool PushOnPropertyChanged )
        {
            this.PushOnPropertyChanged = PushOnPropertyChanged;
            this.File = File;
            if ( this.File.Exists )
            {
                Pull();
            }
        }


        public void Pull()
        {
            File.Refresh();
            string rawJson = System.IO.File.ReadAllText(File.FullName);
            Dictionary<string, object> obj = TinyJson.JSONParser.FromJson<Dictionary<string, object>>(rawJson);
            foreach ( KeyValuePair<string, object> entry in obj )
            {
                System.Type type = this.GetType();
                System.Reflection.PropertyInfo prop = type.GetProperty(entry.Key);
                prop?.SetValue(this, entry.Value);
            }
        }

        public void Push()
        {
            string rawJson = TinyJson.JSONWriter.ToJson(this);
            System.IO.File.WriteAllText(File.FullName, rawJson);
            File.Refresh();
        }
    }
}
