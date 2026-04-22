using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestAppFromAPB.Enums;
using TestAppFromAPB.Interfaces;
using TestAppFromAPB.Models;
using TestAppFromAPB.Services;

namespace TestAppFromAPB.ViewModels;

public class FormAPBViewModel
{
	public List<APBFileModel> fileModels;

	public IFilePicker filePicker;

	public ILogger logger;

	public FormAPBViewModel()
	{
		fileModels = new List<APBFileModel>();
		filePicker = new FilePickerService();
		logger = new LoggerService();
	}

	public async Task<string> ChangeFilter(FilterMethod filter)
	{
		if (fileModels.Count != 0)
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			switch (filter)
			{
			case FilterMethod.Age:
				dictionary = (from u in fileModels
					group u by u.Age.ToString() into g
					orderby int.Parse(g.Key)
					select g).ToDictionary((IGrouping<string, APBFileModel> g) => g.Key, (IGrouping<string, APBFileModel> g) => g.Select((APBFileModel u) => u.Age + ", " + u.Name).ToList());
				break;
			case FilterMethod.Name:
				dictionary = (from u in fileModels
					where !string.IsNullOrEmpty(u.Name)
					group u by u.Name[0].ToString().ToUpper() into g
					orderby g.Key
					select g).ToDictionary((IGrouping<string, APBFileModel> g) => g.Key, (IGrouping<string, APBFileModel> g) => g.Select((APBFileModel u) => u.Age + ", " + u.Name).ToList());
				break;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = 6;
			foreach (KeyValuePair<string, List<string>> item in dictionary)
			{
				int num3 = num * num2;
				foreach (string item2 in item.Value)
				{
					if (num3 > 0)
					{
						stringBuilder.Append(' ', num3);
					}
					stringBuilder.Append("| ");
					stringBuilder.AppendLine(item2);
				}
				num++;
			}
			return stringBuilder.ToString();
		}
		return "File is empty";
	}

	public async Task<string> ParceFile(string Path, FilterMethod filter, bool addFile = false)
	{
		if (addFile)
		{
			if (fileModels.Count() != 0)
			{
				new List<APBFileModel>();
				List<APBFileModel> collection;
				try
				{
					collection = (from line in File.ReadAllLines(Path)
						select line.Split(';') into parts
						select new APBFileModel
						{
							id = int.Parse(parts[0]),
							Age = int.Parse(parts[1]),
							Name = parts[2]
						}).ToList();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error parsing file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return "Error parsing file: " + ex.Message;
				}
				fileModels.AddRange(collection);
			}
		}
		else
		{
			try
			{
				fileModels = (from line in File.ReadAllLines(Path)
					select line.Split(';') into parts
					select new APBFileModel
					{
						id = int.Parse(parts[0]),
						Age = int.Parse(parts[1]),
						Name = parts[2]
					}).ToList();
			}
			catch (Exception ex2)
			{
				MessageBox.Show("Error parsing file: " + ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return "Error parsing file: " + ex2.Message;
			}
		}
		return await ChangeFilter(filter);
	}
}
