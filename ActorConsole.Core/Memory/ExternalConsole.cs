using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;


// from airyz, i made some changes
// TODO: maybe instead of using externalconsole, use External.Call()

public static class ExternalConsole
{
    [Flags]
    private enum ProcessAccessFlags : uint
    {
        All = 0x1F0FFFu,
        Terminate = 1u,
        CreateThread = 2u,
        VirtualMemoryOperation = 8u,
        VirtualMemoryRead = 0x10u,
        VirtualMemoryWrite = 0x20u,
        DuplicateHandle = 0x40u,
        CreateProcess = 0x80u,
        SetQuota = 0x100u,
        SetInformation = 0x200u,
        QueryInformation = 0x400u,
        QueryLimitedInformation = 0x1000u,
        Synchronize = 0x100000u
    }

    [Flags]
    private enum FreeType
    {
        Decommit = 0x4000,
        Release = 0x8000
    }

    [Flags]
    private enum AllocationType
    {
        Commit = 0x1000,
        Reserve = 0x2000,
        Decommit = 0x4000,
        Release = 0x8000,
        Reset = 0x80000,
        Physical = 0x400000,
        TopDown = 0x100000,
        WriteWatch = 0x200000,
        LargePages = 0x20000000
    }

    [Flags]
    private enum MemoryProtection
    {
        Execute = 0x10,
        ExecuteRead = 0x20,
        ExecuteReadWrite = 0x40,
        ExecuteWriteCopy = 0x80,
        NoAccess = 1,
        ReadOnly = 2,
        ReadWrite = 4,
        WriteCopy = 8,
        GuardModifierflag = 0x100,
        NoCacheModifierflag = 0x200,
        WriteCombineModifierflag = 0x400
    }


    private static IntPtr hProcess = IntPtr.Zero;


    private static IntPtr cbuf_addtext_alloc = IntPtr.Zero;


    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
    private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
    private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, FreeType dwFreeType);

    [DllImport("kernel32.dll")]
    private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);

    internal static void Send(string command)
    {
        uint cbuf_address = 4213536u;
        hProcess = ActorConsole.Core.Memory.IW4.mem.Handle;

        byte[] callbytes = BitConverter.GetBytes(cbuf_address);
        if (command == "")
        {
            return;
        }
        byte[] cbuf_addtext_wrapper = new byte[35]
        {
                85, 139, 236, 131, 236, 8, 199, 69, 248, 0,
                0, 0, 0, 199, 69, 252, 0, 0, 0, 0,
                255, 117, 248, 106, 0, 255, 85, 252, 131, 196,
                8, 139, 229, 93, 195
        };
        if (cbuf_addtext_alloc == IntPtr.Zero)
        {
            cbuf_addtext_alloc = VirtualAllocEx(hProcess, IntPtr.Zero, (IntPtr)cbuf_addtext_wrapper.Length, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ExecuteReadWrite);
            byte[] commandbytes = Encoding.ASCII.GetBytes(command);
            IntPtr commandaddress = VirtualAllocEx(hProcess, IntPtr.Zero, (IntPtr)commandbytes.Length, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ExecuteReadWrite);
            int lpNumberOfBytesWritten = 0;
            int lpNumberOfBytesWritten2 = commandbytes.Length;
            WriteProcessMemory(hProcess, commandaddress, commandbytes, commandbytes.Length, out lpNumberOfBytesWritten2);
            Array.Copy(BitConverter.GetBytes(commandaddress.ToInt64()), 0, cbuf_addtext_wrapper, 9, 4);
            Array.Copy(callbytes, 0, cbuf_addtext_wrapper, 16, 4);
            WriteProcessMemory(hProcess, cbuf_addtext_alloc, cbuf_addtext_wrapper, cbuf_addtext_wrapper.Length, out lpNumberOfBytesWritten);
            CreateRemoteThread(hProcess, IntPtr.Zero, 0u, cbuf_addtext_alloc, IntPtr.Zero, 0u, out var _);
            if (cbuf_addtext_alloc != IntPtr.Zero && commandaddress != IntPtr.Zero)
            {
                VirtualFreeEx(hProcess, cbuf_addtext_alloc, cbuf_addtext_wrapper.Length, FreeType.Release);
                VirtualFreeEx(hProcess, commandaddress, cbuf_addtext_wrapper.Length, FreeType.Release);
            }
        }
        cbuf_addtext_alloc = IntPtr.Zero;
    }


}

