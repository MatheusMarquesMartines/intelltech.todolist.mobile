using SQLite.Net.Interop;


namespace todolist.Class
{
    public interface IConfig
    {
        string DirSQLite { get; }
        ISQLitePlatform Platform { get; }
    }
}
