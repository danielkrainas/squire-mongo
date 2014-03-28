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

    public class MongoRoleResolver : IRoleResolver
    {
        public MongoRoleResolver()
        {
        }

        public ICollection<string> ResolveFor(IPlayer player)
        {
            if (player is MongoPlayer)
            {
                return ((MongoPlayer)player).Roles;
            }

            return new List<string>();
        }
    }
}
