using System;
using SQLite.Net.Interop;
using Xamarin.Forms;
using ToDoList.Services;

[assembly: Dependency(typeof(ToDoList.Droid.Config))]


namespace ToDoList.Droid
{
    public class Config : IConfig
    {
        private string _dirSQLite;
        private SQLite.Net.Interop.ISQLitePlatform _platform;
        public string DirSQLite
        {
            get
            {
                if (string.IsNullOrEmpty(_dirSQLite))
                {
                    _dirSQLite = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return _dirSQLite;
            }
        }
        public ISQLitePlatform Platform
        {
            get
            {
                if (_platform == null)
                {
                    _platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return _platform;
            }
        }
    }
}