using System;
using System.IO;
using System.Linq;
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
        }

        private async void FormAPB_Load(object? sender, EventArgs e)
        {
            Filter.SelectedIndex = 0;
            // При загрузке формы, этот метод получает последние 10 путей к файлам из логгера и добавляет их в ComboBox History. Затем он устанавливает начальный метод фильтрации на основе выбранного элемента в ComboBox Filter. Это позволяет пользователю сразу видеть историю открытых файлов и выбирать метод фильтрации при загрузке приложения.
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
        // Этот метод обрабатывает событие клика по кнопке "Select File". Он открывает диалоговое окно для выбора файла, получает путь к выбранному файлу и отображает его в текстовом поле CurrentPath. Затем он вызывает метод ParceFile из viewModel для обработки выбранного файла с учетом текущего метода фильтрации и отображает результат в FilePreviwText. Также сохраняет путь к файлу в лог и добавляет его в историю.
        private async void SelectFile_Click(object sender, EventArgs e)
        {
            var path = await viewModel.filePicker.GetFileAsync();
            CurrentPath.Text = path;
            var result = await viewModel.ParceFile(path, method);
            FilePreviwText.Text = result;
            await viewModel.logger.SavePathAsync(path);
            History.Items.Add(path);
            PlusFile.Enabled = true;
        }
        // Этот метод обрабатывает событие изменения выбранного элемента в ComboBox History. Он получает путь к файлу из выбранного элемента, отображает его в CurrentPath и вызывает метод ParceFile для обработки файла с учетом текущего метода фильтрации. Результат отображается в FilePreviwText, а кнопка PlusFile становится доступной для добавления нового файла.
        private async void History_SelectedIndexChanged(object sender, EventArgs e)
        {
            var path = History.SelectedItem?.ToString();
            if (!String.IsNullOrEmpty(path))
            {
                CurrentPath.Text = path;
                var result = await viewModel.ParceFile(path, method);
                FilePreviwText.Text = result;
                PlusFile.Enabled = true;
            }
        }
        // Этот метод обрабатывает событие изменения выбранного элемента в ComboBox Filter. Он определяет новый метод фильтрации на основе выбранного элемента и вызывает метод ParceFile для обработки текущего файла с учетом нового метода фильтрации. Результат отображается в FilePreviwText. Если путь к файлу не указан, он просто применяет новый фильтр к существующим моделям и обновляет отображение.
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
        // Копирует текст из FilePreviwText в буфер обмена. Этот метод обрабатывает событие клика по кнопке "Copy Buffer". Он получает текст из FilePreviwText и использует Clipboard.SetText для копирования этого текста в буфер обмена, что позволяет пользователю вставить его в другое место.
        private void CopyBuffer_Click(object sender, EventArgs e)
        {
            var fileText = FilePreviwText.Text;
            Clipboard.SetText(fileText);
        }
        // Этот метод обрабатывает событие клика по кнопке "Save File". Он получает текст из FilePreviwText и вызывает метод SaveFileAsync из viewModel для сохранения этого текста в файл. После сохранения он очищает FilePreviwText и отключает кнопку PlusFile, чтобы предотвратить добавление новых файлов до тех пор, пока пользователь не выберет новый файл или не загрузит его через историю.
        private async void SaveFile_Click(object sender, EventArgs e)
        {
            var fileText = FilePreviwText.Text;
            await viewModel.filePicker.SaveFileAsync(fileText);
            FilePreviwText.Text = string.Empty;
            PlusFile.Enabled = false;
        }

        private async void PlusFile_Click(object? sender, EventArgs e)
        {
            var path = await viewModel.filePicker.GetFileAsync();
            var result = await viewModel.ParceFile(path, method, addFile: true);
            CurrentPath.Text = path;
            await viewModel.logger.SavePathAsync(path);
            FilePreviwText.Text = result;
            PlusFile.Enabled = true;
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
                //Проверяем, есть ли уже открытые файлы, если нет, то просто открываем, если есть, то спрашиваем, что делать с новым файлом
                if (viewModel.fileModels.Count() == 0)
                {
                    CurrentPath.Text = path;
                    var text = await viewModel.ParceFile(path, method);
                    FilePreviwText.Text = text;
                    await viewModel.logger.SavePathAsync(path);
                    History.Items.Add(path);
                    PlusFile.Enabled = true;
                    return;
                }
                //Создаем диалоговое окно с вариантами действий, если уже есть открытые файлы
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
                    PlusFile.Enabled = true;
                }
                else if (res == addForExist)
                {
                    var added = await viewModel.ParceFile(path, method, addFile: true);
                    CurrentPath.Text = path;
                    await viewModel.logger.SavePathAsync(path);
                    FilePreviwText.Text = added;
                    PlusFile.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("You can use only .txt file!", "Format error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
