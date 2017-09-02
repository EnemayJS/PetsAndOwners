using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OAPets.Models
{
    public class OAPetsInitializer : SqliteCreateDatabaseIfNotExists<OAPetsContext>
    {
        public OAPetsInitializer(DbModelBuilder modelBuilder)
        : base(modelBuilder) { }

        protected override void Seed(OAPetsContext context)
        {
            context.Set<Owner>().Add(new Owner() { Id = 1, Name = "Homer Simpson" });
            context.Set<Owner>().Add(new Owner() { Id = 2, Name = "Billy Sad" });
            context.Set<Owner>().Add(new Owner() { Id = 3, Name = "David Cornel" });
            context.Set<Owner>().Add(new Owner() { Id = 4, Name = "Nicolas Penton" });
            context.Set<Owner>().Add(new Owner() { Id = 5, Name = "Martin Collman" });

            context.Set<Pet>().Add(new Pet() { Id = 1, Name = "Snowball", OwnerId = 1});
            context.Set<Pet>().Add(new Pet() { Id = 2, Name = "Santa's Helper", OwnerId = 1 });
            context.Set<Pet>().Add(new Pet() { Id = 3, Name = "T-rex", OwnerId = 1 });
            context.Set<Pet>().Add(new Pet() { Id = 4, Name = "Pony", OwnerId = 1 });
            context.Set<Pet>().Add(new Pet() { Id = 5, Name = "Killer", OwnerId = 2 });
            context.Set<Pet>().Add(new Pet() { Id = 6, Name = "Mike", OwnerId = 3 });
            context.Set<Pet>().Add(new Pet() { Id = 7, Name = "Corny", OwnerId = 4 });
            context.Set<Pet>().Add(new Pet() { Id = 8, Name = "Willy", OwnerId = 5 });

        }
    }
}