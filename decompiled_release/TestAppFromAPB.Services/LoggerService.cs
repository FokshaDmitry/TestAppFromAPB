using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestAppFromAPB.Interfaces;
using TestAppFromAPB.Models;

namespace TestAppFromAPB.Services;

public class LoggerService : ILogger
{
	private string logFile;

	public LoggerService()
	{
		logFile = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\log.json";
	}

	public async Task<List<string>> GetPathesAsync(int count)
	{
		List<LoggerModel> list = JsonConvert.DeserializeObject<List<LoggerModel>>(await File.ReadAllTextAsync(logFile));
		if (list == null)
		{
			list = new List<LoggerModel>();
		}
		return (from l in list
			orderby l.CreateTime descending
			select l.Path).Take(count).ToList();
	}

	public async Task SavePathAsync(string path)
	{
		List<LoggerModel> list = JsonConvert.DeserializeObject<List<LoggerModel>>(await File.ReadAllTextAsync(logFile));
		if (list == null)
		{
			list = new List<LoggerModel>();
		}
		if (!list.Select((LoggerModel l) => l.Path).Contains<string>(path))
		{
			list.Add(new LoggerModel
			{
				Id = Guid.NewGuid(),
				Path = path,
				CreateTime = DateTime.Now
			});
			await File.WriteAllTextAsync(logFile, JsonConvert.SerializeObject(list));
		}
	}
}
