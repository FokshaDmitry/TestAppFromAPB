using TestAppFromAPB.Interfaces;
using TestAppFromAPB.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Forms;

namespace TestAppFromAPB.Services
{
    public class LoggerService : ILogger
    {
        string logFile;
        string logDirectory;
        string tempDir;
        public LoggerService()
        {
            tempDir = Path.Combine(Path.GetTempPath(), "TestAppFromAPB");
            logDirectory = Path.Combine(tempDir, "Log");
            logFile = Path.Combine(logDirectory, "log.json");
        }

    public async Task<List<string>> GetPathesAsync(int count)
    {
        

        try
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            if (!File.Exists(logFile))
            {
                return new List<string>();
            }
            string logText;
            using (var stream = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                logText = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(logText))
            {
                return new List<string>();
            }

            var logList = JsonConvert.DeserializeObject<List<LoggerModel>>(logText);

            if (logList == null)
            {
                return new List<string>();
            }

            return logList
                .OrderByDescending(l => l.CreateTime)
                .Select(l => l.Path)
                .Take(count)
                .ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при чтении логов: {ex.Message}");
            return new List<string>();
        }
    }

    public async Task SavePathAsync(string path)
        {
            var logText = await File.ReadAllTextAsync(logFile);
            var logList = JsonConvert.DeserializeObject<List<LoggerModel>>(logText);
            if (logList == null)
            {
                logList = new List<LoggerModel>();
            }
            if (!logList.Select(l => l.Path).Contains(path))
            {
                logList.Add(new LoggerModel() { Id = Guid.NewGuid(), Path = path, CreateTime = DateTime.Now });
                await File.WriteAllTextAsync(logFile, JsonConvert.SerializeObject(logList));
            }
        }
    }
}
