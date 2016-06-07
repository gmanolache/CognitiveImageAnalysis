using System;
using SQLite;

namespace CognitiveImageViewer
{
    public class RedditSqliteExporter : IDisposable
    {
        private readonly SQLiteConnection connection;

        public RedditSqliteExporter(string database)
        {
            connection = new SQLiteConnection(database);
            connection.CreateTable<RedditImageOutput>();
        }


        public void Dispose()
        {
            connection?.Close();
        }

        public void AddRedditProcessedImage(RedditImageOutput image)
        {
            connection.Insert(image);
        }

        public bool IsProcessed(string link)
        {
            var result = connection.Query<RedditImageOutput>("select id from RedditImageOutput where Link = ?", link);
            return result.Count > 0;
        }
    }
}