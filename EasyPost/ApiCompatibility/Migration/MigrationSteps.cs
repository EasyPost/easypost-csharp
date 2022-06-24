using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost.Clients;
using EasyPost.Interfaces;
using EasyPost.Models.Migrations.V2;

namespace EasyPost.ApiCompatibility.Migration
{
    internal static class MigrationSteps
    {
        /// <summary>
        ///     List of migrations to execute between API versions.
        ///     Please, remember to add new entries AT THE BOTTOM of the list.
        ///     It's very important to ensure these migrations execute in order.
        /// </summary>
        private static readonly List<MigrationSet> MigrationSets = new List<MigrationSet>
        {
            new MigrationSet(ApiVersion.V2, ApiVersion.Latest)
            {
                Migrations = new List<IMigrationEntry>
                {
                    new SampleMigration()
                }
            },
        };

        /// <summary>
        ///     Migrate an object from its client-version-compatible form to the latest-API-version-compatible form.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static MigrationResult MigrateIfNeeded(BaseClient client, IMigratable obj)
        {
            ApiVersionDetails clientApiVersion = client.ApiVersionDetails;
            // Always gets the migrations between the client version and the latest API version
            List<MigrationSet> migrationSets = GetMigrationSets(clientApiVersion, ApiVersionDetails.GetApiVersionDetails(ApiVersion.Latest));

            IMigratable updatedObject = obj;
            Type finalType = obj.GetType();
            foreach (var migrationSet in migrationSets)
            {
                MigrationResult? migrationResult = migrationSet.MigrateForwardIfNeeded(updatedObject);
                if (migrationResult == null)
                {
                    continue;
                }

                updatedObject = migrationResult.NewObject;
                finalType = migrationResult.NewObjectType;
            }

            return new MigrationResult(obj, updatedObject, finalType);
        }

        private static List<MigrationSet> GetMigrationSets(ApiVersionDetails fromApiVersion, ApiVersionDetails toApiVersion)
        {
            return MigrationSets.Where(migrationSet => migrationSet.FromApiVersion >= fromApiVersion).Where(migrationSet => toApiVersion <= migrationSet.ToApiVersion).ToList();
        }
    }
}
