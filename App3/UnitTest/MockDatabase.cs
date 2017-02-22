using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using todolist.Class;
using System.Collections;

namespace todolist
{
    public class MockDatabase : IDisposable
    {
        HashSet<Activity> _dateActivities = new HashSet<Activity>();

        public MockDatabase()
        { }

        #region Activity

        public void InsertActivity(Activity a)
        {
            _dateActivities.Add(a);
        }

        public void DeleteActivity(Activity a)
        {
            _dateActivities.Remove(a);
        }
        public void DeleteAllActivities()
        {
            _dateActivities.Clear();
        }
        public IEnumerable GetActivity(int id)
        {
            return _dateActivities.Where(dp => dp.ID == id);
        }
        public List<Activity> GetActivities()
        {
            return _dateActivities.ToList();
        }

        public void Dispose()
        {
           // connectionSQLite.Dispose();
        }
        #endregion
    }
}