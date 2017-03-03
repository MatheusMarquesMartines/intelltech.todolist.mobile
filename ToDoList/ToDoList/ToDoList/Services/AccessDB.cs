using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToDoList.Models;
using Xamarin.Forms;

namespace ToDoList.Services
{
    public class AccessDB : IDisposable
    {
        public SQLiteConnection connectionSQLite;
        public AccessDB()
        {
            var config = DependencyService.Get<IConfig>();
            connectionSQLite = new SQLiteConnection(config.Platform, Path.Combine(config.DirSQLite, "todolist2.db3"));
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
        public Activity GetActivity(String id)
        {
            try
            {
                return connectionSQLite.Table<Activity>().FirstOrDefault(c => c.Id == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Activity> GetActivities()
        {
            return connectionSQLite.Table<Activity>().OrderBy(c => c.Id).ToList();
        }

        #endregion

        public void Dispose()
        {
            connectionSQLite.Dispose();
        }
    }
}
