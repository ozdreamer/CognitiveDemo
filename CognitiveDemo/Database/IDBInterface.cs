using System;
using SQLite.Net;

namespace CognitiveDemo
{
    public interface IDBInterface
    {
        SQLiteConnection CreateConnection();
    }
}