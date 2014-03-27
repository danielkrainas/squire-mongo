namespace Squire.Mongo
{
    using MongoDB.Driver;
    using System;

    public interface IMongoContext
    {
        MongoClient Client
        {
            get;
        }

        MongoUrl ConnectionString
        {
            get;
        }

        MongoDatabase Database
        {
            get;
        }

        MongoServer Server
        {
            get;
        }
    }
}
