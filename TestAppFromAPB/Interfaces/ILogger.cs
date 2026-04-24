using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestAppFromAPB.Interfaces
{
    public interface ILogger
    {
        public Task SavePathAsync(string path);
        public Task<List<string>> GetPathesAsync(int count);
    }
}
