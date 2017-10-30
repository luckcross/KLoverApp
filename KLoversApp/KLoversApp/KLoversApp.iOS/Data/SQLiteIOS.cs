using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.IO;
using KLoversApp.iOS.Data;
using Xamarin.Forms;
using KLoversApp.Data;

[assembly: Dependency(typeof(SQLiteIOS))]

namespace KLoversApp.iOS.Data
{
    public class SQLiteIOS : ISQLite
    {
        public SQLiteIOS() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            string fileName = "TesteDB.db3";
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentPath, "..", "Library");
            string path = Path.Combine(libraryPath, fileName);

            SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(path);
            return connection;
        }

    }
}