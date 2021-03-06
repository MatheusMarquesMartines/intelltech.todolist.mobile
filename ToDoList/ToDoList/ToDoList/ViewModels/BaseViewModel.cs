﻿using ToDoList.Helpers;
using ToDoList.Models;
using ToDoList.Services;

using Xamarin.Forms;

namespace ToDoList.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Activity> DataStore => DependencyService.Get<IDataStore<Activity>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        bool isBusySync = false;
        public bool IsBusySync
        {
            get { return isBusySync; }
            set { SetProperty(ref isBusySync, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}

