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
        private LogFilterCriteria activeFilters = new LogFilterCriteria();
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
            filter_button.Enabled = false;

            listView.View = View.Details;
            listView.VirtualMode = true;
            listView.FullRowSelect = true;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.Columns.Add("Time", 135);
            listView.Columns.Add("Level", 55);
            listView.Columns.Add("Thread", 65);
            listView.Columns.Add("Message", -2);
            listView.RetrieveVirtualItem += ListView_RetrieveVirtualItem;
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
            if (filterForm.ShowDialog() == DialogResult.OK)
            {
                activeFilters = filterForm.Criteria;

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

            _ = CheckProcessComplete();

            totalLogCount = NativeInterop.GetTotalCount(sessionHandle);
            count_label.Text = $"{totalLogCount} log entries parsed in {parseTime.ElapsedMilliseconds}ms";
        }
        public async Task CheckProcessComplete()
        {
            await Task.Run(async () =>
            {
                while (!NativeInterop.IsProcessingComplete(sessionHandle))
                {
                    await Task.Delay(100);
                }
            });

            metaLabel.Text = "Metadata ready";
            filter_button.Enabled = true;

            NativeInterop.GetLogInfoCounts(sessionHandle, out infoCount, out debugCount, out warningCount, out errorCount, out fatalCount);
            softStat_label.Text = $"Infos: {infoCount}\r\nDebugs: {debugCount}\r\nWarnings: {warningCount}";
            hardStat_label.Text = $"Errors: {errorCount}\r\nFatals: {fatalCount}";


            await LoadAndDisplayLogs();
        }

        private async Task LoadAndDisplayLogs()
        {
            var result = await Task.Run(() =>
            {
                // Count of records matching our filters
                int structCount = NativeInterop.RequestLogEntries(sessionHandle, activeFilters.WarningLevel, activeFilters.ThreadId, 0, 0);
                LogEntryManaged[] entries = new LogEntryManaged[structCount];
                // Count of records writed
                int logsCount = NativeInterop.GetRequestedEntries(sessionHandle, entries, structCount);
                // structCount and logsCount should be always same. There is nothing changing when we are catching entries.
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


    }
}
