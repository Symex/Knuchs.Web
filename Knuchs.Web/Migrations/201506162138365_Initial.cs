namespace Knuchs.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        HasNewsletter = c.Boolean(nullable: false),
                        PictureLink = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        RefBlogEntry_Id = c.Int(),
                        RefUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogEntries", t => t.RefBlogEntry_Id)
                .ForeignKey("dbo.Users", t => t.RefUser_Id)
                .Index(t => t.RefBlogEntry_Id)
                .Index(t => t.RefUser_Id);
            
            CreateTable(
                "dbo.BlogEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkText = c.String(),
                        LinkUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "RefUser_Id" });
            DropIndex("dbo.Comments", new[] { "RefBlogEntry_Id" });
            DropForeignKey("dbo.Comments", "RefUser_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "RefBlogEntry_Id", "dbo.BlogEntries");
            DropTable("dbo.Links");
            DropTable("dbo.BlogEntries");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
        }
    }
}
