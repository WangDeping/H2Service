namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhomepagecol_kb : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.HomePages", "RYKBMC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "ZKKBMC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "ZKKB1MC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "ZKKB2MC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "CYKBMC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "QKDJ1MC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "QKDJ2MC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "QKDJ3MC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "QKDJ4MC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "QKDJ5MC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "QKDJ6MC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "QKDJ7MC", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "QKDJ8MC", c => c.String(unicode: false));
            //DropColumn("dbo.HomePages", "_RYKB");
            //DropColumn("dbo.HomePages", "_ZKKB");
            //DropColumn("dbo.HomePages", "_ZKKB1");
            //DropColumn("dbo.HomePages", "_ZKKB2");
            //DropColumn("dbo.HomePages", "_CYKB");
            //DropColumn("dbo.HomePages", "_QKDJ1");
            //DropColumn("dbo.HomePages", "_QKDJ2");
            //DropColumn("dbo.HomePages", "_QKDJ3");
            //DropColumn("dbo.HomePages", "_QKDJ4");
            //DropColumn("dbo.HomePages", "_QKDJ5");
            //DropColumn("dbo.HomePages", "_QKDJ6");
            //DropColumn("dbo.HomePages", "_QKDJ7");
            //DropColumn("dbo.HomePages", "_QKDJ8");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HomePages", "_QKDJ8", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_QKDJ7", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_QKDJ6", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_QKDJ5", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_QKDJ4", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_QKDJ3", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_QKDJ2", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_QKDJ1", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_CYKB", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_ZKKB2", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_ZKKB1", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_ZKKB", c => c.String(unicode: false));
            AddColumn("dbo.HomePages", "_RYKB", c => c.String(unicode: false));
            DropColumn("dbo.HomePages", "QKDJ8MC");
            DropColumn("dbo.HomePages", "QKDJ7MC");
            DropColumn("dbo.HomePages", "QKDJ6MC");
            DropColumn("dbo.HomePages", "QKDJ5MC");
            DropColumn("dbo.HomePages", "QKDJ4MC");
            DropColumn("dbo.HomePages", "QKDJ3MC");
            DropColumn("dbo.HomePages", "QKDJ2MC");
            DropColumn("dbo.HomePages", "QKDJ1MC");
            DropColumn("dbo.HomePages", "CYKBMC");
            DropColumn("dbo.HomePages", "ZKKB2MC");
            DropColumn("dbo.HomePages", "ZKKB1MC");
            DropColumn("dbo.HomePages", "ZKKBMC");
            DropColumn("dbo.HomePages", "RYKBMC");
        }
    }
}
