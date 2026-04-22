using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestAppFromAPB.Interfaces;

public interface ILogger
{
	Task SavePathAsync(string path);

	Task<List<string>> GetPathesAsync(int count);
}
