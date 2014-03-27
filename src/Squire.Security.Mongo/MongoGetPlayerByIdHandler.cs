namespace Squire.Security.Mongo
{
    using Squire.Sentinel;
    using Squire.Sentinel.Queries;
    using Squire.Unhinged.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;
    using MongoDB.Driver.Builders;
    using MongoDB.Bson;
    using Squire.Mongo;
    using Squire.Mongo.Authentication;

    public class MongoGetPlayerByIdHandler : IExecuteQuery<GetPlayerById, IPlayer>
    {
        private readonly IMongoContext mongo;

        public MongoGetPlayerByIdHandler(IMongoContext mongo)
        {
            mongo.VerifyParam("mongo").IsNotNull();
            this.mongo = mongo;
        }

        public IPlayer Execute(GetPlayerById query)
        {
            return mongo.Database.GetCollection<MongoPlayer>("players")
                .FindOneById(BsonValue.Create(query.Id));
        }
    }
}
