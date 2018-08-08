using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views;
using System.Collections.Generic;
using TimeStamp.Resources.Model;
using TimeStamp.Resources.DataHelper;
using TimeStamp.Resources;
using Android.Util;
using Android.Content;

namespace TimeStamp
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ListView lstData;
        List<Tag> lstSource = new List<Tag>();
        DataBase db;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            ////create Database
            db = new DataBase();
            db.CreateDatabase();
            //string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //Log.Info("DB_PATH", folder);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            lstData = FindViewById<ListView>(Resource.Id.listView);


            var editName = FindViewById<EditText>(Resource.Id.edtName);

            Button btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            Button btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            Button btnDelete = FindViewById<Button>(Resource.Id.btnDelete);


            //load data
            LoadData();

            btnAdd.Click += delegate
            {
                Tag tag = new Tag()
                {
                    TagName = editName.Text
                };
                db.InsertIntoTableTag(tag);
                LoadData();
            };

            btnEdit.Click += delegate
            {
                Tag tag = new Tag()
                {
                    ID = int.Parse(editName.Tag.ToString()),
                    TagName = editName.Text
                };
                db.UpdateTableTag(tag);
                LoadData();
            };

            btnDelete.Click += delegate
            {
                //set alert for executing the task
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Confirm delete");
                alert.SetMessage("Delete this list?");
                alert.SetPositiveButton("Delete", (senderAlert, args) =>
                {
                    Tag tag = new Tag()
                    {
                        ID = int.Parse(editName.Tag.ToString())
                    };
                    db.DeleteIntoTableTag(tag);
                    LoadData();
                    Toast.MakeText(this, "Deleted!", ToastLength.Short).Show();
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Ok.", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();


            };

            var btnUseIt = FindViewById<Button>(Resource.Id.btnUseIt);

            lstData.ItemClick += (s, e) =>
            {

                for (int i = 0; i < lstData.Count; i++)
                {
                    if (e.Position == i)
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.LightGray);
                    else
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }

                //binding data
                TextView txtName = e.View.FindViewById<TextView>(Resource.Id.txtTag);
                editName.Text = txtName.Text;
                editName.Tag = e.Id;
                btnUseIt.Enabled = true;
            };


            btnUseIt.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(UseTag));
                intent.PutExtra("tag_id", int.Parse(editName.Tag.ToString()));
                StartActivity(intent);
            };

            var btnClearAll = FindViewById<Button>(Resource.Id.btnClearTag);
            btnClearAll.Click += delegate
            {
                //set alert for executing the task
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Confirm delete");
                alert.SetMessage("Delete all lists?");
                alert.SetPositiveButton("Delete", (senderAlert, args) =>
                {
                    db.ClearDatabase();
                    LoadData();
                    Toast.MakeText(this, "Deleted!", ToastLength.Short).Show();
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "Ok.", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();


            };

            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
        }


        private void LoadData()
        {
            lstSource = db.SelectTableTag();
            ListViewAdapter adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }


    }
}

