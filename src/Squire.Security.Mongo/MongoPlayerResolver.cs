namespace Squire.Security.Mongo
{
    using Squire.Mongo;
    using Squire.Mongo.Authentication;
    using Squire.Sentinel;
    using Squire.Sentinel.Authentication;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;

    public class MongoPlayerResolver : IPlayerResolver
    {
        private readonly IMongoContext mongo;

        public MongoPlayerResolver(IMongoContext mongo)
        {
            mongo.VerifyParam("mongo").IsNotNull();
            this.mongo = mongo;
        }

        public IPlayer Resolve(RegistrationDetails registration)
        {
            return new MongoPlayer(registration.Name, registration.Email, registration.Password);
        }

        public IPlayer Resolve(string name)
        {
            var players = this.mongo.Database.GetCollection<MongoPlayer>("players");
            return players.AsQueryable().FirstOrDefault(p => p.Name == name);
        }
    }
}
