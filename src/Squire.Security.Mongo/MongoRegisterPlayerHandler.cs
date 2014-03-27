namespace Squire.Mongo.Authentication
{
    using Squire.Sentinel.Commands;
    using Squire.Unhinged.Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class MongoRegisterPlayerHandler : IHandleCommand<RegisterPlayer>
    {
        private readonly IMongoContext mongo;

        public MongoRegisterPlayerHandler(IMongoContext mongo)
        {
            mongo.VerifyParam("mongo").IsNotNull();
            this.mongo = mongo;
        }

        public void Invoke(RegisterPlayer command)
        {
            var player = new MongoPlayer(command.Name, command.Email, command.PasswordHash);
            this.mongo.Database.GetCollection<MongoPlayer>("players").Save(player);
        }
    }
}
