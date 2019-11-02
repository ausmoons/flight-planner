namespace flight_planner.data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FlightPlannerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false; //vienmēr jābū false, lai būtu kontrole pār datubāzi
        }

        protected override void Seed(FlightPlannerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
