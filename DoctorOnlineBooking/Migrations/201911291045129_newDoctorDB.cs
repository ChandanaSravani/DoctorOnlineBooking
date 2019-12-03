namespace DoctorOnlineBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newDoctorDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Start_Time_M", c => c.String());
            AddColumn("dbo.Doctors", "End_Time_M", c => c.String());
            AddColumn("dbo.Doctors", "Start_Time_E", c => c.String());
            AddColumn("dbo.Doctors", "End_Time_E", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "End_Time_E");
            DropColumn("dbo.Doctors", "Start_Time_E");
            DropColumn("dbo.Doctors", "End_Time_M");
            DropColumn("dbo.Doctors", "Start_Time_M");
        }
    }
}
