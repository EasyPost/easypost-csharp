using System;
using EasyPost.ApiCompatibility.Migration;
using EasyPost.Interfaces;
using EasyPost.Models.V2;
using Address = EasyPost.Models.Base.Address;

namespace EasyPost.Models.Migrations.V2
{
    internal class SampleMigration : IMigrationEntry
    {
        public Type FromType => typeof(Address);

        public MigrationGroup Group => MigrationGroup.Sample;

        public Type ToType => typeof(User);

        public MigrationResult MigrateBackwards(IMigratable obj) => throw new NotImplementedException();

        public MigrationResult MigrateForwards(IMigratable obj)
        {
            var oldObject = new Address();
            var newObject = new User();
            return new MigrationResult(oldObject, newObject, typeof(Address));
        }
    }
}
