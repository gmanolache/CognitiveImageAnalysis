using System;
using System.Collections.Generic;
using SQLite;

namespace CognitiveImageViewer
{
    public class RedditSqliteImporter : IDisposable
    {
        private readonly SQLiteConnection connection;

        public RedditSqliteImporter(string database)
        {
            connection = new SQLiteConnection(database);
        }

        public void Dispose()
        {
            connection?.Close();
        }

        public IEnumerable<RedditImage> Import()
        {
            return connection.Query<RedditImage>("SELECT * from gonewild");
        }
    }
}