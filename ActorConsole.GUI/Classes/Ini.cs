/*
 *
 *https://github.com/Enichan/Ini
 *
 * from master commit = Oct 20, 2018
 *
 * i converted it to .net framework
 *
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ActorConsole.GUI.Classes
{
    internal struct IniValue
    {
        private static bool TryParseInt( string text, out int value )
        {
            if ( int.TryParse(text,
                System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture,
                out int res) )
            {
                value = res;
                return true;
            }
            value = 0;
            return false;
        }

        private static bool TryParseDouble( string text, out double value )
        {
            if ( double.TryParse(text,
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture,
                out double res) )
            {
                value = res;
                return true;
            }
            value = double.NaN;
            return false;
        }

        public string Value;

        public IniValue( object value )
        {
            if ( value is IFormattable formattable )
            {
                Value = formattable.ToString(null, System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                Value = value?.ToString();
            }
        }

        public IniValue( string value )
        {
            Value = value;
        }

        public bool ToBool( bool valueIfInvalid = false )
        {
            if ( TryConvertBool(out bool res) )
            {
                return res;
            }
            return valueIfInvalid;
        }

        public bool TryConvertBool( out bool result )
        {
            if ( Value == null )
            {
                result = default;
                return false;
            }
            string boolStr = Value.Trim().ToLowerInvariant();
            if ( boolStr == "true" )
            {
                result = true;
                return true;
            }
            else if ( boolStr == "false" )
            {
                result = false;
                return true;
            }
            result = default;
            return false;
        }

        public int ToInt( int valueIfInvalid = 0 )
        {
            if ( TryConvertInt(out int res) )
            {
                return res;
            }
            return valueIfInvalid;
        }

        public bool TryConvertInt( out int result )
        {
            if ( Value == null )
            {
                result = default;
                return false;
            }
            if ( TryParseInt(Value.Trim(), out result) )
            {
                return true;
            }
            return false;
        }

        public double ToDouble( double valueIfInvalid = 0 )
        {
            if ( TryConvertDouble(out double res) )
            {
                return res;
            }
            return valueIfInvalid;
        }

        public bool TryConvertDouble( out double result )
        {
            if ( Value == null )
            {
                result = default;
                return false; ;
            }
            if ( TryParseDouble(Value.Trim(), out result) )
            {
                return true;
            }
            return false;
        }

        public string GetString()
        {
            return GetString(true, false);
        }

        public string GetString( bool preserveWhitespace )
        {
            return GetString(true, preserveWhitespace);
        }

        public string GetString( bool allowOuterQuotes, bool preserveWhitespace )
        {
            if ( Value == null )
            {
                return "";
            }
            string trimmed = Value.Trim();
            if ( allowOuterQuotes && trimmed.Length >= 2 && trimmed[ 0 ] == '"' && trimmed[ trimmed.Length - 1 ] == '"' )
            {
                string inner = trimmed.Substring(1, trimmed.Length - 2);
                return preserveWhitespace ? inner : inner.Trim();
            }
            else
            {
                return preserveWhitespace ? Value : Value.Trim();
            }
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator IniValue( byte o )
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue( short o )
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue( int o )
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue( sbyte o )
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue( ushort o )
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue( uint o )
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue( float o )
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue( double o )
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue( bool o )
        {
            return new IniValue(o);
        }

        public static implicit operator IniValue( string o )
        {
            return new IniValue(o);
        }

        private static readonly IniValue _default = new IniValue();
        public static IniValue Default => _default;
    }

    internal class IniFile : IEnumerable<KeyValuePair<string, IniSection>>, IDictionary<string, IniSection>
    {
        private readonly Dictionary<string, IniSection> sections;
        public IEqualityComparer<string> StringComparer;

        public bool SaveEmptySections = false;

        public IniFile()
            : this(DefaultComparer)
        {
        }

        public IniFile( IEqualityComparer<string> stringComparer )
        {
            StringComparer = stringComparer;
            sections = new Dictionary<string, IniSection>(StringComparer);
        }

        public void Save( string path, FileMode mode = FileMode.Create )
        {
            FileStream stream = new FileStream(path, mode, FileAccess.Write);
            Save(stream);
            stream.Dispose();
        }

        public void Save( Stream stream )
        {
            StreamWriter writer = new StreamWriter(stream);
            Save(writer);
            writer.Dispose();
        }

        public void Save( StreamWriter writer )
        {
            foreach ( KeyValuePair<string, IniSection> section in sections )
            {
                if ( section.Value.Count > 0 || SaveEmptySections )
                {
                    writer.WriteLine(string.Format("[{0}]", section.Key.Trim()));
                    foreach ( KeyValuePair<string, IniValue> kvp in section.Value )
                    {
                        writer.WriteLine(string.Format("{0}={1}", kvp.Key, kvp.Value));
                    }
                    writer.WriteLine("");
                }
            }
        }

        public void Load( string path, bool ordered = false )
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Load(stream, ordered);
            stream.Dispose();
        }

        public void Load( Stream stream, bool ordered = false )
        {
            StreamReader reader = new StreamReader(stream);
            Load(reader, ordered);
            reader.Dispose();
        }

        public void Load( StreamReader reader, bool ordered = false )
        {
            IniSection section = null;

            while ( !reader.EndOfStream )
            {
                string line = reader.ReadLine();

                if ( line != null )
                {
                    string trimStart = line.TrimStart();

                    if ( trimStart.Length > 0 )
                    {
                        if ( trimStart[ 0 ] == '[' )
                        {
                            int sectionEnd = trimStart.IndexOf(']');
                            if ( sectionEnd > 0 )
                            {
                                string sectionName = trimStart.Substring(1, sectionEnd - 1).Trim();
                                section = new IniSection(StringComparer) { Ordered = ordered };
                                sections[ sectionName ] = section;
                            }
                        }
                        else if ( section != null && trimStart[ 0 ] != ';' )
                        {
                            if ( LoadValue(line, out string key, out IniValue val) )
                            {
                                section[ key ] = val;
                            }
                        }
                    }
                }
            }
        }

        private bool LoadValue( string line, out string key, out IniValue val )
        {
            int assignIndex = line.IndexOf('=');
            if ( assignIndex <= 0 )
            {
                key = null;
                val = null;
                return false;
            }

            key = line.Substring(0, assignIndex).Trim();
            string value = line.Substring(assignIndex + 1);

            val = new IniValue(value);
            return true;
        }

        public bool ContainsSection( string section )
        {
            return sections.ContainsKey(section);
        }

        public bool TryGetSection( string section, out IniSection result )
        {
            return sections.TryGetValue(section, out result);
        }

        bool IDictionary<string, IniSection>.TryGetValue( string key, out IniSection value )
        {
            return TryGetSection(key, out value);
        }

        public bool Remove( string section )
        {
            return sections.Remove(section);
        }

        public IniSection Add( string section, Dictionary<string, IniValue> values, bool ordered = false )
        {
            return Add(section, new IniSection(values, StringComparer) { Ordered = ordered });
        }

        public IniSection Add( string section, IniSection value )
        {
            if ( value.Comparer != StringComparer )
            {
                value = new IniSection(value, StringComparer);
            }
            sections.Add(section, value);
            return value;
        }

        public IniSection Add( string section, bool ordered = false )
        {
            IniSection value = new IniSection(StringComparer) { Ordered = ordered };
            sections.Add(section, value);
            return value;
        }

        void IDictionary<string, IniSection>.Add( string key, IniSection value )
        {
            Add(key, value);
        }

        bool IDictionary<string, IniSection>.ContainsKey( string key )
        {
            return ContainsSection(key);
        }

        public ICollection<string> Keys => sections.Keys;

        public ICollection<IniSection> Values => sections.Values;

        void ICollection<KeyValuePair<string, IniSection>>.Add( KeyValuePair<string, IniSection> item )
        {
            ((IDictionary<string, IniSection>)sections).Add(item);
        }

        public void Clear()
        {
            sections.Clear();
        }

        bool ICollection<KeyValuePair<string, IniSection>>.Contains( KeyValuePair<string, IniSection> item )
        {
            return ((IDictionary<string, IniSection>)sections).Contains(item);
        }

        void ICollection<KeyValuePair<string, IniSection>>.CopyTo( KeyValuePair<string, IniSection>[] array, int arrayIndex )
        {
            ((IDictionary<string, IniSection>)sections).CopyTo(array, arrayIndex);
        }

        public int Count => sections.Count;

        bool ICollection<KeyValuePair<string, IniSection>>.IsReadOnly => ((IDictionary<string, IniSection>)sections).IsReadOnly;

        bool ICollection<KeyValuePair<string, IniSection>>.Remove( KeyValuePair<string, IniSection> item )
        {
            return ((IDictionary<string, IniSection>)sections).Remove(item);
        }

        public IEnumerator<KeyValuePair<string, IniSection>> GetEnumerator()
        {
            return sections.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IniSection this[ string section ]
        {
            get
            {
                if ( sections.TryGetValue(section, out IniSection s) )
                {
                    return s;
                }
                s = new IniSection(StringComparer);
                sections[ section ] = s;
                return s;
            }
            set
            {
                IniSection v = value;
                if ( v.Comparer != StringComparer )
                {
                    v = new IniSection(v, StringComparer);
                }
                sections[ section ] = v;
            }
        }

        public string GetContents()
        {
            MemoryStream stream = new MemoryStream();
            Save(stream);
            stream.Flush();
            StringBuilder builder = new StringBuilder(Encoding.UTF8.GetString(stream.ToArray()));
            stream.Dispose();
            return builder.ToString();
        }

        public static IEqualityComparer<string> DefaultComparer = new CaseInsensitiveStringComparer();

        private class CaseInsensitiveStringComparer : IEqualityComparer<string>
        {
            public bool Equals( string x, string y )
            {
                return string.Compare(x, y, true) == 0;
            }

            public int GetHashCode( string obj )
            {
                return obj.ToLowerInvariant().GetHashCode();
            }

#if JS
        public new bool Equals(object x, object y) {
            var xs = x as string;
            var ys = y as string;
            if (xs == null || ys == null) {
                return xs == null && ys == null;
            }
            return Equals(xs, ys);
        }

        public int GetHashCode(object obj) {
            if (obj is string) {
                return GetHashCode((string)obj);
            }
            return obj.ToStringInvariant().ToLowerInvariant().GetHashCode();
        }
#endif
        }
    }

    internal class IniSection : IEnumerable<KeyValuePair<string, IniValue>>, IDictionary<string, IniValue>
    {
        private readonly Dictionary<string, IniValue> values;

        #region Ordered

        private List<string> orderedKeys;

        public int IndexOf( string key )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call IndexOf(string) on IniSection: section was not ordered.");
            }
            return IndexOf(key, 0, orderedKeys.Count);
        }

        public int IndexOf( string key, int index )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call IndexOf(string, int) on IniSection: section was not ordered.");
            }
            return IndexOf(key, index, orderedKeys.Count - index);
        }

        public int IndexOf( string key, int index, int count )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call IndexOf(string, int, int) on IniSection: section was not ordered.");
            }
            if ( index < 0 || index > orderedKeys.Count )
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            if ( count < 0 )
            {
                throw new IndexOutOfRangeException("Count cannot be less than zero." + Environment.NewLine + "Parameter name: count");
            }
            if ( index + count > orderedKeys.Count )
            {
                throw new ArgumentException("Index and count were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            int end = index + count;
            for ( int i = index; i < end; i++ )
            {
                if ( Comparer.Equals(orderedKeys[ i ], key) )
                {
                    return i;
                }
            }
            return -1;
        }

        public int LastIndexOf( string key )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call LastIndexOf(string) on IniSection: section was not ordered.");
            }
            return LastIndexOf(key, 0, orderedKeys.Count);
        }

        public int LastIndexOf( string key, int index )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call LastIndexOf(string, int) on IniSection: section was not ordered.");
            }
            return LastIndexOf(key, index, orderedKeys.Count - index);
        }

        public int LastIndexOf( string key, int index, int count )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call LastIndexOf(string, int, int) on IniSection: section was not ordered.");
            }
            if ( index < 0 || index > orderedKeys.Count )
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            if ( count < 0 )
            {
                throw new IndexOutOfRangeException("Count cannot be less than zero." + Environment.NewLine + "Parameter name: count");
            }
            if ( index + count > orderedKeys.Count )
            {
                throw new ArgumentException("Index and count were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            int end = index + count;
            for ( int i = end - 1; i >= index; i-- )
            {
                if ( Comparer.Equals(orderedKeys[ i ], key) )
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert( int index, string key, IniValue value )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call Insert(int, string, IniValue) on IniSection: section was not ordered.");
            }
            if ( index < 0 || index > orderedKeys.Count )
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            values.Add(key, value);
            orderedKeys.Insert(index, key);
        }

        public void InsertRange( int index, IEnumerable<KeyValuePair<string, IniValue>> collection )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call InsertRange(int, IEnumerable<KeyValuePair<string, IniValue>>) on IniSection: section was not ordered.");
            }
            if ( collection == null )
            {
                throw new ArgumentNullException("Value cannot be null." + Environment.NewLine + "Parameter name: collection");
            }
            if ( index < 0 || index > orderedKeys.Count )
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            foreach ( KeyValuePair<string, IniValue> kvp in collection )
            {
                Insert(index, kvp.Key, kvp.Value);
                index++;
            }
        }

        public void RemoveAt( int index )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call RemoveAt(int) on IniSection: section was not ordered.");
            }
            if ( index < 0 || index > orderedKeys.Count )
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            string key = orderedKeys[ index ];
            orderedKeys.RemoveAt(index);
            values.Remove(key);
        }

        public void RemoveRange( int index, int count )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call RemoveRange(int, int) on IniSection: section was not ordered.");
            }
            if ( index < 0 || index > orderedKeys.Count )
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            if ( count < 0 )
            {
                throw new IndexOutOfRangeException("Count cannot be less than zero." + Environment.NewLine + "Parameter name: count");
            }
            if ( index + count > orderedKeys.Count )
            {
                throw new ArgumentException("Index and count were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            for ( int i = 0; i < count; i++ )
            {
                RemoveAt(index);
            }
        }

        public void Reverse()
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call Reverse() on IniSection: section was not ordered.");
            }
            orderedKeys.Reverse();
        }

        public void Reverse( int index, int count )
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call Reverse(int, int) on IniSection: section was not ordered.");
            }
            if ( index < 0 || index > orderedKeys.Count )
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            if ( count < 0 )
            {
                throw new IndexOutOfRangeException("Count cannot be less than zero." + Environment.NewLine + "Parameter name: count");
            }
            if ( index + count > orderedKeys.Count )
            {
                throw new ArgumentException("Index and count were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            orderedKeys.Reverse(index, count);
        }

        public ICollection<IniValue> GetOrderedValues()
        {
            if ( !Ordered )
            {
                throw new InvalidOperationException("Cannot call GetOrderedValues() on IniSection: section was not ordered.");
            }
            List<IniValue> list = new List<IniValue>();
            for ( int i = 0; i < orderedKeys.Count; i++ )
            {
                list.Add(values[ orderedKeys[ i ] ]);
            }
            return list;
        }

        public IniValue this[ int index ]
        {
            get
            {
                if ( !Ordered )
                {
                    throw new InvalidOperationException("Cannot index IniSection using integer key: section was not ordered.");
                }
                if ( index < 0 || index >= orderedKeys.Count )
                {
                    throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
                }
                return values[ orderedKeys[ index ] ];
            }
            set
            {
                if ( !Ordered )
                {
                    throw new InvalidOperationException("Cannot index IniSection using integer key: section was not ordered.");
                }
                if ( index < 0 || index >= orderedKeys.Count )
                {
                    throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
                }
                string key = orderedKeys[ index ];
                values[ key ] = value;
            }
        }

        public bool Ordered
        {
            get => orderedKeys != null;
            set
            {
                if ( Ordered != value )
                {
                    orderedKeys = value ? new List<string>(values.Keys) : null;
                }
            }
        }

        #endregion Ordered

        public IniSection()
            : this(IniFile.DefaultComparer)
        {
        }

        public IniSection( IEqualityComparer<string> stringComparer )
        {
            values = new Dictionary<string, IniValue>(stringComparer);
        }

        public IniSection( Dictionary<string, IniValue> values )
            : this(values, IniFile.DefaultComparer)
        {
        }

        public IniSection( Dictionary<string, IniValue> values, IEqualityComparer<string> stringComparer )
        {
            this.values = new Dictionary<string, IniValue>(values, stringComparer);
        }

        public IniSection( IniSection values )
            : this(values, IniFile.DefaultComparer)
        {
        }

        public IniSection( IniSection values, IEqualityComparer<string> stringComparer )
        {
            this.values = new Dictionary<string, IniValue>(values.values, stringComparer);
        }

        public void Add( string key, IniValue value )
        {
            values.Add(key, value);
            if ( Ordered )
            {
                orderedKeys.Add(key);
            }
        }

        public bool ContainsKey( string key )
        {
            return values.ContainsKey(key);
        }

        /// <summary>
        /// Returns this IniSection's collection of keys. If the IniSection is ordered, the keys will be returned in order.
        /// </summary>
        public ICollection<string> Keys
        {
            get
            {
                if ( Ordered )
                {
                    return orderedKeys;
                }
                else
                {
                    return values.Keys;
                }
            }
        }

        public bool Remove( string key )
        {
            bool ret = values.Remove(key);
            if ( Ordered && ret )
            {
                for ( int i = 0; i < orderedKeys.Count; i++ )
                {
                    if ( Comparer.Equals(orderedKeys[ i ], key) )
                    {
                        orderedKeys.RemoveAt(i);
                        break;
                    }
                }
            }
            return ret;
        }

        public bool TryGetValue( string key, out IniValue value )
        {
            return values.TryGetValue(key, out value);
        }

        /// <summary>
        /// Returns the values in this IniSection. These values are always out of order. To get ordered values from an IniSection call GetOrderedValues instead.
        /// </summary>
        public ICollection<IniValue> Values => values.Values;

        void ICollection<KeyValuePair<string, IniValue>>.Add( KeyValuePair<string, IniValue> item )
        {
            ((IDictionary<string, IniValue>)values).Add(item);
            if ( Ordered )
            {
                orderedKeys.Add(item.Key);
            }
        }

        public void Clear()
        {
            values.Clear();
            if ( Ordered )
            {
                orderedKeys.Clear();
            }
        }

        bool ICollection<KeyValuePair<string, IniValue>>.Contains( KeyValuePair<string, IniValue> item )
        {
            return ((IDictionary<string, IniValue>)values).Contains(item);
        }

        void ICollection<KeyValuePair<string, IniValue>>.CopyTo( KeyValuePair<string, IniValue>[] array, int arrayIndex )
        {
            ((IDictionary<string, IniValue>)values).CopyTo(array, arrayIndex);
        }

        public int Count => values.Count;

        bool ICollection<KeyValuePair<string, IniValue>>.IsReadOnly => ((IDictionary<string, IniValue>)values).IsReadOnly;

        bool ICollection<KeyValuePair<string, IniValue>>.Remove( KeyValuePair<string, IniValue> item )
        {
            bool ret = ((IDictionary<string, IniValue>)values).Remove(item);
            if ( Ordered && ret )
            {
                for ( int i = 0; i < orderedKeys.Count; i++ )
                {
                    if ( Comparer.Equals(orderedKeys[ i ], item.Key) )
                    {
                        orderedKeys.RemoveAt(i);
                        break;
                    }
                }
            }
            return ret;
        }

        public IEnumerator<KeyValuePair<string, IniValue>> GetEnumerator()
        {
            if ( Ordered )
            {
                return GetOrderedEnumerator();
            }
            else
            {
                return values.GetEnumerator();
            }
        }

        private IEnumerator<KeyValuePair<string, IniValue>> GetOrderedEnumerator()
        {
            for ( int i = 0; i < orderedKeys.Count; i++ )
            {
                yield return new KeyValuePair<string, IniValue>(orderedKeys[ i ], values[ orderedKeys[ i ] ]);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEqualityComparer<string> Comparer => values.Comparer;

        public IniValue this[ string name ]
        {
            get
            {
                if ( values.TryGetValue(name, out IniValue val) )
                {
                    return val;
                }
                return IniValue.Default;
            }
            set
            {
                if ( Ordered && !orderedKeys.Contains(name) )
                {
                    orderedKeys.Add(name);
                }
                values[ name ] = value;
            }
        }

        public static implicit operator IniSection( Dictionary<string, IniValue> dict )
        {
            return new IniSection(dict);
        }

        public static explicit operator Dictionary<string, IniValue>( IniSection section )
        {
            return section.values;
        }
    }
}