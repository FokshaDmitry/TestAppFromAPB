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
        }

        private async void SelectFile_Click(object sender, EventArgs e)
        {
            var path = await viewModel.filePicker.GetFileAsync();
            CurrentPath.Text = path;
            var result = await viewModel.ParceFile(path, method);
            FilePreviwText.Text = result;
            await viewModel.logger.SavePathAsync(path);
            History.Items.Add(path);
        }

        private async void History_SelectedIndexChanged(object sender, EventArgs e)
        {
            var path = History.SelectedItem?.ToString();
            if (!String.IsNullOrEmpty(path))
            {
                CurrentPath.Text = path;
                var result = await viewModel.ParceFile(path, method);
                FilePreviwText.Text = result;
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
        }
    }
}
