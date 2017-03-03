using System;
using System.Collections.Generic;
using System.Linq;
using SQLite.Net.Interop;

using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Services
{
    public interface IConfig
    {
        string DirSQLite { get; }
        ISQLitePlatform Platform { get; }
    }
}
