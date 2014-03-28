namespace Squire.Security.Mongo
{
    using MongoDB.Bson;
    using MongoDB.Bson.IO;
    using MongoDB.Bson.Serialization;
    using Squire.Mongo.Authentication;
    using Squire.Sentinel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class MongoPlayerTokenizer : IPlayerTokenizer
    {
        public MongoPlayerTokenizer()
        {
        }

        public string GetToken(IPlayer player)
        {
            player.VerifyParam("player").IsNotNull();
            if (!(player is MongoPlayer))
            {
                throw new InvalidOperationException("player type not supported by this registry");
            }

            var document = new BsonDocument();
            using(var buffer = new BsonBuffer())
            {
                using (var bsonWriter = BsonWriter.Create(document))
                {
                    BsonSerializer.Serialize<MongoPlayer>(bsonWriter, (MongoPlayer)player);
                }
            }

            return Convert.ToBase64String(document.ToBson());
        }

        public IPlayer Redeem(string token)
        {
            try
            {
                var player = BsonSerializer.Deserialize<MongoPlayer>(Convert.FromBase64String(token));
                return player;
            }
            catch
            {
                return null;
            }
        }
    }
}
