namespace Squire.Decoupled.Mongo.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Squire.Decoupled.DomainEvents;
    using Squire.Validation;
    using Squire.Mongo;

    public static class EventPipelineBuilderExtensions
    {
        public static EventPipelineBuilder StoreWithMongo(this EventPipelineBuilder builder, IMongoContext mongo)
        {
            builder.VerifyParam("builder").IsNotNull();
            mongo.VerifyParam("mongo").IsNotNull();
            return builder.StoreEvents(new MongoEventStorage(mongo));
        }
    }
}
