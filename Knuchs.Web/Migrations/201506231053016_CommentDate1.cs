namespace Knuchs.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentDate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Title");
        }
    }
}
