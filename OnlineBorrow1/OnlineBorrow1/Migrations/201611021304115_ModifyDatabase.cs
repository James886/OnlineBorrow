namespace OnlineBorrow1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.borrowInformations", "user_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.borrowInformations", "user_id");
        }
    }
}
