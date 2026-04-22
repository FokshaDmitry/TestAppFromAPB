using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestAppFromAPB.Enums;
using TestAppFromAPB.ViewModels;

namespace TestAppFromAPB
{
    public partial class FormAPB : Form
    {
        FormAPBViewModel viewModel;
        FilterMethod method;
        public FormAPB()
        {
            InitializeComponent();
            viewModel = new FormAPBViewModel();
            this.Load += FormAPB_Load;
            this.DragEnter += FormAPB_DragEnter;
            this.DragDrop += FormAPB_DragDrop;
            // wire designer button (button1) as PlusFile
            button1.Click += PlusFile_Click;
        }

        private async void FormAPB_Load(object? sender, EventArgs e)
        {
            Filter.SelectedIndex = 0;
            var historyList = await viewModel.logger.GetPathesAsync(10);
            if (historyList != null && historyList.Count() != 0)
            {
                foreach (var item in historyList)
                {
                    History.Items.Add(item);
                }
            }
            var filter = Filter.SelectedItem?.ToString();
            switch (filter)
            {
                case "Name":
                    method = FilterMethod.Name;
                    break;
                case "Age":
                    method = FilterMethod.Age;
                    break;
            }
            // disable plus file until a file is loaded
            button1.Enabled = false;
        }

        private async void SelectFile_Click(object sender, EventArgs e)
        {
            var path = await viewModel.filePicker.GetFileAsync();
            CurrentPath.Text = path;
            var result = await viewModel.ParceFile(path, method);
            FilePreviwText.Text = result;
            await viewModel.logger.SavePathAsync(path);
            History.Items.Add(path);
            button1.Enabled = true;
        }

        private async void History_SelectedIndexChanged(object sender, EventArgs e)
        {
            var path = History.SelectedItem?.ToString();
            if (!String.IsNullOrEmpty(path))
            {
                CurrentPath.Text = path;
                var result = await viewModel.ParceFile(path, method);
                FilePreviwText.Text = result;
                button1.Enabled = true;
            }
        }

        private async void Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filter = Filter.SelectedItem?.ToString();
            switch (filter)
            {
                case "Name":
                    method = FilterMethod.Name;
                    break;
                case "Age":
                    method = FilterMethod.Age;
                    break;
            }
            var path = CurrentPath.Text;
            if (!String.IsNullOrEmpty(path))
            {
                var result = await viewModel.ParceFile(path, method);
                FilePreviwText.Text = result;
            }
            else
            {
                // if no path, just apply filter on existing models
                var text = await viewModel.ChangeFilter(method);
                FilePreviwText.Text = text;
            }
        }

        private void CopyBuffer_Click(object sender, EventArgs e)
        {
            var fileText = FilePreviwText.Text;
            Clipboard.SetText(fileText);
        }

        private async void SaveFile_Click(object sender, EventArgs e)
        {
            var fileText = FilePreviwText.Text;
            await viewModel.filePicker.SaveFileAsync(fileText);
            FilePreviwText.Text = string.Empty;
            button1.Enabled = false;
        }

        private async void PlusFile_Click(object? sender, EventArgs e)
        {
            var path = await viewModel.filePicker.GetFileAsync();
            var result = await viewModel.ParceFile(path, method, addFile: true);
            CurrentPath.Text = path;
            await viewModel.logger.SavePathAsync(path);
            FilePreviwText.Text = result;
            button1.Enabled = true;
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

        private async void FormAPB_DragDrop(object? sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var path = files[0];
            if (Path.GetExtension(path).ToLower() == ".txt")
            {
                if (viewModel.fileModels.Count() == 0)
                {
                    CurrentPath.Text = path;
                    var text = await viewModel.ParceFile(path, method);
                    FilePreviwText.Text = text;
                    await viewModel.logger.SavePathAsync(path);
                    History.Items.Add(path);
                    button1.Enabled = true;
                    return;
                }
                var replace = new TaskDialogButton("Replace");
                var addForExist = new TaskDialogButton("Add for Exist");
                var res = TaskDialog.ShowDialog(new TaskDialogPage
                {
                    Caption = "File action",
                    Heading = "What do you want?",
                    Text = "Choose actions",
                    Buttons = { replace, addForExist, TaskDialogButton.Cancel }
                });
                if (res == replace)
                {
                    CurrentPath.Text = path;
                    var text2 = await viewModel.ParceFile(path, method);
                    FilePreviwText.Text = text2;
                    await viewModel.logger.SavePathAsync(path);
                    History.Items.Add(path);
                    button1.Enabled = true;
                }
                else if (res == addForExist)
                {
                    var added = await viewModel.ParceFile(path, method, addFile: true);
                    CurrentPath.Text = path;
                    await viewModel.logger.SavePathAsync(path);
                    FilePreviwText.Text = added;
                    button1.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("You can use only .txt file!", "Format error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
