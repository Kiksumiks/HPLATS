using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LogAnalyticsGUI {
    public static class NativeInterop {

        private const string DllName = "LogEngineCore.dll";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr CreateLogSession(string filepath);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseLogSession(IntPtr sessionHandle);

        //[DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int RequestLogEntries(IntPtr sessionHandle, Byte levelFilter, Byte threadFilter, UInt64 minTimeFilter, UInt64 maxTimeFilter);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetRequestedEntries(IntPtr sessionHandle, [Out] LogEntryManaged[] outbuffer, int maxCount, Byte levelFilter, Byte threadFilter, UInt64 minTimeFilter, UInt64 maxTimeFilter);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTotalCount(IntPtr sessionHandle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsProcessingComplete(IntPtr sessionHandle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetLogInfoCounts(IntPtr sessionHandle, out int cInfo, out int cDebug, out int cWarning, out int cError, out int cFatal);
        
    }
}
