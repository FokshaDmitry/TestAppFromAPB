using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TestAppFromAPB.Enums;
using TestAppFromAPB.ViewModels;

namespace TestAppFromAPB;

public class FormAPB : Form
{
	private FormAPBViewModel viewModel;

	private FilterMethod method;

	private IContainer components;

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

	public FormAPB()
	{
		InitializeComponent();
		viewModel = new FormAPBViewModel();
		base.Load += FormAPB_Load;
		base.DragEnter += FormAPB_DragEnter;
		base.DragDrop += FormAPB_DragDrop;
	}

	private async void FormAPB_DragDrop(object? sender, DragEventArgs e)
	{
		string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
		string path = array[0];
		if (Path.GetExtension(path).ToLower() == ".txt")
		{
			if (viewModel.fileModels.Count() == 0)
			{
				CurrentPath.Text = path;
				string text = await viewModel.ParceFile(path, method);
				FilePreviwText.Text = text;
				await viewModel.logger.SavePathAsync(path);
				History.Items.Add(path);
				PlusFile.Enabled = true;
				return;
			}
			TaskDialogButton taskDialogButton = new TaskDialogButton("Replace");
			TaskDialogButton taskDialogButton2 = new TaskDialogButton("Add for Exist");
			TaskDialogButton taskDialogButton3 = TaskDialog.ShowDialog(new TaskDialogPage
			{
				Caption = "File action",
				Heading = "What do you want?",
				Text = "Choose actions",
				Buttons = 
				{
					taskDialogButton,
					taskDialogButton2,
					TaskDialogButton.Cancel
				}
			});
			if (taskDialogButton3 == taskDialogButton)
			{
				CurrentPath.Text = path;
				string text2 = await viewModel.ParceFile(path, method);
				FilePreviwText.Text = text2;
				await viewModel.logger.SavePathAsync(path);
				History.Items.Add(path);
				PlusFile.Enabled = true;
			}
			else if (taskDialogButton3 == taskDialogButton2)
			{
				string resultAdd = await viewModel.ParceFile(path, method, addFile: true);
				CurrentPath.Text = path;
				await viewModel.logger.SavePathAsync(path);
				FilePreviwText.Text = resultAdd;
				PlusFile.Enabled = true;
			}
		}
		else
		{
			MessageBox.Show("You can use only .txt file!", "Format error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void FormAPB_DragEnter(object? sender, DragEventArgs e)
	{
		if (e.Data.GetDataPresent(DataFormats.FileDrop))
		{
			if (Path.GetExtension(((string[])e.Data.GetData(DataFormats.FileDrop))[0]).Equals(".txt", StringComparison.OrdinalIgnoreCase))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}
	}

	private async void FormAPB_Load(object? sender, EventArgs e)
	{
		Filter.SelectedIndex = 0;
		List<string> list = await viewModel.logger.GetPathesAsync(10);
		if (list != null && list.Count() != 0)
		{
			foreach (string item in list)
			{
				History.Items.Add(item);
			}
		}
		string text = Filter.SelectedItem?.ToString();
		if (!(text == "Name"))
		{
			if (text == "Age")
			{
				method = FilterMethod.Age;
			}
		}
		else
		{
			method = FilterMethod.Name;
		}
		PlusFile.Enabled = false;
	}

	private async void SelectFile_Click(object sender, EventArgs e)
	{
		string path = await viewModel.filePicker.GetFileAsync();
		CurrentPath.Text = path;
		string text = await viewModel.ParceFile(path, method);
		FilePreviwText.Text = text;
		await viewModel.logger.SavePathAsync(path);
		History.Items.Add(path);
		PlusFile.Enabled = true;
	}

	private async void History_SelectedIndexChanged(object sender, EventArgs e)
	{
		string text = History.SelectedItem?.ToString();
		if (!string.IsNullOrEmpty(text))
		{
			CurrentPath.Text = text;
			string text2 = await viewModel.ParceFile(text, method);
			FilePreviwText.Text = text2;
			PlusFile.Enabled = true;
		}
	}

	private async void Filter_SelectedIndexChanged(object sender, EventArgs e)
	{
		string text = Filter.SelectedItem?.ToString();
		if (!(text == "Name"))
		{
			if (text == "Age")
			{
				method = FilterMethod.Age;
			}
		}
		else
		{
			method = FilterMethod.Name;
		}
		string text2 = await viewModel.ChangeFilter(method);
		FilePreviwText.Text = text2;
	}

	private void CopyBuffer_Click(object sender, EventArgs e)
	{
		Clipboard.SetText(FilePreviwText.Text);
	}

	private async void SaveFile_Click(object sender, EventArgs e)
	{
		string text = FilePreviwText.Text;
		await viewModel.filePicker.SaveFileAsync(text);
		FilePreviwText.Text = "";
		PlusFile.Enabled = false;
	}

	private async void PlusFile_Click(object sender, EventArgs e)
	{
		string path = await viewModel.filePicker.GetFileAsync();
		string result = await viewModel.ParceFile(path, method, addFile: true);
		CurrentPath.Text = path;
		await viewModel.logger.SavePathAsync(path);
		FilePreviwText.Text = result;
		PlusFile.Enabled = true;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.FilePreviwText = new System.Windows.Forms.RichTextBox();
		this.SaveFile = new System.Windows.Forms.Button();
		this.CopyBuffer = new System.Windows.Forms.Button();
		this.SelectFile = new System.Windows.Forms.Button();
		this.Filter = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.History = new System.Windows.Forms.ComboBox();
		this.FilterText = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.CurrentPath = new System.Windows.Forms.TextBox();
		this.PlusFile = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.FilePreviwText.Location = new System.Drawing.Point(394, 59);
		this.FilePreviwText.Name = "FilePreviwText";
		this.FilePreviwText.Size = new System.Drawing.Size(383, 319);
		this.FilePreviwText.TabIndex = 0;
		this.FilePreviwText.Text = "";
		this.FilePreviwText.WordWrap = false;
		this.SaveFile.Location = new System.Drawing.Point(431, 415);
		this.SaveFile.Name = "SaveFile";
		this.SaveFile.Size = new System.Drawing.Size(75, 23);
		this.SaveFile.TabIndex = 1;
		this.SaveFile.Text = "Save File";
		this.SaveFile.UseVisualStyleBackColor = true;
		this.SaveFile.Click += new System.EventHandler(SaveFile_Click);
		this.CopyBuffer.Location = new System.Drawing.Point(622, 415);
		this.CopyBuffer.Name = "CopyBuffer";
		this.CopyBuffer.Size = new System.Drawing.Size(120, 23);
		this.CopyBuffer.TabIndex = 2;
		this.CopyBuffer.Text = "Copy in Buffer";
		this.CopyBuffer.UseVisualStyleBackColor = true;
		this.CopyBuffer.Click += new System.EventHandler(CopyBuffer_Click);
		this.SelectFile.Location = new System.Drawing.Point(24, 296);
		this.SelectFile.Name = "SelectFile";
		this.SelectFile.Size = new System.Drawing.Size(75, 23);
		this.SelectFile.TabIndex = 3;
		this.SelectFile.Text = "Select File";
		this.SelectFile.UseVisualStyleBackColor = true;
		this.SelectFile.Click += new System.EventHandler(SelectFile_Click);
		this.Filter.DisplayMember = "(none)";
		this.Filter.FormattingEnabled = true;
		this.Filter.Items.AddRange("Name", "Age");
		this.Filter.Location = new System.Drawing.Point(24, 59);
		this.Filter.Name = "Filter";
		this.Filter.Size = new System.Drawing.Size(205, 23);
		this.Filter.TabIndex = 5;
		this.Filter.ValueMember = "(none)";
		this.Filter.SelectedIndexChanged += new System.EventHandler(Filter_SelectedIndexChanged);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Segoe UI", 18f);
		this.label1.Location = new System.Drawing.Point(394, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(154, 32);
		this.label1.TabIndex = 7;
		this.label1.Text = "File Structure";
		this.History.FormattingEnabled = true;
		this.History.Location = new System.Drawing.Point(24, 137);
		this.History.Name = "History";
		this.History.Size = new System.Drawing.Size(205, 23);
		this.History.TabIndex = 8;
		this.History.SelectedIndexChanged += new System.EventHandler(History_SelectedIndexChanged);
		this.FilterText.AutoSize = true;
		this.FilterText.Font = new System.Drawing.Font("Segoe UI", 14f);
		this.FilterText.Location = new System.Drawing.Point(24, 26);
		this.FilterText.Name = "FilterText";
		this.FilterText.Size = new System.Drawing.Size(54, 25);
		this.FilterText.TabIndex = 9;
		this.FilterText.Text = "Filter";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Segoe UI", 14f);
		this.label3.Location = new System.Drawing.Point(24, 99);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(71, 25);
		this.label3.TabIndex = 10;
		this.label3.Text = "History";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Segoe UI", 14f);
		this.label4.Location = new System.Drawing.Point(24, 184);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(49, 25);
		this.label4.TabIndex = 11;
		this.label4.Text = "Path";
		this.CurrentPath.Location = new System.Drawing.Point(24, 223);
		this.CurrentPath.Name = "CurrentPath";
		this.CurrentPath.Size = new System.Drawing.Size(205, 23);
		this.CurrentPath.TabIndex = 12;
		this.PlusFile.Location = new System.Drawing.Point(154, 296);
		this.PlusFile.Name = "PlusFile";
		this.PlusFile.Size = new System.Drawing.Size(75, 23);
		this.PlusFile.TabIndex = 13;
		this.PlusFile.Text = "Plus File";
		this.PlusFile.UseVisualStyleBackColor = true;
		this.PlusFile.Click += new System.EventHandler(PlusFile_Click);
		this.AllowDrop = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 450);
		base.Controls.Add(this.PlusFile);
		base.Controls.Add(this.CurrentPath);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.FilterText);
		base.Controls.Add(this.History);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.Filter);
		base.Controls.Add(this.SelectFile);
		base.Controls.Add(this.CopyBuffer);
		base.Controls.Add(this.SaveFile);
		base.Controls.Add(this.FilePreviwText);
		base.Name = "FormAPB";
		this.Text = "TestAppForAPB";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
