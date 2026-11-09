using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;

//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

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
            logLevel.SelectedIndex = 0;
            logLevel.DropDownStyle = ComboBoxStyle.DropDownList;

            Criteria = currentCriteria ?? new LogFilterCriteria();


        }

        private void FilterForm_Load(object sender, EventArgs e)
        {
            dtpStart.Value = Criteria.StartTime == DateTime.MinValue ? DateTime.Now.AddDays(-1) : Criteria.StartTime;
            dtpEnd.Value = Criteria.EndTime == DateTime.MaxValue ? DateTime.Now : Criteria.EndTime;
            logLevel.SelectedIndex = Criteria.WarningLevel;
            threadId.SelectedIndex = Criteria.ThreadId;
        }

        private void apply_button_Click(object sender, EventArgs e)
        {
            Criteria.StartTime = dtpStart.Value;
            Criteria.EndTime = dtpEnd.Value;
            Criteria.WarningLevel = (Byte)logLevel.SelectedIndex;
            Criteria.ThreadId = (Byte)threadId.SelectedIndex;
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
