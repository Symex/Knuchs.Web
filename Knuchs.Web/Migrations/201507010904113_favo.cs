namespace Knuchs.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefFavBlog_Id = c.Int(),
                        RefFavUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogEntries", t => t.RefFavBlog_Id)
                .ForeignKey("dbo.Users", t => t.RefFavUser_Id)
                .Index(t => t.RefFavBlog_Id)
                .Index(t => t.RefFavUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favourites", "RefFavUser_Id", "dbo.Users");
            DropForeignKey("dbo.Favourites", "RefFavBlog_Id", "dbo.BlogEntries");
            DropIndex("dbo.Favourites", new[] { "RefFavUser_Id" });
            DropIndex("dbo.Favourites", new[] { "RefFavBlog_Id" });
            DropTable("dbo.Favourites");
        }
    }
}
