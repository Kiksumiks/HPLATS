namespace LogAnalyticsGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            string filePath = filepath_label.Text;

            var parseTime = System.Diagnostics.Stopwatch.StartNew();
            IntPtr handle = NativeInterop.CreateLogSession(filePath);
            parseTime.Stop();
            if (handle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to open file.\nCheck file path!", "File Error", MessageBoxButtons.OK);
                return;
            }

            //var parseElapsed = parseTime.Elapsed;

            int logCount = NativeInterop.GetSessionCount(handle);

            var copyTime = System.Diagnostics.Stopwatch.StartNew();
            LogEntryManaged[] entries = new LogEntryManaged[logCount];
            int retrievedEntryCount = NativeInterop.GetLogEntries(handle, entries, logCount);
            copyTime.Stop();

            for (int i = 0; i < retrievedEntryCount; i++)
            {
                string displayLine = $"{entries[i].level} [{entries[i].date} | {entries[i].time}] {entries[i].message}";
                listBox.Items.Add(displayLine);
            }
            
            //var copyElapsed = copyTime.Elapsed;

            NativeInterop.CloseLogSession(handle);

            statistic_label.Text = $"Read time: {parseTime.ElapsedMilliseconds}ms\nCopy time: {copyTime.ElapsedMilliseconds}ms";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                filepath_label.Text = ofd.FileName;
            }
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
        }
    }
}
