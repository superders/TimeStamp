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
using Java.Lang;
using TimeStamp.Resources.Model;

namespace TimeStamp.Resources
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtName { get; set; }
    }
    public class ListViewAdapter : BaseAdapter
    {
        private List<Tag> lstTag;
        private Activity activity;

        public override int Count
        {
            get
            {
                return lstTag.Count;
            }
        }

        public ListViewAdapter(Activity activity, List<Tag> lstTag)
        {
            this.activity = activity;
            this.lstTag = lstTag;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstTag[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.tag_list, parent, false);
            var txtName = view.FindViewById<TextView>(Resource.Id.txtTag);

            txtName.Text = lstTag[position].TagName;

            return view;
        }
    }

    public class ListViewAdapterMom : BaseAdapter
    {
        private List<Moment> lstMom;
        private Activity activity;

        public override int Count
        {
            get
            {
                return lstMom.Count;
            }
        }

        public ListViewAdapterMom(Activity activity, List<Moment> lstMom)
        {
            this.activity = activity;
            this.lstMom = lstMom;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstMom[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.tag_list, parent, false);
            var txtTimeStamp = view.FindViewById<TextView>(Resource.Id.txtTag);

            txtTimeStamp.Text = lstMom[position].Timestamp.ToString();

            return view;
        }
    }
}