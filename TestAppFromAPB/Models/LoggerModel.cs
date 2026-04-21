using System;
using System.Collections.Generic;
using System.Text;

namespace TestAppFromAPB.Models
{
    public class LoggerModel
    {
        public Guid Id { get; set; }
        public string? Path { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
