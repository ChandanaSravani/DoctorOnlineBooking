namespace DoctorOnlineBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewLoginDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logins", "SapId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logins", "SapId");
        }
    }
}
