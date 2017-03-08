using System;
using ToDoList.Helpers;

namespace ToDoList.Models
{
    public class BaseDataObject : ObservableObject
    {
        public BaseDataObject()
        {
            GUID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// GUID for item
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Azure created at time stamp
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Azure UpdateAt timestamp for online/offline sync
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Azure version for online/offline sync
        /// </summary>
        public string AzureVersion { get; set; }
    }
}
