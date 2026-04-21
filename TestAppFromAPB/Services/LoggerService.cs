using TestAppFromAPB.Interfaces;
using TestAppFromAPB.Models;
using Newtonsoft.Json;

namespace TestAppFromAPB.Services
{
    public class LoggerService : ILogger
    {
        string logFile;
        public LoggerService()
        {
            logFile = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\log.json";
        }
        public async Task<List<string>> GetPathesAsync(int count)
        {
            var logText = await File.ReadAllTextAsync(logFile);
            var logList = JsonConvert.DeserializeObject<List<LoggerModel>>(logText);
            if (logList == null)
            {
                logList = new List<LoggerModel>();
            }
            return logList.OrderByDescending(l => l.CreateTime).Select(l => l.Path).Take(count).ToList()!;
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
