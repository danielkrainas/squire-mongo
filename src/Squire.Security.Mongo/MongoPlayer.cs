namespace Squire.Mongo.Authentication
{
    using Squire.Sentinel;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MongoPlayer : IPlayer
    {
        public MongoPlayer()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Id = ObjectId.GenerateNewId(this.CreatedOn);
        }

        public MongoPlayer(ObjectId id)
            : this()
        {
            this.Id = id;
        }

        public MongoPlayer(string name, string email, string passwordHash)
            : this()
        {
            this.Name = name;
            this.Email = email;
            this.Password = passwordHash;
        }

        public ObjectId Id
        {
            get;
            private set;
        }

        string IPlayer.Id
        {
            get
            {
                return this.Id.ToString();
            }
        }

        public string Email
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool IsDisabled
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public string AuthenticationType
        {
            get
            {
                return "incant.mongo";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Password
        {
            get;
            set;
        }
    }
}
