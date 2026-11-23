namespace LogAnalyticsGUI
{
    partial class FilterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            threadId = new ComboBox();
            logLevel = new ComboBox();
            apply_button = new Button();
            cancel_button = new Button();
            dtpStart_label = new Label();
            dtpEnd_label = new Label();
            logLevel_label = new Label();
            threadId_label = new Label();
            SuspendLayout();
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(76, 12);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(200, 23);
            dtpStart.TabIndex = 0;
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(76, 41);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(200, 23);
            dtpEnd.TabIndex = 1;
            // 
            // threadId
            // 
            threadId.FormattingEnabled = true;
            threadId.Location = new Point(75, 99);
            threadId.Name = "threadId";
            threadId.Size = new Size(121, 23);
            threadId.TabIndex = 2;
            // 
            // logLevel
            // 
            logLevel.FormattingEnabled = true;
            logLevel.Location = new Point(75, 70);
            logLevel.Name = "logLevel";
            logLevel.Size = new Size(121, 23);
            logLevel.TabIndex = 3;
            // 
            // apply_button
            // 
            apply_button.Location = new Point(180, 142);
            apply_button.Name = "apply_button";
            apply_button.Size = new Size(75, 23);
            apply_button.TabIndex = 4;
            apply_button.Text = "Apply";
            apply_button.UseVisualStyleBackColor = true;
            apply_button.Click += apply_button_Click;
            // 
            // cancel_button
            // 
            cancel_button.Location = new Point(55, 142);
            cancel_button.Name = "cancel_button";
            cancel_button.Size = new Size(75, 23);
            cancel_button.TabIndex = 5;
            cancel_button.Text = "Cancel";
            cancel_button.UseVisualStyleBackColor = true;
            cancel_button.Click += cancel_button_Click;
            // 
            // dtpStart_label
            // 
            dtpStart_label.AutoSize = true;
            dtpStart_label.Location = new Point(12, 18);
            dtpStart_label.Name = "dtpStart_label";
            dtpStart_label.Size = new Size(58, 15);
            dtpStart_label.TabIndex = 6;
            dtpStart_label.Text = "Start time";
            // 
            // dtpEnd_label
            // 
            dtpEnd_label.AutoSize = true;
            dtpEnd_label.Location = new Point(16, 47);
            dtpEnd_label.Name = "dtpEnd_label";
            dtpEnd_label.Size = new Size(54, 15);
            dtpEnd_label.TabIndex = 7;
            dtpEnd_label.Text = "End time";
            // 
            // logLevel_label
            // 
            logLevel_label.AutoSize = true;
            logLevel_label.Location = new Point(16, 73);
            logLevel_label.Name = "logLevel_label";
            logLevel_label.Size = new Size(34, 15);
            logLevel_label.TabIndex = 8;
            logLevel_label.Text = "Level";
            // 
            // threadId_label
            // 
            threadId_label.AutoSize = true;
            threadId_label.Location = new Point(12, 102);
            threadId_label.Name = "threadId_label";
            threadId_label.Size = new Size(53, 15);
            threadId_label.TabIndex = 9;
            threadId_label.Text = "ThreadId";
            // 
            // FilterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(291, 173);
            Controls.Add(threadId_label);
            Controls.Add(logLevel_label);
            Controls.Add(dtpEnd_label);
            Controls.Add(dtpStart_label);
            Controls.Add(cancel_button);
            Controls.Add(apply_button);
            Controls.Add(logLevel);
            Controls.Add(threadId);
            Controls.Add(dtpEnd);
            Controls.Add(dtpStart);
            Name = "FilterForm";
            Text = "FilterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private ComboBox threadId;
        private ComboBox logLevel;
        private Button apply_button;
        private Button cancel_button;
        private Label dtpStart_label;
        private Label dtpEnd_label;
        private Label logLevel_label;
        private Label threadId_label;
    }
}