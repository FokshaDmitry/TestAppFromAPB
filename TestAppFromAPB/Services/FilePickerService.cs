using System;
using System.Collections.Generic;
using System.Text;
using TestAppFromAPB.Interfaces;

namespace TestAppFromAPB.Services
{
    public class FilePickerService : IFilePicker
    {
        public async Task<string> GetFileAsync()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select file";

                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Получаем полный путь к файлу
                    string filePath = openFileDialog.FileName;
                    return filePath;
                }
                else
                {
                    MessageBox.Show($"Please try again");
                    return "";
                }
            }
        }
        public async Task SaveFileAsync(string fileText)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Настройки диалога
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.Title = "Save filter file";
                saveFileDialog.DefaultExt = "txt";

                // Предложим стандартное имя файла
                saveFileDialog.FileName = "FilteredData.txt";

                // Показываем диалог пользователю
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Путь, который выбрал пользователь
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        File.WriteAllText(filePath, fileText, System.Text.Encoding.UTF8);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"File don't save: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
