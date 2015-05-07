﻿using System.Data.Entity.Core.EntityClient;

namespace Snippets_Misc.EF_EDMX
{
    class EntityConnectionCreation
    {
        void Temp()
        {
            #region EntityConnectionCreationAndUsage

            var entityBuilder = new EntityConnectionStringBuilder()
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = "the database connection string",
                Metadata = @"res://*/MySample.csdl|res://*/MySample.ssdl|res://*/MySample.msl"
            };

            var entityConn = new EntityConnection(entityBuilder.ToString());

            using (var ctx = new MySampleContainer(entityConn))
            {
                //use the DbContext as required
            }

            #endregion
        }
    }
}