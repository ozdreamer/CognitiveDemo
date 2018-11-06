using SQLite;

namespace CognitiveDemo
{
    public interface IDBInterface
    {
        SQLiteConnection CreateConnection();
    }
}