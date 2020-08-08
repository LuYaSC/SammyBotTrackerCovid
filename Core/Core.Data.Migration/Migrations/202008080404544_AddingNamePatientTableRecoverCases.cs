namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNamePatientTableRecoverCases : DbMigration
    {
        public override void Up()
        {
            AddColumn("doctor.CasosRecuperados", "NombrePaciente", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            DropColumn("doctor.CasosRecuperados", "NombrePaciente");
        }
    }
}
