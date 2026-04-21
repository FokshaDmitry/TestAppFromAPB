using System;
using System.Collections.Generic;
using System.Text;

namespace TestAppFromAPB.Interfaces
{
    public interface IFilePicker
    {
        public Task<string> GetFileAsync();
        public Task SaveFileAsync(string fileText);
    }
}
