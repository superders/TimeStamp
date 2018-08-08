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
using TimeStamp.Resources.Model;
using TimeStamp.Resources.DataHelper;
using TimeStamp.Resources;

namespace TimeStamp
{
    [Activity(Label = "UseTag")]
    public class UseTag : Activity
    {
        ListView lstData;
        int tagId;
        List<Moment> lstSource = new List<Moment>();
        DataBase db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.whale);

            ////create Database
            db = new DataBase();
            db.CreateDatabase();

            tagId = Intent.GetIntExtra("tag_id", -1);

            lstData = FindViewById<ListView>(Resource.Id.momentList);

            // Create your application here


            //load data
            LoadData();

            var ImageButton = FindViewById<ImageButton>(Resource.Id.imageButton1);
            ImageButton.Click += delegate
                {
                    //LinearLayout ListView = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
                    //TextView TextView = new TextView(BaseContext);
                    //TextView.Text = DateTime.Now.ToString();
                    //ListView.AddView(TextView);


                    //SALVO I DATI
                    Moment mom = new Moment()
                    {
                        IdTag = tagId,
                        Timestamp = DateTime.Now
                    };
                    db.InsertIntoTableMoment(mom);
                    LoadData();
                };

            var btnClear = FindViewById<Button>(Resource.Id.btnClear);
            btnClear.Click += delegate
            {
                db.DeleteIntoTableMoment(tagId);
                LoadData();
            };
        }

        private void LoadData()
        {
            lstSource = db.SelectMomentOfTag(tagId);
            ListViewAdapterMom adapter = new ListViewAdapterMom(this, lstSource);
            lstData.Adapter = adapter;
        }
    }
}