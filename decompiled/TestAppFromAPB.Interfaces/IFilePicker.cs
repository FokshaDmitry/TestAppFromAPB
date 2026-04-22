using System.Threading.Tasks;

namespace TestAppFromAPB.Interfaces;

public interface IFilePicker
{
	Task<string> GetFileAsync();

	Task SaveFileAsync(string fileText);
}
