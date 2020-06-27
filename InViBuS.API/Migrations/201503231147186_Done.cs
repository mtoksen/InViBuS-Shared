namespace InViBuS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Done : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ProjectData.AnalysisData",
                c => new
                    {
                        IdAnalysisData = c.Int(nullable: false),
                        AnalysisDataBLOB = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.IdAnalysisData)
                .ForeignKey("ProjectData.AnalysisMetadata", t => t.IdAnalysisData)
                .Index(t => t.IdAnalysisData);
            
            CreateTable(
                "ProjectData.AnalysisMetadata",
                c => new
                    {
                        IdAnalysisMetadata = c.Int(nullable: false, identity: true),
                        IdProject = c.Int(nullable: false),
                        Iteration = c.Int(nullable: false),
                        NumSim = c.Int(nullable: false),
                        TimeSim = c.Int(nullable: false),
                        NumIn = c.Int(nullable: false),
                        NumOut = c.Int(nullable: false),
                        Subject = c.String(nullable: false),
                        UploadDate = c.DateTimeOffset(nullable: false, precision: 7),
                        DescriptionLong = c.String(),
                        Conclusion = c.String(nullable: false),
                        SourceFile = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdAnalysisMetadata)
                .ForeignKey("ProjectData.Projects", t => t.IdProject, cascadeDelete: true)
                .Index(t => t.IdProject);
            
            CreateTable(
                "ProjectData.Filters",
                c => new
                    {
                        IdFilter = c.Int(nullable: false, identity: true),
                        IdAnalysisMetadata = c.Int(nullable: false),
                        NumFilter = c.Int(nullable: false),
                        MinVal = c.Double(nullable: false),
                        MaxVal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdFilter)
                .ForeignKey("ProjectData.AnalysisMetadata", t => t.IdAnalysisMetadata, cascadeDelete: true)
                .Index(t => t.IdAnalysisMetadata);
            
            CreateTable(
                "ProjectData.Projects",
                c => new
                    {
                        IdProject = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        StartDate = c.DateTimeOffset(nullable: false, precision: 7),
                        LastUploadedDate = c.DateTimeOffset(nullable: false, precision: 7),
                        IdProjectManager = c.String(),
                    })
                .PrimaryKey(t => t.IdProject)
                .Index(t => t.ProjectName, unique: true);
            
            CreateTable(
                "ProjectData.SourceData",
                c => new
                    {
                        IdSourceData = c.Int(nullable: false, identity: true),
                        IdProject = c.Int(nullable: false),
                        SourceDataBLOB = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.IdSourceData)
                .ForeignKey("ProjectData.Projects", t => t.IdProject, cascadeDelete: true)
                .Index(t => t.IdProject);
            
            CreateTable(
                "UserData.Users",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 16),
                        Company = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(nullable: false, maxLength: 16),
                        ConfirmPasswordHash = c.String(nullable: false),
                        Id = c.String(),
                        Project_IdProject = c.Int(),
                    })
                .PrimaryKey(t => t.UserName)
                .ForeignKey("ProjectData.Projects", t => t.Project_IdProject)
                .Index(t => t.Project_IdProject);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("ProjectData.AnalysisData", "IdAnalysisData", "ProjectData.AnalysisMetadata");
            DropForeignKey("ProjectData.AnalysisMetadata", "IdProject", "ProjectData.Projects");
            DropForeignKey("UserData.Users", "Project_IdProject", "ProjectData.Projects");
            DropForeignKey("ProjectData.SourceData", "IdProject", "ProjectData.Projects");
            DropForeignKey("ProjectData.Filters", "IdAnalysisMetadata", "ProjectData.AnalysisMetadata");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("UserData.Users", new[] { "Project_IdProject" });
            DropIndex("ProjectData.SourceData", new[] { "IdProject" });
            DropIndex("ProjectData.Projects", new[] { "ProjectName" });
            DropIndex("ProjectData.Filters", new[] { "IdAnalysisMetadata" });
            DropIndex("ProjectData.AnalysisMetadata", new[] { "IdProject" });
            DropIndex("ProjectData.AnalysisData", new[] { "IdAnalysisData" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("UserData.Users");
            DropTable("ProjectData.SourceData");
            DropTable("ProjectData.Projects");
            DropTable("ProjectData.Filters");
            DropTable("ProjectData.AnalysisMetadata");
            DropTable("ProjectData.AnalysisData");
        }
    }
}
