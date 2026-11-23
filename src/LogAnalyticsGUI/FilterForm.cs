using System.ComponentModel;

namespace LogAnalyticsGUI
{
    public partial class FilterForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LogFilterCriteria Criteria { get; set; }

        public FilterForm(LogFilterCriteria currentCriteria)
        {
            InitializeComponent();
     
            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;

            logLevel.Items.AddRange(new string[] { "All", "Debug", "Info", "Warning", "Error", "Fatal" });
            logLevel.SelectedIndex = currentCriteria.WarningLevel;
            logLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            threadId.SelectedIndex = -1;

            Criteria = currentCriteria ?? new LogFilterCriteria();
        }

/*
        private void FilterForm_Load(object sender, EventArgs e)
        {
            dtpStart.Value = Criteria.StartTime == DateTime.MinValue ? DateTime.Now.AddDays(-1) : Criteria.StartTime;
            dtpEnd.Value = Criteria.EndTime == DateTime.MaxValue ? DateTime.Now : Criteria.EndTime;
            logLevel.SelectedIndex = Criteria.WarningLevel;
            threadId.SelectedIndex = Criteria.ThreadId;
        }
*/

        private void apply_button_Click(object sender, EventArgs e)
        {
            Criteria.StartTime = dtpStart.Value;
            Criteria.EndTime = dtpEnd.Value;
            Criteria.WarningLevel = (logLevel.SelectedIndex < 0) ? (byte)0 : (byte)logLevel.SelectedIndex; // Why the fuck we have to cast 0 ??? Retarded language
            Criteria.ThreadId = (threadId.SelectedIndex < 0) ? (byte)0 : (byte)threadId.SelectedIndex; // Why the fuck we have to cast 0 ??? Retarded language
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
