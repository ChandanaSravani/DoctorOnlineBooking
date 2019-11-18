namespace DoctorOnlineBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logins", "UserMail", c => c.String());
            DropColumn("dbo.Logins", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logins", "UserName", c => c.String());
            DropColumn("dbo.Logins", "UserMail");
        }
    }
}
