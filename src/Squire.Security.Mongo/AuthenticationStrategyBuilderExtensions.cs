namespace Squire.Security.Mongo
{
    using Squire.Mongo;
    using Squire.Sentinel.Authentication;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class AuthenticationStrategyBuilderExtensions
    {
        public static AuthenticationStrategyBuilder MongoRoles(this AuthenticationStrategyBuilder builder)
        {
            return builder.ResolveRolesBy(new MongoRoleResolver());
        }

        public static AuthenticationStrategyBuilder MongoPlayers(this AuthenticationStrategyBuilder builder, IMongoContext mongo)
        {
            return builder.ResolvePlayersBy(new MongoPlayerResolver(mongo));
        }

        public static AuthenticationStrategyBuilder MongoPlayersAndRoles(this AuthenticationStrategyBuilder builder, IMongoContext mongo)
        {
            return builder.MongoPlayers(mongo).MongoRoles();
        }
    }
}
