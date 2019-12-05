namespace DoctorOnlineBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newAnnotationsOnPatientData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientDatas", "PatientName", c => c.String(nullable: false));
            AlterColumn("dbo.PatientDatas", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.PatientDatas", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.PatientDatas", "City", c => c.String(nullable: false));
            AlterColumn("dbo.PatientDatas", "State", c => c.String(nullable: false));
            AlterColumn("dbo.PatientDatas", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.PatientDatas", "Gender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientDatas", "Gender", c => c.String());
            AlterColumn("dbo.PatientDatas", "Country", c => c.String());
            AlterColumn("dbo.PatientDatas", "State", c => c.String());
            AlterColumn("dbo.PatientDatas", "City", c => c.String());
            AlterColumn("dbo.PatientDatas", "Address", c => c.String());
            AlterColumn("dbo.PatientDatas", "PhoneNumber", c => c.String());
            AlterColumn("dbo.PatientDatas", "PatientName", c => c.String());
        }
    }
}
