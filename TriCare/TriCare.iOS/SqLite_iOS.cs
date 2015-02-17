using System;
using TriCare.iOS;
using TriCare.Data;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency (typeof (SqLite_iOS))]
namespace TriCare.iOS
{
	public class SqLite_iOS : ISqLite
	{
		public SqLite_iOS ()
		{
		}
		public SQLite.Net.SQLiteConnection GetConnection ()
		{
			string sqliteFilename = "";
			#if DEBUG
			sqliteFilename = "TriCareSQLiteDBG.db3";
			#else
			sqliteFilename = "TriCareSQLite.db3";
			#endif
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			// Create the connection
			var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			var conn = new SQLite.Net.SQLiteConnection(plat, path);
			// Return the database connection
			return conn;
		}
	}
}

