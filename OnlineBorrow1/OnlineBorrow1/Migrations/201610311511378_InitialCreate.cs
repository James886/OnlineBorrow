namespace OnlineBorrow1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.borrowInformations",
                c => new
                    {
                        information_id = c.Int(nullable: false, identity: true),
                        单位名称 = c.String(),
                        借用人 = c.String(),
                        联系电话 = c.String(),
                        借用人身份 = c.String(),
                        所在班级 = c.String(),
                        学号 = c.String(),
                        用途 = c.String(),
                        其它 = c.String(),
                        具体内容描述 = c.String(),
                        借用机房 = c.String(),
                        借用具体时间始 = c.String(),
                        借用具体时间终 = c.String(),
                        提交时间 = c.DateTime(nullable: false),
                        informationCategory = c.Int(nullable: false),
                        负责人意见 = c.String(),
                        系领导意见 = c.String(),
                        批准时间 = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.information_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.borrowInformations");
        }
    }
}
