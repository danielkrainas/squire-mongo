namespace Squire.Decoupled.Mongo.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Squire.Validation;
    using MongoDB.Driver.Linq;
    using Squire.Decoupled.DomainEvents;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System.IO;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.IO;
    using MongoDB.Driver.Builders;

    public class MongoStoredEventBatch
    {
        public MongoStoredEventBatch(Guid batchId)
        {
            this.Id = batchId;
            this.Entries = new List<MongoStoredEvent>();
        }

        public MongoStoredEventBatch()
            : this(Guid.Empty)
        {
        }

        public Guid Id
        {
            get;
            set;
        }

        public List<MongoStoredEvent> Entries
        {
            get;
            set;
        }

        public IEnumerable<IDomainEvent> Events
        {
            get
            {
                foreach (var entry in this.Entries)
                {
                    yield return entry.ToEvent();
                }
            }
        }
    }
}
