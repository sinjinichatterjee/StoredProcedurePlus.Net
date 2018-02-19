using StoredProcedurePlus.Net.StoredProcedureManagers;

namespace StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures
{
    public class MyFirstStoredProcedure:StoredProcedureManager<School>
    {
        protected override void Setup(ProcedureConfiguration<School> configuration)
        {
            configuration.ProcedureName = "SomeSPName";
            configuration.Input.Maps(v => v.SchoolId).Max(2).HasParameterName("Hamba");
            configuration.Input.Maps(v => v.SchoolName).Required();

            //configuration.Output<School>.Maps(v => v.SchoolName).Required();
        }
    }
}
