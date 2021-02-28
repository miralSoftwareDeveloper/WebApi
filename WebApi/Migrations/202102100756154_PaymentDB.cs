namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreditCardNumber = c.String(nullable: false),
                        CardHolder = c.String(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        SecurityCode = c.String(maxLength: 3),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PaymentId = c.Int(nullable: false),
                        StatusName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: true)
                .Index(t => t.PaymentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentStatus", "PaymentId", "dbo.Payments");
            DropIndex("dbo.PaymentStatus", new[] { "PaymentId" });
            DropTable("dbo.PaymentStatus");
            DropTable("dbo.Payments");
        }
    }
}
