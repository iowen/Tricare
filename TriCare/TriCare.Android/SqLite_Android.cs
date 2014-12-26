using System;
using TriCare.Droid;
using TriCare.Data;
using Xamarin.Forms;
using System.IO;


[assembly: Dependency (typeof (SqLite_Android))]
namespace TriCare.Droid
{
	public class SqLite_Android : ISqLite
	{
		public SqLite_Android ()
		{
		}
		public SQLite.Net.SQLiteConnection GetConnection () {
			var sqliteFilename = "TriCareSQLite.db3";
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			// Create the connection
			var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var conn = new SQLite.Net.SQLiteConnection(plat, path);
			// Return the database connection 
			return conn;
		}
	}
}

