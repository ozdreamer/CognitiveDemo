using System;
using System.IO;
using CognitiveDemo.iOS;
using Foundation;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseService))]
namespace CognitiveDemo.iOS
{
    public class DatabaseService : IDBInterface
    {
        public SQLiteConnection CreateConnection()
        {
            var sqliteFilename = "CognitiveDemo.db";

            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            string path = Path.Combine(libFolder, sqliteFilename);

            // This is where we copy in the pre-created database
            if (!File.Exists(path))
            {
                var existingDb = NSBundle.MainBundle.PathForResource("CognitiveDemo", "db");
                File.Copy(existingDb, path);
            }

            return new SQLiteConnection(path);
        }
    }
}