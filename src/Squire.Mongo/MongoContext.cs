namespace Squire.Mongo
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using Squire.Validation;
    using System.Threading.Tasks;

    public abstract class MongoContext : IMongoContext
    {
        private MongoClient client;

        private MongoDatabase database = null;

        private MongoServer server = null;

        private MongoUrl url = null;

        public MongoContext(string connectionStringName)
        {
            connectionStringName.VerifyParam("connectionStringName").IsNotNull();
            var connectionStringSetting = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSetting == null)
            {
                throw new InvalidOperationException(string.Format("could not locate connection string '{0}'", connectionStringName));
            }

            this.Initialize(connectionStringSetting.ConnectionString);
        }

        public MongoContext()
        {
            var connectionStringSetting = ConfigurationManager.ConnectionStrings[this.GetType().Name];
            if (connectionStringSetting == null)
            {
                throw new InvalidOperationException(string.Format("could not locate connection string '{0}'", this.GetType().Name));
            }

            this.Initialize(connectionStringSetting.ConnectionString);
        }

        private void Initialize(string connectionString)
        {
            this.url = MongoUrl.Create(connectionString);
            this.client = new MongoClient(this.url);
            this.server = this.client.GetServer();
            this.database = this.server.GetDatabase(this.url.DatabaseName);
        }

        public MongoUrl ConnectionString
        {
            get
            {
                return this.url;
            }
        }

        public MongoClient Client
        {
            get
            {
                return this.client;
            }
        }

        public MongoDatabase Database
        {
            get
            {
                return this.database;
            }
        }

        public MongoServer Server
        {
            get
            {
                return this.server;
            }
        }
    }
}
