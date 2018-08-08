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

namespace TimeStamp.Resources.Model
{
    public class Moment
    {

        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }

        public DateTime Timestamp { get; set; }

        public int IdTag { get; set; }

        public override string ToString()
        {
            return string.Format("[Moment:ID={0} IdTag={1}, Timestamp={2}]", ID, IdTag, Timestamp.ToString());
        }
    }
}