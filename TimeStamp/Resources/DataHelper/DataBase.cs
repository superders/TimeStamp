using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Android.Util;
using TimeStamp.Resources.Model;

namespace TimeStamp.Resources.DataHelper
{
    public class DataBase
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool CreateDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    connection.CreateTable<Tag>();
                    connection.CreateTable<Moment>();
                    return true;
                }


            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
                return false;
            }
        }

        #region TAG
        public bool InsertIntoTableTag(Tag data)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    connection.Insert(data);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
                return false;
            }
        }

        public List<Tag> SelectTableTag()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    return connection.Table<Tag>().ToList();

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
                return null;
            }
        }

        public bool UpdateTableTag(Tag data)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    connection.Query<Tag>("UPDATE TAG SET TagName =? WHERE ID=?", data.TagName, data.ID);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
                return false;
            }
        }

        public bool DeleteIntoTableTag(Tag data)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    connection.Delete(data);
                    DeleteIntoTableMoment(data.ID);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
                return false;
            }
        }

        public bool SelectTag(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    connection.Query<Tag>("SELECT * FROM TAG WHERE ID=?", id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
                return false;
            }
        }

        internal void ClearDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    connection.DeleteAll<Tag>();
                    connection.DeleteAll<Moment>();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
            }
        }

        #endregion

        #region MOMENT
        public bool InsertIntoTableMoment(Moment data)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    connection.Insert(data);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
                return false;
            }
        }

        public List<Moment> SelectTableMoment()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    return connection.Table<Moment>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// cancella tutti i record del tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public bool DeleteIntoTableMoment(int tagId)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    connection.Query<Tag>("DELETE FROM MOMENT WHERE IdTag=?", tagId);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
                return false;
            }
        }

        public List<Moment> SelectMomentOfTag(int tagId)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Tag.db")))
                {
                    return connection.Query<Moment>("SELECT * FROM MOMENT WHERE IdTag=?", tagId).ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SqLiteEx", ex.Message);
                return null;
            }
        }
        #endregion
    }
}