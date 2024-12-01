using System;
using System.Data.Entity;
using System.Linq;
using Assignment2_MooreJoshua.Models;

namespace Assignment2_MooreJoshua
{
    public class HarryRosenWatchesDB : DbContext
    {
        // Your context has been configured to use a 'HarryRosen' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Assignment2_MooreJoshua.HarryRosen' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'HarryRosen' 
        // connection string in the application configuration file.
        public HarryRosenWatchesDB()
            : base()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<HarryRosenWatchesDB>());



        }

        public DbSet<Item> Items { get; set; }
    }
}
