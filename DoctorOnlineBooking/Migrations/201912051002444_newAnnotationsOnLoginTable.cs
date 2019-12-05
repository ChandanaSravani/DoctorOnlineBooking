namespace DoctorOnlineBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newAnnotationsOnLoginTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logins", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logins", "Password", c => c.String());
        }
    }
}
