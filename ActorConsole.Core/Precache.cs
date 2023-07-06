using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using static ActorConsole.Core.Precache;

namespace ActorConsole.Core
{
    public class Precache : ObservableCollection<KeyValuePair<string, AssetType>>, INotifyPropertyChanged
    {
        public Precache( FileInfo File ) : this(System.IO.File.ReadAllText(File.FullName))
        {
        }
        public Precache( string rawPrecache ) : this()
        {
            Parse(rawPrecache);
        }
        public Precache()
        {
            CollectionChanged += Precache_CollectionChanged;
        }

        public enum AssetType
        {
            xanim,
            xmodel
        }

        public IEnumerable<string> MultiplayerDeathAnim => this.Where(item => item.Value == AssetType.xanim && item.Key.StartsWith("pb_") && item.Key.Contains("death")).Select(i => i.Key);
        public IEnumerable<string> MultiplayerIdleAnims => this.Where(item => item.Value == AssetType.xanim && item.Key.StartsWith("pb_") && !item.Key.Contains("death")).Select(i => i.Key);
        public IEnumerable<string> SingleplayerAnims => this.Where(item => item.Value == AssetType.xanim && !item.Key.StartsWith("pb_")).Select(i => i.Key);
        public IEnumerable<string> SingleplayerModels => this.Where(item => item.Value == AssetType.xmodel).Select(i => i.Key);

        public void Parse( string rawPrecache )
        {
            const string ANIM_FUNCNAME = "precachempanim";
            const string MODEL_FUNCNAME = "precachemodel";
            for ( int i = 0; i < rawPrecache.Length; i++ )
            {
                if ( rawPrecache[ i ] == '/' && rawPrecache[ i + 1 ] == '/' )
                {
                    // entered comment and skipping till end
                    while ( rawPrecache[ i ] != '\n' && rawPrecache[ i ] != '\r' )
                    {
                        i++;
                    }
                }
                else if ( new string(rawPrecache.Skip(i).Take(ANIM_FUNCNAME.Length).ToArray()).ToLower() == ANIM_FUNCNAME )
                {
                    this.Add(new KeyValuePair<string, AssetType>(ParseFromFuncNameIndex(i, ANIM_FUNCNAME.Length), AssetType.xanim));
                }
                else if ( new string(rawPrecache.Skip(i).Take(MODEL_FUNCNAME.Length).ToArray()).ToLower() == MODEL_FUNCNAME )
                {
                    this.Add(new KeyValuePair<string, AssetType>(ParseFromFuncNameIndex(i, MODEL_FUNCNAME.Length), AssetType.xmodel));
                }
            }

            string ParseFromFuncNameIndex( int indexinRawString, int funcNameLength )
            {
                int startindex = indexinRawString + funcNameLength + 2;
                string target = rawPrecache.Substring(startindex);
                return target.Substring(0, target.IndexOf('"'));
            }
        }

        private void Precache_CollectionChanged( object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e )
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(MultiplayerIdleAnims)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(MultiplayerDeathAnim)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(SingleplayerAnims)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(SingleplayerModels)));
        }
    }
}
