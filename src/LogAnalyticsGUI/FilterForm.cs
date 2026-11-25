using System.ComponentModel;

namespace LogAnalyticsGUI
{
    public partial class FilterForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LogFilter Filter { get; set; }

        public FilterForm(LogFilter currentCriteria)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;

            Filter = currentCriteria ?? new LogFilter();

            logLevel.Items.AddRange(["All", "Debug", "Info", "Warning", "Error", "Fatal"]);
            logLevel.SelectedIndex = Filter.WarningLevel;
            logLevel.DropDownStyle = ComboBoxStyle.DropDownList;

            threadId.Items.AddRange(["All", "1", "2", "3", "4"]);
            threadId.SelectedIndex = Filter.ThreadId;
            threadId.DropDownStyle = ComboBoxStyle.DropDownList;

            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpStart.ShowCheckBox = true;

            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpEnd.ShowCheckBox = true;

            dtpStart.Checked = Filter.StartTime.HasValue;
            if (Filter.StartTime.HasValue) dtpStart.Value = Filter.StartTime.Value;

            dtpEnd.Checked = Filter.EndTime.HasValue;
            if (Filter.EndTime.HasValue) dtpEnd.Value = Filter.EndTime.Value;
      
        }

        private void apply_button_Click(object sender, EventArgs e)
        {
            Filter.StartTime = dtpStart.Checked ? dtpStart.Value : null;
            Filter.EndTime = dtpEnd.Checked ? dtpEnd.Value : null;
            Filter.WarningLevel = (byte)Math.Max(0, logLevel.SelectedIndex);
            Filter.ThreadId = (byte)Math.Max(0, threadId.SelectedIndex);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
