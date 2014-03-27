namespace Squire.Mongo
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class MongoDatabaseExtensions
    {
        public static MongoCollection<T> GetCollection<T>(this MongoDatabase database)
        {
            var name = typeof(T).Name.ToLowerInvariant();
            if (name.EndsWith("y"))
            {
                name = name.Substring(0, name.Length - 1) + "ies";
            }
            else
            {
                name += "s";
            }

            var collection = database.GetCollection<T>(name);
            if (collection == null)
            {
                throw new InvalidOperationException("collection not found.");
            }

            return collection;
        }
    }
}
