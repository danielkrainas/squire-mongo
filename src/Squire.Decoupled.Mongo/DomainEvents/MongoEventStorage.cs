namespace Squire.Decoupled.Mongo.DomainEvents
{
    using Squire.Validation;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Squire.Decoupled.DomainEvents;
    using Squire.Mongo;

    public class MongoEventStorage : IDomainEventStorage
    {
        private readonly IMongoContext mongo;

        public MongoEventStorage(IMongoContext mongo)
        {
            mongo.VerifyParam("mongo").IsNotNull();
            this.mongo = mongo;
        }

        private void WithCollection(Action<MongoCollection<MongoStoredEventBatch>> activity)
        {
            var collection = this.mongo.Database.GetCollection<MongoStoredEventBatch>("mongo_domainevents");
            activity(collection);
        }

        private void WithBatch(Guid batchId, Action<MongoStoredEventBatch, MongoCollection> activity)
        {
            this.WithCollection(collection =>
            {
                var batch = collection.AsQueryable().FirstOrDefault(b => b.Id == batchId);
                activity(batch, collection);
            });
        }

        public void Hold(Guid batchId, IDomainEvent domainEvent)
        {
            ValidationHelper.ArgumentNotEmpty(batchId, "batchId", "you must specify a batchId");
            this.WithBatch(batchId, (batch, collection) =>
                {
                    if (batch == null)
                    {
                        batch = new MongoStoredEventBatch(batchId);
                    }

                    var storedEvent = new MongoStoredEvent();
                    storedEvent.SetEvent(domainEvent);
                    var result = collection.Save(batch);
                    if (!result.Ok)
                    {
                        // replace with a more meaningful exception class.
                        throw new InvalidOperationException("couldn't save domain event");
                    }
                });
        }

        public IEnumerable<IDomainEvent> Release(Guid batchId)
        {
            ValidationHelper.ArgumentNotEmpty(batchId, "batchId", "you must specify a batchId");
            var result = Enumerable.Empty<IDomainEvent>();
            this.WithBatch(batchId, (batch, collection) =>
                {
                    if (batch != null)
                    {
                        result = batch.Events;
                    }
                });

            return result;
        }

        public void Delete(Guid batchId)
        {
            ValidationHelper.ArgumentNotEmpty(batchId, "batchId", "you must specify a batchId");
            this.WithCollection(collection =>
                {
                    var result = collection.Remove(Query<MongoStoredEventBatch>.EQ(b => b.Id, batchId));
                    if (!result.Ok)
                    {
                        throw new InvalidOperationException(string.Format("failed to remove events for batch '{0}'", batchId));
                    }
                });
        }
    }
}
