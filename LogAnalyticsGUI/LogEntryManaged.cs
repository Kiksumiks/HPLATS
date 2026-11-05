using System.Runtime.InteropServices;

namespace LogAnalyticsGUI
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct LogEntryManaged
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string date;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string time;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string level;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string thread_id;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string message;
    }
}