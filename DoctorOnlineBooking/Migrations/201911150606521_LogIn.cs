namespace DoctorOnlineBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogIn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logins", "UserName", c => c.String());
            DropColumn("dbo.Logins", "UserMail");
            DropColumn("dbo.Logins", "SapId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logins", "SapId", c => c.Long(nullable: false));
            AddColumn("dbo.Logins", "UserMail", c => c.String());
            DropColumn("dbo.Logins", "UserName");
        }
    }
}
