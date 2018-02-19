using StoredProcedurePlus.Net.EntityManagers;
using StoredProcedurePlus.Net.StoredProcedureManagers;

namespace StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures
{
    public class MockSp : StoredProcedureManager<AllTypeParams>
    {
        protected override void Setup(ProcedureConfiguration<AllTypeParams> configuration)
        {
            configuration.Mock = true;
            configuration.Input.Maps(v => v.Id).Min(1);
            configuration.Input.Maps(v => v.RowChanged).Out();
        }
    }
}
