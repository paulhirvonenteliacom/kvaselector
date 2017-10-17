namespace kvaselector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FavoriteNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FavoriteServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FavoriteNameId = c.Int(nullable: false),
                        ServiceTypeId = c.Int(nullable: false),
                        SelectedDiagnosisCodeId = c.String(),
                        SelectedTreatmentCodeId = c.String(),
                        SelectedCheckedTreatmentCodeId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceType = c.Int(nullable: false),
                        DiagnosisCodeRemark = c.String(),
                        NoOfDiagnosisCodes = c.Int(nullable: false),
                        TreatmentCodeRemark = c.String(),
                        NoOfTreatmentCodes = c.Int(nullable: false),
                        TreatmentTypeId = c.String(),
                        NoOfCheckedTreatmentLists = c.Int(nullable: false),
                        CheckedTreatmentTypeId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Treatments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TreatmentTypeId = c.Int(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TreatmentTypes", t => t.TreatmentTypeId, cascadeDelete: true)
                .Index(t => t.TreatmentTypeId);
            
            CreateTable(
                "dbo.TreatmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Treatments", "TreatmentTypeId", "dbo.TreatmentTypes");
            DropIndex("dbo.Treatments", new[] { "TreatmentTypeId" });
            DropTable("dbo.TreatmentTypes");
            DropTable("dbo.Treatments");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.Services");
            DropTable("dbo.FavoriteServices");
            DropTable("dbo.FavoriteNames");
            DropTable("dbo.Diagnosis");
        }
    }
}
