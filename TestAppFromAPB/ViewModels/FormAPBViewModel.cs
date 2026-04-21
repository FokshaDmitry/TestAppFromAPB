using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Text;
using TestAppFromAPB.Enums;
using TestAppFromAPB.Interfaces;
using TestAppFromAPB.Models;
using TestAppFromAPB.Services;

namespace TestAppFromAPB.ViewModels
{
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
        public async Task<string> ParceFile(string Path, FilterMethod filter)
        {
            // Parce файла с использованием LINQ и отложеным выполнением
            fileModels = File.ReadAllLines(Path).Select(line => line.Split(';')).Select(parts => new APBFileModel{ id = int.Parse(parts[0]), Age = int.Parse(parts[1]), Name = parts[2]}).ToList();
            if (fileModels.Count != 0)
            {
                //Создаём словарь для фильтрации по группам
                Dictionary<string, List<string>> filterByGroup = new Dictionary<string, List<string>>();

                switch (filter)
                {
                    case FilterMethod.Age:
                        filterByGroup = fileModels.GroupBy(u => u.Age.ToString()).OrderBy(g => int.Parse(g.Key)).ToDictionary(g => g.Key, g => g.Select(u => u.Age + ", " + u.Name).ToList())!;
                        break;

                    case FilterMethod.Name:
                        filterByGroup = fileModels.Where(u => !string.IsNullOrEmpty(u.Name)).GroupBy(u => u.Name![0].ToString().ToUpper()).OrderBy(g => g.Key).ToDictionary(g => g.Key, g => g.Select(u => u.Age + ", " + u.Name).ToList())!;
                        break;
                }
                // Используем StringBuilder, чтобы не перегружать память и сборщик мусора
                StringBuilder stringBuilder = new StringBuilder();
                int currentIndentCount = 0;
                int step = 6;

                foreach (var group in filterByGroup)
                {
                    int spacesToAppend = currentIndentCount * step;

                    foreach (var data in group.Value)
                    {
                        if (spacesToAppend > 0)
                        {
                            stringBuilder.Append(' ', spacesToAppend);
                        }
                        stringBuilder.Append("| ");
                        stringBuilder.AppendLine(data);
                    }
                    currentIndentCount++;
                }
                return stringBuilder.ToString();
            }
            else
            {
                return "File is empty";
            }
        }
        
    }
}
