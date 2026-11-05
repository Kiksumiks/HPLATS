using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace LogAnalyticsGUI {
    public static class NativeInterop {

        private const string DllName = "LogEngineCore.dll";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr CreateLogSession(string filepath);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetSessionCount(IntPtr sessionHandle);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseLogSession(IntPtr sessionHandle);

        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        public static extern int GetLogEntries(IntPtr sessionHandle, [Out] LogEntryManaged[] outbuffer, int maxCount);
    }
}
