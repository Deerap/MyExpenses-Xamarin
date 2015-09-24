//#define PRELOAD
using System;
using System.Collections.Generic;
using System.Linq;
using MyExpenses.Portable.Helpers;
using MyExpenses.Portable.Interfaces;
using MyExpenses.Portable.Models;
using SQLite.Net;


namespace MyExpenses.Portable.DataLayer
{
    /// <summary>
    /// TaskDatabase builds on SQLite.Net and represents a specific database, in our case, the Task DB.
    /// It contains methods for retrieval and persistance as well as db creation, all based on the 
    /// underlying ORM.
    /// </summary>
    public class ExpenseDatabase
    {
        static object locker = new object();

        SQLiteConnection database;
        private ICloudService cloudService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDatabase"/> ExpenseDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public ExpenseDatabase(SQLiteConnection conn)
        {
            database = conn;
            // create the tables
            database.CreateTable<Expense>();
            cloudService = ServiceContainer.Resolve<ICloudService>();
        }

        public IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                var items = (from i in database.Table<T>() select i).ToList();
                return items;
            }
        }

        /// <summary>
        /// Only grab items that are visible and for this specific user!
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Expense> GetVisibleItems()
        {
            lock (locker)
            {
                var items = database.Table<Expense>().ToList().Where(x => x.IsVisible && x.UserId == cloudService.UserId).OrderBy(x => x.Due);
                return items;
            }
        }

        public Expense GetItem(string id)
        {
            lock (locker)
            {
                return database.Table<Expense>().FirstOrDefault(x => x.AzureId == id);
            }
        }

        public T GetItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                return database.Table<T>().FirstOrDefault(x => x.Id == id);
                // Following throws NotSupportedException - thanks aliegeni
                //return (from i in Table<T> ()
                //        where i.ID == id
                //        select i).FirstOrDefault ();
            }
        }

        public int SaveItem<T>(T item) where T : IBusinessEntity
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                return database.Delete<T>(id);
            }
        }
    }
}