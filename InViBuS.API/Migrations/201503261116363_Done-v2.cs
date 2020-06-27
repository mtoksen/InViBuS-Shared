namespace InViBuS.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Donev2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("UserData.Users", "Password", c => c.String(nullable: false, maxLength: 16));
            DropColumn("UserData.Users", "PasswordHash");
            DropColumn("UserData.Users", "ConfirmPasswordHash");
        }
        
        public override void Down()
        {
            AddColumn("UserData.Users", "ConfirmPasswordHash", c => c.String(nullable: false));
            AddColumn("UserData.Users", "PasswordHash", c => c.String(nullable: false, maxLength: 16));
            DropColumn("UserData.Users", "Password");
        }
    }
}
