namespace Squire.Decoupled.Mongo.DomainEvents
{
    using Squire.Validation;
    using Squire.Decoupled.DomainEvents;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using System;

    public class MongoStoredEvent
    {
        public MongoStoredEvent()
        {
            this.TypeName = null;
            this.SerializedEvent = null;
        }

        public string TypeName
        {
            get;
            private set;
        }

        public byte[] SerializedEvent
        {
            get;
            set;
        }

        public void SetEvent<TEvent>(TEvent domainEvent)
            where TEvent : class, IDomainEvent
        {
            ValidationHelper.ArgumentNotNull(domainEvent, "domainEvent");
            this.TypeName = typeof(TEvent).AssemblyQualifiedName;
            this.SerializedEvent = domainEvent.ToBson();
        }

        public IDomainEvent ToEvent()
        {
            if (this.TypeName == null || this.SerializedEvent == null)
            {
                throw new InvalidOperationException("this stored event instance cannot be deserialized. no metadata was found.");
            }

            var type = Type.GetType(this.TypeName);
            if (type == null)
            {
                throw new InvalidOperationException(string.Format("stored event type '{0}' could not be found.", this.TypeName));
            }

            var bson = BsonSerializer.Deserialize<BsonDocument>(this.SerializedEvent);
            var result = BsonSerializer.Deserialize(bson, type) as IDomainEvent;
            if (result == null)
            {
                throw new InvalidOperationException(string.Format("The event type '{0}' cannot be cast to IDomainEvent", this.TypeName));
            }

            return result;
        }
    }
}
