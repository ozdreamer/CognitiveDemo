using System;
using SQLite.Net;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace CognitiveDemo
{
    public class DatabaseManager
    {
        SQLiteConnection dbConnection;

        class temp
        {
            public string type { get; set; }
            public string name { get; set; }
            public string tbl_name { get; set; }
            public string rootpage { get; set; }
            public string sql { get; set; }
        }

        public DatabaseManager()
        {
            this.dbConnection = DependencyService.Get<IDBInterface>().CreateConnection();
        }

        public List<User> GetAllUsers()
        {
            return this.dbConnection.Query<User>("SELECT * FROM User");
        }

        public User GetUser(string email)
        {
            return this.dbConnection.Query<User>($"SELECT * FROM User WHERE Email='{email}'").FirstOrDefault();
        }

        public int SaveUser(User user)
        {
            return this.dbConnection.Insert(user);
        }
    }
}
