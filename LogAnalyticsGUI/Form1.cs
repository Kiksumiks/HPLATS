using System.Drawing.Interop;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LogAnalyticsGUI
{
    public partial class Form1 : Form
    {
        private IntPtr sessionHandle = IntPtr.Zero;
        private LogFilterCriteria activeFilters = new LogFilterCriteria();
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
        }

        public async Task CheckProcessComplete()
        {
            while (!NativeInterop.IsProcessingComplete(sessionHandle))
            {
                await Task.Delay(100);
            }

            metaLabel.Text = "Metadata ready";
            filter_button.Enabled = true;

            RequestMetadataInfo();
            GetLogInfos();
        }
        private void RequestMetadataInfo()
        {
            NativeInterop.GetLogInfoCounts(sessionHandle, out infoCount, out debugCount, out warningCount, out errorCount, out fatalCount);
            softStat_label.Text = $"Infos: {infoCount}\r\nDebugs: {debugCount}\r\nWarnings: {warningCount}";
            hardStat_label.Text = $"Errors: {errorCount}\r\nFatals: {fatalCount}";
        }

        private void GetLogInfos()
        {
            listBox.Items.Clear();
            int arrayCount = 0;
            switch(activeFilters.WarningLevel)
            {
                case 0: arrayCount = totalLogCount; break;
                case 1: arrayCount = infoCount; break;
                case 2: arrayCount = debugCount; break;
                case 3: arrayCount = warningCount; break;
                case 4: arrayCount = errorCount; break;
                case 5: arrayCount = fatalCount; break;
                default: return;
            }
            LogEntryManaged[] entries = new LogEntryManaged[arrayCount];
            int logsCount = NativeInterop.GetRequestedEntries(sessionHandle, entries, arrayCount, activeFilters.WarningLevel, 0, 0, 0);

            UpdateDisplayList(entries, logsCount);
        }

        private async void UpdateDisplayList(LogEntryManaged[] entries, int count) 
        {
            for (int i = 0; i < count; i++)
            {
                string displayLine = $"{entries[i].level} [{entries[i].date} | {entries[i].time}] {entries[i].message}";
                listBox.Items.Add(displayLine);
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

            CheckProcessComplete();

            totalLogCount = NativeInterop.GetTotalCount(sessionHandle);
            count_label.Text = $"{totalLogCount} log entries parsed in {parseTime.ElapsedMilliseconds}ms";

            //statistic_label.Text = $"Entries: {logCount}\nRead time: {parseTime.ElapsedMilliseconds}ms\nRequest time: -";

            //Default all filters to 0 so we get every log.
            //var processTime = System.Diagnostics.Stopwatch.StartNew();

            //int logCount = NativeInterop.RequestLogEntries(sessionHandle, 0, 0, 0, 0);

            //LogEntryManaged[] entries = new LogEntryManaged[logCount];
            //int retrievedEntryCount = NativeInterop.GetRequestedEntries(sessionHandle, entries, logCount);
            //processTime.Stop();

            // UI update
            // UI update

            //for (int i = 0; i < retrievedEntryCount; i++)
            //{
            //string displayLine = $"{entries[i].level} [{entries[i].date} | {entries[i].time}] {entries[i].message}";
            //listBox.Items.Add(displayLine);
            //}

            //statistic_label.Text = $"Entries: {retrievedEntryCount}\nRead time: {parseTime.ElapsedMilliseconds}ms\nRequest time: {processTime.ElapsedMilliseconds}ms";
        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                InitialDirectory = "",
                Filter = "log files (*.txt)|*.txt|All files (*.*)|*.*",
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

                GetLogInfos();
            }
        }

    }
}
