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
            load_button = new Button();
            listBox = new ListBox();
            comboBox1 = new ComboBox();
            browse_button = new Button();
            filepath_label = new Label();
            clear_button = new Button();
            statistic_label = new Label();
            SuspendLayout();
            // 
            // load_button
            // 
            load_button.Location = new Point(713, 415);
            load_button.Name = "load_button";
            load_button.Size = new Size(75, 23);
            load_button.TabIndex = 0;
            load_button.Text = "Load";
            load_button.UseVisualStyleBackColor = true;
            load_button.Click += load_button_Click;
            // 
            // listBox
            // 
            listBox.FormattingEnabled = true;
            listBox.Location = new Point(12, 12);
            listBox.Name = "listBox";
            listBox.Size = new Size(616, 379);
            listBox.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(634, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // browse_button
            // 
            browse_button.Location = new Point(12, 415);
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
            filepath_label.Location = new Point(93, 419);
            filepath_label.Name = "filepath_label";
            filepath_label.Size = new Size(92, 15);
            filepath_label.TabIndex = 4;
            filepath_label.Text = "File not selected";
            // 
            // clear_button
            // 
            clear_button.Location = new Point(632, 415);
            clear_button.Name = "clear_button";
            clear_button.Size = new Size(75, 23);
            clear_button.TabIndex = 5;
            clear_button.Text = "Clear";
            clear_button.UseVisualStyleBackColor = true;
            clear_button.Click += clear_button_Click;
            // 
            // statistic_label
            // 
            statistic_label.AutoSize = true;
            statistic_label.Location = new Point(632, 361);
            statistic_label.Name = "statistic_label";
            statistic_label.Size = new Size(65, 30);
            statistic_label.TabIndex = 6;
            statistic_label.Text = "Read time:\r\nCopy time:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(statistic_label);
            Controls.Add(clear_button);
            Controls.Add(filepath_label);
            Controls.Add(browse_button);
            Controls.Add(comboBox1);
            Controls.Add(listBox);
            Controls.Add(load_button);
            Name = "Form1";
            Text = "HPLATS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button load_button;
        private ListBox listBox;
        private ComboBox comboBox1;
        private Button browse_button;
        private Label filepath_label;
        private Button clear_button;
        private Label statistic_label;
    }
}
