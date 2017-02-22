using System;
using SQLite.Net;
using System.IO;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using todolist.Class;

namespace todolist
{
    public class AccessDB : IDisposable
    {
        public SQLiteConnection connectionSQLite;
        public AccessDB()
        {
            var config = DependencyService.Get<IConfig>();
            connectionSQLite = new SQLiteConnection(config.Platform, Path.Combine(config.DirSQLite, "todolist.db3"));
            connectionSQLite.CreateTable<Activity>();
        }

        #region Activity

        public void InsertActivity(Activity a)
        {
            connectionSQLite.Insert(a);
        }
        public void UpdateActivity(Activity a)
        {
            connectionSQLite.Update(a);
        }
        public void DeleteActivity(Activity a)
        {
            connectionSQLite.Delete(a);
        }
        public void DeleteAllActivities()
        {
            connectionSQLite.DeleteAll<Activity>();
        }
        public Activity GetActivity(int id)
        {
            return connectionSQLite.Table<Activity>().FirstOrDefault(c => c.ID == id);
        }
        public List<Activity> GetActivities()
        {
            return connectionSQLite.Table<Activity>().OrderBy(c => c.ID).ToList();
        }

        #endregion

        public void Dispose()
        {
            connectionSQLite.Dispose();
        }
    }
}
