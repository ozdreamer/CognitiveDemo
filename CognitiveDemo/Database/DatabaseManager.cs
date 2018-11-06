using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace CognitiveDemo
{
    public class DatabaseManager
    {
        SQLiteConnection dbConnection;

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

        public User GetUser(Guid userId)
        {
            return this.dbConnection.Query<User>($"SELECT * FROM User WHERE UserId='{userId}'").FirstOrDefault();
        }


        public int SaveUser(User user)
        {
            return this.dbConnection.Insert(user);
        }
    }
}
