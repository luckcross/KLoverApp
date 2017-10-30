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
using KLoversApp.Data;
using System.IO;
using Xamarin.Forms;
using KLoversApp.Droid.Data;

[assembly: Dependency(typeof(SQLiteAndroid))]

namespace KLoversApp.Droid.Data
{
    public class SQLiteAndroid : ISQLite
    {
        public SQLiteAndroid() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            string sqlLiteFileName = "TesteDB.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentsPath, sqlLiteFileName);

            SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(path);
            return connection;
        }
    }
}