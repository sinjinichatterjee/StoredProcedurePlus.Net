using StoredProcedurePlus.Net.StoredProcedureManagers;

namespace StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures
{
    public class SpResourceSummary : StoredProcedureManager<ResourceSummary>
    {
        protected override void Setup(ProcedureConfiguration<ResourceSummary> configuration)
        {
            configuration.Mock = true;
            configuration.ProcedureName = "InsertResourceSummary";
            //configuration.ConnectionString = "Data Source=PIS03CDIVDISS33;Initial Catalog=PerformanceTestDb;Integrated Security=True;Connection Timeout=60;Min Pool Size=20; Max Pool Size=500;";
            configuration.Input.Maps(v => v.PersonId).Out();
            configuration.Input.Maps(v => v.PersonName).Required().MinLength(1).MaxLength(50);
            configuration.Input.Maps(v => v.EmailAddress).Required().MaxLength(50);
            configuration.Input.Maps(v => v.MobileNo).Required().MaxLength(50);
            configuration.Input.Maps(v => v.Country).MaxLength(50);
        }
    }
}
