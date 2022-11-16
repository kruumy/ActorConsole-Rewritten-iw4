using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ActorConsole.Core.Memory
{
    /*
     * uses code from 
     * https://github.com/erfg12/memory.dll/
     * https://github.com/shiversoftdev/External/
     * 
     */
    public class MemoryLite
    {
        #region DllImports
        [DllImport("kernel32.dll")]
        private static extern PointerEx OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(PointerEx hProcess, PointerEx lpBaseAddress, [Out] byte[] lpBuffer, PointerEx dwSize, ref PointerEx lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(PointerEx hProcess, PointerEx lpBaseAddress, byte[] lpBuffer, int dwSize, ref PointerEx lpNumberOfBytesWritten);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool VirtualProtectEx(IntPtr processHandle, IntPtr address, int size, int protectionType, out int oldProtectionType);

        #endregion

        #region Properties
        public Process BaseProcess { get; private set; }
        public PointerEx Handle { get; private set; }
        public PointerEx BaseAddress => BaseProcess?.MainModule.BaseAddress ?? IntPtr.Zero;
        #endregion

        #region Constructors
        public MemoryLite(Process target_process)
        {
            BaseProcess = target_process;

            const int PROCESS_ACCESS = 0x0400 | 0x0010 | 0x0020 | 0x0008 | 0x02 | 0x0040;
            Handle = OpenProcess(PROCESS_ACCESS, false, BaseProcess.Id);
        }
        #endregion

        #region Read&Write
        public byte[] ReadBytes(PointerEx addr, PointerEx NumOfBytes)
        {
            byte[] data = new byte[NumOfBytes];
            PointerEx bytesRead = IntPtr.Zero;
            ReadProcessMemory(Handle, addr, data, NumOfBytes, ref bytesRead);
            return data;
        }
        public void WriteBytes(PointerEx addr, byte[] data)
        {
            PointerEx bytesWritten = IntPtr.Zero;
            VirtualProtectEx(Handle, addr, data.Length, 0x40, out int oldProtection);
            WriteProcessMemory(Handle, addr, data, data.Length, ref bytesWritten);
            VirtualProtectEx(Handle, addr, data.Length, oldProtection, out int _);
        }
        public T Read<T>(PointerEx addr) where T : new()
        {
            PointerEx size = Marshal.SizeOf(new T());
            byte[] data = ReadBytes(addr, size);
            if (typeof(T) == typeof(IntPtr) || typeof(T) == typeof(PointerEx))
            {
                IntPtr val = IntPtr.Size == sizeof(int) ? (IntPtr)BitConverter.ToInt32(data, 0) : (IntPtr)BitConverter.ToInt64(data, 0);
                if (typeof(T) == typeof(IntPtr)) return (dynamic)val;
                return (dynamic)new PointerEx(val);
            }
            if (typeof(T) == typeof(float)) return (dynamic)BitConverter.ToSingle(data, 0);
            if (typeof(T) == typeof(long)) return (dynamic)BitConverter.ToInt64(data, 0);
            if (typeof(T) == typeof(ulong)) return (dynamic)BitConverter.ToUInt64(data, 0);
            if (typeof(T) == typeof(int)) return (dynamic)BitConverter.ToInt32(data, 0);
            if (typeof(T) == typeof(uint)) return (dynamic)BitConverter.ToUInt32(data, 0);
            if (typeof(T) == typeof(short)) return (dynamic)BitConverter.ToInt16(data, 0);
            if (typeof(T) == typeof(ushort)) return (dynamic)BitConverter.ToUInt16(data, 0);
            if (typeof(T) == typeof(byte)) return (dynamic)data[0];
            if (typeof(T) == typeof(sbyte)) return (dynamic)data[0];
            throw new Exception($"Invalid Type, {typeof(T)}");
        }
        public void Write<T>(PointerEx addr, T value) where T : new()
        {
            byte[] data = Array.Empty<byte>();
            if (value is IntPtr ip) data = IntPtr.Size == sizeof(int) ? BitConverter.GetBytes(ip.ToInt32()) : BitConverter.GetBytes(ip.ToInt64());
            else if (value is PointerEx ipx) data = IntPtr.Size == sizeof(int) ? BitConverter.GetBytes(ipx.IntPtr.ToInt32()) : BitConverter.GetBytes(ipx.IntPtr.ToInt64());
            else if (value is float f) data = BitConverter.GetBytes(f);
            else if (value is long l) data = BitConverter.GetBytes(l);
            else if (value is ulong ul) data = BitConverter.GetBytes(ul);
            else if (value is int i) data = BitConverter.GetBytes(i);
            else if (value is uint ui) data = BitConverter.GetBytes(ui);
            else if (value is short s) data = BitConverter.GetBytes(s);
            else if (value is ushort u) data = BitConverter.GetBytes(u);
            else if (value is byte b) data = new byte[] { b };
            else if (value is sbyte sb) data = new byte[] { (byte)sb };
            else throw new Exception($"Invalid Type, {typeof(T)}");
            WriteBytes(addr, data);
        }
        public T[] ReadArray<T>(PointerEx absoluteAddress, PointerEx numItems) where T : struct
        {
            // Modified to do less IPC reads, in exchange for slightly worse local performance via 
            T[] arr = new T[numItems];
            int size = Marshal.SizeOf(typeof(T));
            IEnumerable<byte> data = ReadBytes(absoluteAddress, numItems * size);
            for (int i = 0; i < numItems; i++)
            {
                arr[i] = data.Skip(i * size).Take(size).ToArray().ToStruct<T>();
            }
            return arr;
        }

        public void WriteArray<T>(PointerEx absoluteAddress, T[] array) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] writeData = new byte[size * array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i].ToByteArray().CopyTo(writeData, i * size);
            }
            WriteBytes(absoluteAddress, writeData);
        }


        public string ReadString(PointerEx addr, int MaxLength = -1, int buffSize = 256)
        {
            if (MaxLength < 0)
            {
                int MaxStringReadLength = 1023;
                MaxLength = MaxStringReadLength;
            }
            byte[] buffer;
            byte[] rawString = new byte[MaxLength + 1];
            int bytesRead = 0;
            while (bytesRead < MaxLength)
            {
                buffer = ReadBytes(addr + bytesRead, buffSize);
                for (int i = 0; i < buffer.Length && i + bytesRead < MaxLength; i++)
                {
                    if (buffer[i] == 0) return rawString.String();
                    rawString[bytesRead + i] = buffer[i];
                }
                bytesRead += buffSize;
            }
            return rawString.String();
        }

        public void WriteString(PointerEx addr, string Value)
        {
            WriteArray(addr, Value.Bytes());
        }


        #endregion
    }
    #region PointerEx
    public struct PointerEx
    {
        public IntPtr IntPtr { get; set; }
        public PointerEx(IntPtr value)
        {
            IntPtr = value;
        }
        public PointerEx(byte value)
        {
            IntPtr = new IntPtr(value);
        }
        public PointerEx(int value)
        {
            IntPtr = new IntPtr(value);
        }
        public PointerEx(long value)
        {
            IntPtr = new IntPtr(value);
        }

        #region overrides
        public static implicit operator IntPtr(PointerEx px)
        {
            return px.IntPtr;
        }

        public static implicit operator PointerEx(IntPtr ip)
        {
            return new PointerEx(ip);
        }

        public static PointerEx operator +(PointerEx px, PointerEx pxo)
        {
            return px.Add(pxo);
        }

        public static PointerEx operator -(PointerEx px, PointerEx pxo)
        {
            return px.Subtract(pxo);
        }

        public static PointerEx operator &(PointerEx px, PointerEx pxo)
        {
            return IntPtr.Size == sizeof(int) ? ((int)px & (int)pxo) : ((long)px & (long)pxo);
        }

        public static bool operator ==(PointerEx px, PointerEx pxo)
        {
            return px.IntPtr == pxo.IntPtr;
        }

        public static bool operator !=(PointerEx px, PointerEx pxo)
        {
            return px.IntPtr != pxo.IntPtr;
        }

        public override int GetHashCode()
        {
            return this;
        }

        public override bool Equals(object o)
        {
            if (o is PointerEx px)
            {
                return px == this;
            }
            return false;
        }

        public static implicit operator bool(PointerEx px)
        {
            return (long)px != 0;
        }

        public static implicit operator byte(PointerEx px)
        {
            return (byte)px.IntPtr;
        }

        public static implicit operator sbyte(PointerEx px)
        {
            return (sbyte)px.IntPtr;
        }

        public static implicit operator int(PointerEx px)
        {
            return (int)px.IntPtr.ToInt64();
        }

        public static implicit operator uint(PointerEx px)
        {
            return (uint)px.IntPtr.ToInt64();
        }

        public static implicit operator long(PointerEx px)
        {
            return px.IntPtr.ToInt64();
        }

        public static implicit operator ulong(PointerEx px)
        {
            return (ulong)px.IntPtr.ToInt64();
        }

        public static implicit operator PointerEx(int i)
        {
            return new IntPtr(i);
        }

        public static implicit operator PointerEx(uint ui)
        {
            return new IntPtr((int)ui);
        }

        public static implicit operator PointerEx(long l)
        {
            return new IntPtr(l);
        }

        public static implicit operator PointerEx(ulong ul)
        {
            return new IntPtr((long)ul);
        }

        public static bool operator true(PointerEx p)
        {
            return p;
        }

        public static bool operator false(PointerEx p)
        {
            return !p;
        }

        public override string ToString()
        {
            return IntPtr.ToInt64().ToString($"X{IntPtr.Size * 2}");
        }

        public PointerEx Clone()
        {
            return new PointerEx(IntPtr);
        }
        #endregion
    }
    public static class PointerExtensions
    {
        public static IntPtr Add(this IntPtr i, IntPtr offset)
        {
            if (IntPtr.Size == sizeof(int)) return IntPtr.Add(i, offset.ToInt32());
            return new IntPtr(i.ToInt64() + offset.ToInt64());
        }

        public static PointerEx Add(this PointerEx i, PointerEx offset)
        {
            return i.IntPtr.Add(offset);
        }

        public static IntPtr Subtract(this IntPtr i, IntPtr offset)
        {
            if (IntPtr.Size == sizeof(int)) return IntPtr.Subtract(i, offset.ToInt32());
            return new IntPtr(i.ToInt64() - offset.ToInt64());
        }

        public static PointerEx Subtract(this PointerEx i, PointerEx offset)
        {
            return i.IntPtr.Subtract(offset);
        }

        public static PointerEx Align(this PointerEx value, uint alignment) => (value + (alignment - 1)) & ~(alignment - 1);

        public static PointerEx ToPointer(this byte[] data)
        {
            if (IntPtr.Size < data.Length)
            {
                //throw new InvalidCastException(DSTR(DSTR_PTR_CAST_FAIL, data.Length, IntPtr.Size));
                throw new InvalidCastException();
            }

            if (data.Length < IntPtr.Size)
            {
                byte[] _data = new byte[IntPtr.Size];
                data.CopyTo(_data, 0);
                data = _data;
            }

            if (IntPtr.Size == sizeof(long))
            {
                return BitConverter.ToInt64(data, 0);
            }

            return BitConverter.ToInt32(data, 0);
        }
    }
    #endregion
    #region Extensions
    public static class UtilExtensions
    {
        public unsafe static string String(this byte[] buffer, int index = default)
        {
            fixed (byte* bytes = &buffer[index])
            {
                return new string((sbyte*)bytes);
            }
        }
        public static byte[] Bytes(this string s)
        {
            return Encoding.ASCII.GetBytes(s).Append<byte>(0).ToArray();
        }
        public static byte[] ToByteArray<T>(this T s) where T : struct
        {
            PointerEx size = Marshal.SizeOf(s);
            byte[] data = new byte[size];
            PointerEx dwStruct = Marshal.AllocHGlobal((int)size);
            Marshal.StructureToPtr(s, dwStruct, true);
            Marshal.Copy(dwStruct, data, 0, size);
            Marshal.FreeHGlobal(dwStruct);
            return data;
        }
        public static byte[] ToByteArray<T>(this T[] a_s) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] data = new byte[a_s.Length * size];
            for (int i = 0; i < a_s.Length; i++)
            {
                a_s[i].ToByteArray().CopyTo(data, i * size);
            }
            return data;
        }
        public static T ToStruct<T>(this byte[] data) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            T val = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return val;
        }
        public static object ToStruct(this byte[] data, Type t)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            object val = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), t);
            handle.Free();
            return val;
        }
    }
    #endregion
}





