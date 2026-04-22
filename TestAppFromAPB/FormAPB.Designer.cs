namespace TestAppFromAPB
{
    partial class FormAPB
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
            FilePreviwText = new RichTextBox();
            SaveFile = new Button();
            CopyBuffer = new Button();
            SelectFile = new Button();
            Filter = new ComboBox();
            label1 = new Label();
            History = new ComboBox();
            FilterText = new Label();
            label3 = new Label();
            label4 = new Label();
            CurrentPath = new TextBox();
            PlusFile = new Button();
            SuspendLayout();
            // 
            // FilePreviwText
            // 
            FilePreviwText.Location = new Point(394, 59);
            FilePreviwText.Name = "FilePreviwText";
            FilePreviwText.Size = new Size(383, 319);
            FilePreviwText.TabIndex = 0;
            FilePreviwText.Text = "";
            FilePreviwText.WordWrap = false;
            // 
            // SaveFile
            // 
            SaveFile.Location = new Point(431, 415);
            SaveFile.Name = "SaveFile";
            SaveFile.Size = new Size(75, 23);
            SaveFile.TabIndex = 1;
            SaveFile.Text = "Save File";
            SaveFile.UseVisualStyleBackColor = true;
            SaveFile.Click += SaveFile_Click;
            // 
            // CopyBuffer
            // 
            CopyBuffer.Location = new Point(622, 415);
            CopyBuffer.Name = "CopyBuffer";
            CopyBuffer.Size = new Size(120, 23);
            CopyBuffer.TabIndex = 2;
            CopyBuffer.Text = "Copy in Buffer";
            CopyBuffer.UseVisualStyleBackColor = true;
            CopyBuffer.Click += CopyBuffer_Click;
            // 
            // SelectFile
            // 
            SelectFile.Location = new Point(24, 296);
            SelectFile.Name = "SelectFile";
            SelectFile.Size = new Size(75, 23);
            SelectFile.TabIndex = 3;
            SelectFile.Text = "Select File";
            SelectFile.UseVisualStyleBackColor = true;
            SelectFile.Click += SelectFile_Click;
            // 
            // Filter
            // 
            Filter.DisplayMember = "(none)";
            Filter.FormattingEnabled = true;
            Filter.Items.AddRange(new object[] { "Name", "Age" });
            Filter.Location = new Point(24, 59);
            Filter.Name = "Filter";
            Filter.Size = new Size(205, 23);
            Filter.TabIndex = 5;
            Filter.ValueMember = "(none)";
            Filter.SelectedIndexChanged += Filter_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F);
            label1.Location = new Point(394, 9);
            label1.Name = "label1";
            label1.Size = new Size(154, 32);
            label1.TabIndex = 7;
            label1.Text = "File Structure";
            // 
            // History
            // 
            History.FormattingEnabled = true;
            History.Location = new Point(24, 137);
            History.Name = "History";
            History.Size = new Size(205, 23);
            History.TabIndex = 8;
            History.SelectedIndexChanged += History_SelectedIndexChanged;
            // 
            // FilterText
            // 
            FilterText.AutoSize = true;
            FilterText.Font = new Font("Segoe UI", 14F);
            FilterText.Location = new Point(24, 26);
            FilterText.Name = "FilterText";
            FilterText.Size = new Size(54, 25);
            FilterText.TabIndex = 9;
            FilterText.Text = "Filter";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F);
            label3.Location = new Point(24, 99);
            label3.Name = "label3";
            label3.Size = new Size(71, 25);
            label3.TabIndex = 10;
            label3.Text = "History";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(24, 184);
            label4.Name = "label4";
            label4.Size = new Size(49, 25);
            label4.TabIndex = 11;
            label4.Text = "Path";
            // 
            // CurrentPath
            // 
            CurrentPath.Location = new Point(24, 223);
            CurrentPath.Name = "CurrentPath";
            CurrentPath.Size = new Size(205, 23);
            CurrentPath.TabIndex = 12;
            // 
            // PlusFile
            // 
            PlusFile.Location = new Point(154, 296);
            PlusFile.Name = "PlusFile";
            PlusFile.Size = new Size(75, 23);
            PlusFile.TabIndex = 13;
            PlusFile.Text = "Plus File";
            PlusFile.UseVisualStyleBackColor = true;
            PlusFile.Click += PlusFile_Click;
            // 
            // FormAPB
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(PlusFile);
            Controls.Add(CurrentPath);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(FilterText);
            Controls.Add(History);
            Controls.Add(label1);
            Controls.Add(Filter);
            Controls.Add(SelectFile);
            Controls.Add(CopyBuffer);
            Controls.Add(SaveFile);
            Controls.Add(FilePreviwText);
            Name = "FormAPB";
            Text = "TestAppForAPB";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox FilePreviwText;
        private Button SaveFile;
        private Button CopyBuffer;
        private Button SelectFile;
        private ComboBox Filter;
        private Label label1;
        private ComboBox History;
        private Label FilterText;
        private Label label3;
        private Label label4;
        private TextBox CurrentPath;
        private Button PlusFile;
    }
}
