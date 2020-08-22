namespace SBTC.Functions.Patients.DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesexfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("tc.Pacientes", "Genero", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            DropColumn("tc.Pacientes", "Sexo");
        }
        
        public override void Down()
        {
            AddColumn("tc.Pacientes", "Sexo", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            DropColumn("tc.Pacientes", "Genero");
        }
    }
}
