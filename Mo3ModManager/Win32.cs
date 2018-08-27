namespace Mo3ModManager
{
    static class Win32
    {

        #region struct

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct BY_HANDLE_FILE_INFORMATION
        {

            /// DWORD->unsigned int
            public uint dwFileAttributes;

            /// FILETIME->_FILETIME
            public FILETIME ftCreationTime;

            /// FILETIME->_FILETIME
            public FILETIME ftLastAccessTime;

            /// FILETIME->_FILETIME
            public FILETIME ftLastWriteTime;

            /// DWORD->unsigned int
            public uint dwVolumeSerialNumber;

            /// DWORD->unsigned int
            public uint nFileSizeHigh;

            /// DWORD->unsigned int
            public uint nFileSizeLow;

            /// DWORD->unsigned int
            public uint nNumberOfLinks;

            /// DWORD->unsigned int
            public uint nFileIndexHigh;

            /// DWORD->unsigned int
            public uint nFileIndexLow;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct FILETIME
        {

            /// DWORD->unsigned int
            public uint dwLowDateTime;

            /// DWORD->unsigned int
            public uint dwHighDateTime;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct IO_COUNTERS
        {

            /// ULONGLONG->unsigned __int64
            public ulong ReadOperationCount;

            /// ULONGLONG->unsigned __int64
            public ulong WriteOperationCount;

            /// ULONGLONG->unsigned __int64
            public ulong OtherOperationCount;

            /// ULONGLONG->unsigned __int64
            public ulong ReadTransferCount;

            /// ULONGLONG->unsigned __int64
            public ulong WriteTransferCount;

            /// ULONGLONG->unsigned __int64
            public ulong OtherTransferCount;
        }


        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct JOBOBJECT_BASIC_LIMIT_INFORMATION
        {

            /// LARGE_INTEGER->_LARGE_INTEGER
            public LARGE_INTEGER PerProcessUserTimeLimit;

            /// LARGE_INTEGER->_LARGE_INTEGER
            public LARGE_INTEGER PerJobUserTimeLimit;

            /// DWORD->unsigned int
            public uint LimitFlags;

            /// SIZE_T->ULONG_PTR->unsigned int
            public uint MinimumWorkingSetSize;

            /// SIZE_T->ULONG_PTR->unsigned int
            public uint MaximumWorkingSetSize;

            /// DWORD->unsigned int
            public uint ActiveProcessLimit;

            /// ULONG_PTR->unsigned int
            public uint Affinity;

            /// DWORD->unsigned int
            public uint PriorityClass;

            /// DWORD->unsigned int
            public uint SchedulingClass;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit)]
        public struct LARGE_INTEGER
        {

            /// Anonymous_9320654f_2227_43bf_a385_74cc8c562686
            [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
            public Anonymous_9320654f_2227_43bf_a385_74cc8c562686 Struct1;

            /// Anonymous_947eb392_1446_4e25_bbd4_10e98165f3a9
            [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
            public Anonymous_947eb392_1446_4e25_bbd4_10e98165f3a9 u;

            /// LONGLONG->__int64
            [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
            public long QuadPart;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct Anonymous_9320654f_2227_43bf_a385_74cc8c562686
        {

            /// DWORD->unsigned int
            public uint LowPart;

            /// LONG->int
            public int HighPart;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct Anonymous_947eb392_1446_4e25_bbd4_10e98165f3a9
        {

            /// DWORD->unsigned int
            public uint LowPart;

            /// LONG->int
            public int HighPart;
        }


        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {

            /// DWORD->unsigned int
            public uint nLength;

            /// LPVOID->void*
            public System.IntPtr lpSecurityDescriptor;

            /// BOOL->int
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public bool bInheritHandle;
        }


        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
        {

            /// JOBOBJECT_BASIC_LIMIT_INFORMATION->_JOBOBJECT_BASIC_LIMIT_INFORMATION
            public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;

            /// IO_COUNTERS->_IO_COUNTERS
            public IO_COUNTERS IoInfo;

            /// SIZE_T->ULONG_PTR->unsigned int
            public uint ProcessMemoryLimit;

            /// SIZE_T->ULONG_PTR->unsigned int
            public uint JobMemoryLimit;

            /// SIZE_T->ULONG_PTR->unsigned int
            public uint PeakProcessMemoryUsed;

            /// SIZE_T->ULONG_PTR->unsigned int
            public uint PeakJobMemoryUsed;
        }


        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct JOBOBJECT_ASSOCIATE_COMPLETION_PORT
        {

            /// PVOID->void*
            public System.IntPtr CompletionKey;

            /// HANDLE->void*
            public System.IntPtr CompletionPort;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {

            /// HANDLE->void*
            public System.IntPtr hProcess;

            /// HANDLE->void*
            public System.IntPtr hThread;

            /// DWORD->unsigned int
            public uint dwProcessId;

            /// DWORD->unsigned int
            public uint dwThreadId;
        }



        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct STARTUPINFOW
        {

            /// DWORD->unsigned int
            public uint cb;

            /// LPWSTR->WCHAR*
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public string lpReserved;

            /// LPWSTR->WCHAR*
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public string lpDesktop;

            /// LPWSTR->WCHAR*
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public string lpTitle;

            /// DWORD->unsigned int
            public uint dwX;

            /// DWORD->unsigned int
            public uint dwY;

            /// DWORD->unsigned int
            public uint dwXSize;

            /// DWORD->unsigned int
            public uint dwYSize;

            /// DWORD->unsigned int
            public uint dwXCountChars;

            /// DWORD->unsigned int
            public uint dwYCountChars;

            /// DWORD->unsigned int
            public uint dwFillAttribute;

            /// DWORD->unsigned int
            public uint dwFlags;

            /// WORD->unsigned short
            public ushort wShowWindow;

            /// WORD->unsigned short
            public ushort cbReserved2;

            /// LPBYTE->BYTE*
            public System.IntPtr lpReserved2;

            /// HANDLE->void*
            public System.IntPtr hStdInput;

            /// HANDLE->void*
            public System.IntPtr hStdOutput;

            /// HANDLE->void*
            public System.IntPtr hStdError;

        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct OVERLAPPED
        {

            /// ULONG_PTR->unsigned int
            public uint Internal;

            /// ULONG_PTR->unsigned int
            public uint InternalHigh;

            /// Anonymous_7416d31a_1ce9_4e50_b1e1_0f2ad25c0196
            public Anonymous_7416d31a_1ce9_4e50_b1e1_0f2ad25c0196 Union1;

            /// HANDLE->void*
            public System.IntPtr hEvent;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit)]
        public struct Anonymous_7416d31a_1ce9_4e50_b1e1_0f2ad25c0196
        {

            /// Anonymous_ac6e4301_4438_458f_96dd_e86faeeca2a6
            [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
            public Anonymous_ac6e4301_4438_458f_96dd_e86faeeca2a6 Struct1;

            /// PVOID->void*
            [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
            public System.IntPtr Pointer;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct Anonymous_ac6e4301_4438_458f_96dd_e86faeeca2a6
        {

            /// DWORD->unsigned int
            public uint Offset;

            /// DWORD->unsigned int
            public uint OffsetHigh;
        }

        #endregion
        public partial class NativeConstants
        {
            /// INFINITE -> 0xFFFFFFFF
            public const uint INFINITE = uint.MaxValue;

            public static readonly System.IntPtr INVALID_HANDLE_VALUE = new System.IntPtr(-1);

            /// CREATE_SUSPENDED -> 0x00000004
            public const uint CREATE_SUSPENDED = 4;


            /// JOB_OBJECT_MSG_ACTIVE_PROCESS_ZERO -> 4
            public const uint JOB_OBJECT_MSG_ACTIVE_PROCESS_ZERO = 4;

            /// FILE_SHARE_READ -> 0x00000001
            public const uint FILE_SHARE_READ = 1;

            /// FILE_SHARE_WRITE -> 0x00000002
            public const uint FILE_SHARE_WRITE = 2;

            /// FILE_ATTRIBUTE_NORMAL -> 0x00000080
            public const uint FILE_ATTRIBUTE_NORMAL = 128;

            /// OPEN_EXISTING -> 3
            public const uint OPEN_EXISTING = 3;

            /// GENERIC_READ -> (0x80000000L)
            public const uint GENERIC_READ = 0x80000000;

            /// STARTF_FORCEONFEEDBACK -> 0x00000040
            public const uint STARTF_FORCEONFEEDBACK = 64;

            /// CREATE_BREAKAWAY_FROM_JOB -> 0x01000000
            public const uint CREATE_BREAKAWAY_FROM_JOB = 16777216;
        }

        public enum JOBOBJECTINFOCLASS
        {
            JobObjectAssociateCompletionPortInformation = 7,
            JobObjectBasicLimitInformation = 2,
            JobObjectBasicUIRestrictions = 4,
            JobObjectCpuRateControlInformation = 15,
            JobObjectEndOfJobTimeInformation = 6,
            JobObjectExtendedLimitInformation = 9,
            JobObjectGroupInformation = 11,
            JobObjectGroupInformationEx = 14,
            JobObjectLimitViolationInformation2 = 35,
            JobObjectNetRateControlInformation = 32,
            JobObjectNotificationLimitInformation = 12,
            JobObjectNotificationLimitInformation2 = 34,
            JobObjectSecurityLimitInformation = 5
        }



        #region methods

        public partial class NativeMethods
        {

            /// Return Type: BOOL->int
            ///hFile: HANDLE->void*
            ///lpFileInformation: LPBY_HANDLE_FILE_INFORMATION->_BY_HANDLE_FILE_INFORMATION*
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "GetFileInformationByHandle")]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool GetFileInformationByHandle([System.Runtime.InteropServices.InAttribute()] System.IntPtr hFile, [System.Runtime.InteropServices.OutAttribute()] out BY_HANDLE_FILE_INFORMATION lpFileInformation);

        }

        public partial class NativeMethods
        {

            /// Return Type: BOOL->int
            ///lpFileName: LPCWSTR->WCHAR*
            ///lpExistingFileName: LPCWSTR->WCHAR*
            ///lpSecurityAttributes: LPSECURITY_ATTRIBUTES->_SECURITY_ATTRIBUTES*
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "CreateHardLinkW", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool CreateHardLinkW([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpFileName, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpExistingFileName, System.IntPtr lpSecurityAttributes);

        }



        public partial class NativeMethods
        {

            /// Return Type: HANDLE->void*
            ///lpJobAttributes: LPSECURITY_ATTRIBUTES->_SECURITY_ATTRIBUTES*
            ///lpName: LPCWSTR->WCHAR*
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "CreateJobObjectW", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            public static extern System.IntPtr CreateJobObjectW([System.Runtime.InteropServices.InAttribute()] System.IntPtr lpJobAttributes, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpName);

        }

        public partial class NativeMethods
        {

            /// Return Type: HANDLE->void*
            ///lpFileName: LPCWSTR->WCHAR*
            ///dwDesiredAccess: DWORD->unsigned int
            ///dwShareMode: DWORD->unsigned int
            ///lpSecurityAttributes: LPSECURITY_ATTRIBUTES->_SECURITY_ATTRIBUTES*
            ///dwCreationDisposition: DWORD->unsigned int
            ///dwFlagsAndAttributes: DWORD->unsigned int
            ///hTemplateFile: HANDLE->void*
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "CreateFileW")]
            public static extern System.IntPtr CreateFileW([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpFileName, uint dwDesiredAccess, uint dwShareMode, [System.Runtime.InteropServices.InAttribute()] System.IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, [System.Runtime.InteropServices.InAttribute()] System.IntPtr hTemplateFile);

        }


        public partial class NativeMethods
        {

            /// Return Type: BOOL->int
            ///hJob: HANDLE->void*
            ///JobObjectInformationClass: JOBOBJECTINFOCLASS->_JOBOBJECTINFOCLASS
            ///lpJobObjectInformation: LPVOID->void*
            ///cbJobObjectInformationLength: DWORD->unsigned int
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "SetInformationJobObject", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool SetInformationJobObject([System.Runtime.InteropServices.InAttribute()] System.IntPtr hJob, JOBOBJECTINFOCLASS JobObjectInformationClass, [System.Runtime.InteropServices.InAttribute()] System.IntPtr lpJobObjectInformation, uint cbJobObjectInformationLength);

        }
        public partial class NativeMethods
        {

            /// Return Type: BOOL->int
            ///hJob: HANDLE->void*
            ///hProcess: HANDLE->void*
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "AssignProcessToJobObject", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool AssignProcessToJobObject([System.Runtime.InteropServices.InAttribute()] System.IntPtr hJob, [System.Runtime.InteropServices.InAttribute()] System.IntPtr hProcess);

        }

        public partial class NativeMethods
        {

            /// Return Type: BOOL->int
            ///hObject: HANDLE->void*
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "CloseHandle", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool CloseHandle([System.Runtime.InteropServices.InAttribute()] System.IntPtr hObject);

        }

        public partial class NativeMethods
        {

            /// Return Type: HANDLE->void*
            ///FileHandle: HANDLE->void*
            ///ExistingCompletionPort: HANDLE->void*
            ///CompletionKey: ULONG_PTR->unsigned int
            ///NumberOfConcurrentThreads: DWORD->unsigned int
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "CreateIoCompletionPort", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            public static extern System.IntPtr CreateIoCompletionPort([System.Runtime.InteropServices.InAttribute()] System.IntPtr FileHandle, [System.Runtime.InteropServices.InAttribute()] System.IntPtr ExistingCompletionPort, uint CompletionKey, uint NumberOfConcurrentThreads);

        }



        public partial class NativeMethods
        {

            /// Return Type: BOOL->int
            ///lpApplicationName: LPCWSTR->WCHAR*
            ///lpCommandLine: LPWSTR->WCHAR*
            ///lpProcessAttributes: LPSECURITY_ATTRIBUTES->_SECURITY_ATTRIBUTES*
            ///lpThreadAttributes: LPSECURITY_ATTRIBUTES->_SECURITY_ATTRIBUTES*
            ///bInheritHandles: BOOL->int
            ///dwCreationFlags: DWORD->unsigned int
            ///lpEnvironment: LPVOID->void*
            ///lpCurrentDirectory: LPCWSTR->WCHAR*
            ///lpStartupInfo: LPSTARTUPINFOW->_STARTUPINFOW*
            ///lpProcessInformation: LPPROCESS_INFORMATION->_PROCESS_INFORMATION*
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "CreateProcessW", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool CreateProcessW([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpApplicationName, System.Text.StringBuilder lpCommandLine, [System.Runtime.InteropServices.InAttribute()] System.IntPtr lpProcessAttributes, [System.Runtime.InteropServices.InAttribute()] System.IntPtr lpThreadAttributes, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool bInheritHandles, uint dwCreationFlags, System.IntPtr lpEnvironment, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpCurrentDirectory, [System.Runtime.InteropServices.InAttribute()] ref STARTUPINFOW lpStartupInfo, [System.Runtime.InteropServices.OutAttribute()] out PROCESS_INFORMATION lpProcessInformation);

            /*
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "CreateProcessW", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool CreateProcessW([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpApplicationName, System.Text.StringBuilder lpCommandLine, [System.Runtime.InteropServices.InAttribute()] System.IntPtr lpProcessAttributes, [System.Runtime.InteropServices.InAttribute()] System.IntPtr lpThreadAttributes, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool bInheritHandles, uint dwCreationFlags, System.Text.StringBuilder lpEnvironment, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpCurrentDirectory, [System.Runtime.InteropServices.InAttribute()] ref STARTUPINFOW lpStartupInfo, [System.Runtime.InteropServices.OutAttribute()] out PROCESS_INFORMATION lpProcessInformation);
            */

            //CharSet = CharSet.Unicode MUST BE WRITTEN. I wasted three hours on it.

            //lpEnvironment should set to IntPtr.Zero IF YOU DON'T WANT TO OVERRIVE IT. Set it to new StringBuilder() will CLEAR IT. I wasted another hours on it.
        }


        public partial class NativeMethods
        {

            /// Return Type: DWORD->unsigned int
            ///hThread: HANDLE->void*
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "ResumeThread", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            public static extern uint ResumeThread([System.Runtime.InteropServices.InAttribute()] System.IntPtr hThread);

        }

        public partial class NativeMethods
        {

            /// Return Type: BOOL->int
            ///CompletionPort: HANDLE->void*
            ///lpNumberOfBytesTransferred: LPDWORD->DWORD*
            ///lpCompletionKey: PULONG_PTR->unsigned int*
            ///lpOverlapped: LPOVERLAPPED*
            ///dwMilliseconds: DWORD->unsigned int
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "GetQueuedCompletionStatus", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool GetQueuedCompletionStatus([System.Runtime.InteropServices.InAttribute()] System.IntPtr CompletionPort, [System.Runtime.InteropServices.OutAttribute()] out uint lpNumberOfBytesTransferred, [System.Runtime.InteropServices.OutAttribute()] out System.IntPtr lpCompletionKey, [System.Runtime.InteropServices.OutAttribute()] out System.IntPtr lpOverlapped, [System.Runtime.InteropServices.InAttribute()] uint dwMilliseconds);

        }


        public partial class NativeMethods
        {

            /// Return Type: DWORD->unsigned int
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "GetLastError", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            public static extern uint GetLastError();

        }

        #endregion

    }
}
