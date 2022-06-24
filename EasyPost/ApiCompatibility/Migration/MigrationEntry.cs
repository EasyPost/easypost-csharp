using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost.Clients;
using EasyPost.Interfaces;

namespace EasyPost.ApiCompatibility.Migration
{
    internal class MigrationSet
    {
        internal ApiVersionDetails FromApiVersion { get; }
        internal List<IMigrationEntry> Migrations { private get; set; } = new List<IMigrationEntry>();
        internal ApiVersionDetails ToApiVersion { get; }

        internal MigrationSet(ApiVersion fromApiVersion, ApiVersion toApiVersion)
        {
            FromApiVersion = ApiVersionDetails.GetApiVersionDetails(fromApiVersion);
            ToApiVersion = ApiVersionDetails.GetApiVersionDetails(toApiVersion);
        }

        internal MigrationResult? MigrateForwardIfNeeded(IMigratable obj)
        {
            IMigrationEntry? migrationEntry = GetMigrationEntryForGroup(obj.MigrationGroup);

            return migrationEntry?.MigrateForwards(obj);
        }

        private IMigrationEntry? GetMigrationEntryForGroup(MigrationGroup group)
        {
            return Migrations.FirstOrDefault(entry => entry.Group == group);
        }
    }

    internal interface IMigrationEntry
    {
        public Type FromType { get; }
        public MigrationGroup Group { get; }

        public Type ToType { get; }

        /// <summary>
        ///     Migrate a base-IMigratable object to a different type of object.
        /// </summary>
        /// <param name="obj">The base-IMigratable object to migrate.</param>
        /// <returns>A T-type object.</returns>
        public MigrationResult MigrateBackwards(IMigratable obj);

        /// <summary>
        ///     Migrate a base-IMigratable object to a different type of object.
        /// </summary>
        /// <param name="obj">The base-IMigratable object to migrate.</param>
        /// <returns>A T-type object.</returns>
        public MigrationResult MigrateForwards(IMigratable obj);
    }

    internal class MigrationResult
    {
        internal IMigratable NewObject { get; }

        internal Type NewObjectType { get; }
        internal IMigratable OldObject { get; }

        internal MigrationResult(IMigratable oldObject, IMigratable newObject, Type newObjectType)
        {
            OldObject = oldObject;
            NewObject = newObject;
            NewObjectType = newObjectType;
        }

        internal object Cast()
        {
            return Convert.ChangeType(NewObject, NewObjectType);
        }
    }
}
