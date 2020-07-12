namespace SBTC.Functions.Patients.DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingEmpresainPaciente : DbMigration
    {
        public override void Up()
        {
            AddColumn("tc.Pacientes", "EmpresaTrabaja", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("tc.Pacientes", "EmpresaTrabaja");
        }
    }
}
