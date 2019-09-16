using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

namespace Assets.Scripts.Workers.IO.Helpers
{
    internal static class FireBaseDatabase
    {
        private static DatabaseReference _database = null;

        public static DatabaseReference Database
        {
            get
            {
                if (_database == null)
                {
                    ConfigureDB();
                }

                return _database;
            }
        }

        private static void ConfigureDB()
        {   
                        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);

            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://squared-105cf.firebaseio.com/");

            // Get the root reference location of the database.     
            _database = FirebaseDatabase.DefaultInstance.RootReference;
            _database.Database.SetPersistenceEnabled(true);
        }
    }
}
