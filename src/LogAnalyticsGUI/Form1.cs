using System.Drawing.Interop;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace LogAnalyticsGUI
{
    public partial class Form1 : Form
    {
        private IntPtr sessionHandle = IntPtr.Zero;
        private LogFilter activeFilters = new LogFilter();
        private LogEntryManaged[] currentEntries = Array.Empty<LogEntryManaged>();
        private int totalLogCount = 0;
        private int infoCount = 0;
        private int debugCount = 0;
        private int warningCount = 0;
        private int errorCount = 0;
        private int fatalCount = 0;

        public Form1()
        {
            InitializeComponent();

            listView.View = View.Details;
            listView.VirtualMode = true;
            listView.FullRowSelect = true;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.Columns.Add("Time", 135);
            listView.Columns.Add("Level", 55);
            listView.Columns.Add("Thread", 65);
            listView.Columns.Add("Message", -2);
            listView.RetrieveVirtualItem += ListView_RetrieveVirtualItem;

            filter_button.Enabled = false;
        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                InitialDirectory = "",
                Filter = "Log files (*.log)|*.log|All files (*.*)|*.*",
                Multiselect = false,
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadLogFile(ofd.FileName);
            }
        }

        private void filter_button_Click(object sender, EventArgs e)
        {
            using FilterForm filterForm = new(activeFilters);
            if (filterForm.ShowDialog(this) == DialogResult.OK)
            {
                activeFilters = filterForm.Filter;

                _ = LoadAndDisplayLogs();
            }
        }

        private void LoadLogFile(string path)
        {
            if (sessionHandle != IntPtr.Zero)
            {
                NativeInterop.CloseLogSession(sessionHandle);
                sessionHandle = IntPtr.Zero;
            }

            var parseTime = System.Diagnostics.Stopwatch.StartNew();
            sessionHandle = NativeInterop.CreateLogSession(path);
            parseTime.Stop();
            if (sessionHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open file.", "File Error", MessageBoxButtons.OK);
                return;
            }

            NativeInterop.GetLogInfoCounts(sessionHandle, out infoCount, out debugCount, out warningCount, out errorCount, out fatalCount);
            softStat_label.Text = $"Infos: {infoCount}\r\nDebugs: {debugCount}\r\nWarnings: {warningCount}";
            hardStat_label.Text = $"Errors: {errorCount}\r\nFatals: {fatalCount}";

            _ = LoadAndDisplayLogs();

            totalLogCount = NativeInterop.GetTotalCount(sessionHandle);
            count_label.Text = $"{totalLogCount} log entries parsed in {parseTime.ElapsedMilliseconds}ms";

            filter_button.Enabled = true;
        }

        private async Task LoadAndDisplayLogs()
        {
            var result = await Task.Run(() =>
            {
                UInt64 minTime = activeFilters.StartTime.HasValue ? PackTimestamp(activeFilters.StartTime.Value) : 0;
                UInt64 maxTime = activeFilters.EndTime.HasValue ? PackTimestamp(activeFilters.EndTime.Value) : 0;

                // Count of records matching our filters
                int structCount = NativeInterop.RequestLogEntries(sessionHandle, activeFilters.WarningLevel, activeFilters.ThreadId, minTime, maxTime);
                LogEntryManaged[] entries = new LogEntryManaged[structCount];
                // Count of records writed
                int logsCount = NativeInterop.GetRequestedEntries(sessionHandle, entries, structCount);
                // structCount and logsCount should be always same?
                return (entries, logsCount);
            });

            currentEntries = result.entries;
            listView.VirtualListSize = result.logsCount;
            listView.Invalidate();
        }

        private void ListView_RetrieveVirtualItem(object? sender, RetrieveVirtualItemEventArgs e)
        {
            var entry = currentEntries[e.ItemIndex];
            string dateTime = $"[{entry.date} | {entry.time}]";
            ListViewItem item = new ListViewItem(dateTime);
            item.SubItems.Add(entry.level);
            item.SubItems.Add(entry.thread_id);
            item.SubItems.Add(entry.message);
            e.Item = item;
        }

        private static ulong PackTimestamp(DateTime dt)
        {
            uint rDate = ((uint)dt.Year << 16) | ((uint)dt.Month << 8) | (uint)dt.Day;
            uint rTime = ((uint)dt.Hour << 16) | ((uint)dt.Minute << 8) | (uint)dt.Second;

            return ((ulong)rDate << 32) | rTime;
        }

    }
}
