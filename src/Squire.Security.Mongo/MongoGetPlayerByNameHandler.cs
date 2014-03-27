namespace Squire.Mongo.Authentication
{
    using Squire.Sentinel;
    using Squire.Sentinel.Queries;
    using Squire.Unhinged.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;
    using MongoDB.Bson;
    using MongoDB.Driver.Builders;

    public class MongoGetPlayerByNameHandler : IExecuteQuery<GetPlayerByName, IPlayer>
    {
        private IMongoContext mongo;

        public MongoGetPlayerByNameHandler(IMongoContext mongo)
        {
            mongo.VerifyParam("mongo").IsNotNull();
            this.mongo = mongo;
        }

        public IPlayer Execute(GetPlayerByName query)
        {
            return this.mongo.Database.GetCollection<MongoPlayer>("players")
                .FindOne(Query.EQ("Name", query.Name));
        }
    }
}
