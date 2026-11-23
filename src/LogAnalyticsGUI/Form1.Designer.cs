namespace LogAnalyticsGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            browse_button = new Button();
            filepath_label = new Label();
            metaLabel = new Label();
            count_label = new Label();
            softStat_label = new Label();
            filter_button = new Button();
            hardStat_label = new Label();
            listView = new ListView();
            SuspendLayout();
            // 
            // browse_button
            // 
            browse_button.Location = new Point(12, 626);
            browse_button.Name = "browse_button";
            browse_button.Size = new Size(75, 23);
            browse_button.TabIndex = 3;
            browse_button.Text = "Browse";
            browse_button.UseVisualStyleBackColor = true;
            browse_button.Click += browse_button_Click;
            // 
            // filepath_label
            // 
            filepath_label.AutoSize = true;
            filepath_label.Location = new Point(93, 630);
            filepath_label.Name = "filepath_label";
            filepath_label.Size = new Size(92, 15);
            filepath_label.TabIndex = 4;
            filepath_label.Text = "File not selected";
            // 
            // metaLabel
            // 
            metaLabel.AutoSize = true;
            metaLabel.Location = new Point(426, 613);
            metaLabel.Name = "metaLabel";
            metaLabel.Size = new Size(110, 15);
            metaLabel.TabIndex = 8;
            metaLabel.Text = "Metadata not ready";
            // 
            // count_label
            // 
            count_label.AutoSize = true;
            count_label.Location = new Point(12, 608);
            count_label.Name = "count_label";
            count_label.Size = new Size(0, 15);
            count_label.TabIndex = 9;
            // 
            // softStat_label
            // 
            softStat_label.AutoSize = true;
            softStat_label.Location = new Point(698, 609);
            softStat_label.Name = "softStat_label";
            softStat_label.Size = new Size(63, 45);
            softStat_label.TabIndex = 6;
            softStat_label.Text = "Infos:\r\nDebugs:\r\nWarnings: ";
            // 
            // filter_button
            // 
            filter_button.Location = new Point(443, 631);
            filter_button.Name = "filter_button";
            filter_button.Size = new Size(75, 23);
            filter_button.TabIndex = 10;
            filter_button.Text = "Filters";
            filter_button.UseVisualStyleBackColor = true;
            filter_button.Click += filter_button_Click;
            // 
            // hardStat_label
            // 
            hardStat_label.AutoSize = true;
            hardStat_label.Location = new Point(798, 613);
            hardStat_label.Name = "hardStat_label";
            hardStat_label.Size = new Size(40, 30);
            hardStat_label.TabIndex = 11;
            hardStat_label.Text = "Errors:\r\nFatals:";
            // 
            // listView
            // 
            listView.HeaderStyle = ColumnHeaderStyle.None;
            listView.Location = new Point(12, 12);
            listView.Name = "listView";
            listView.Size = new Size(860, 592);
            listView.TabIndex = 12;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            listView.VirtualMode = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 661);
            Controls.Add(listView);
            Controls.Add(hardStat_label);
            Controls.Add(filter_button);
            Controls.Add(count_label);
            Controls.Add(metaLabel);
            Controls.Add(softStat_label);
            Controls.Add(filepath_label);
            Controls.Add(browse_button);
            Name = "Form1";
            Text = "HPLATS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button browse_button;
        private Label filepath_label;
        private Label metaLabel;
        private Label count_label;
        private Label softStat_label;
        private Button filter_button;
        private Label hardStat_label;
        private ListView listView;
    }
}
