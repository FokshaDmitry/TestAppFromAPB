using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestAppFromAPB.Interfaces;

namespace TestAppFromAPB.Services;

public class FilePickerService : IFilePicker
{
	public async Task<string> GetFileAsync()
	{
		using OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = "Select file";
		openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			return openFileDialog.FileName;
		}
		MessageBox.Show("Please try again");
		return "";
	}

	public async Task SaveFileAsync(string fileText)
	{
		using SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "Text files (*.txt)|*.txt|Все файлы (*.*)|*.*";
		saveFileDialog.FilterIndex = 1;
		saveFileDialog.Title = "Save filter file";
		saveFileDialog.DefaultExt = "txt";
		saveFileDialog.FileName = "FilteredData.txt";
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			string fileName = saveFileDialog.FileName;
			try
			{
				File.WriteAllText(fileName, fileText, Encoding.UTF8);
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("File don't save: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
		}
	}
}
