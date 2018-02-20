using StoredProcedurePlus.Net.StoredProcedureManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures
{
    public class SPSelectAdjustmentsParams
    {
    }

    public class Steven_Singer_ADJUSTMENTS
    {
        public string SKU_NO { get; set; }
        public double? Adjust { get; set; }
        public string SPECIAL { get; set; }
        public int? CurrRapp { get; set; }
        public double? WEIGHT { get; set; }
        public string COLOR { get; set; }
        public string CLARITY { get; set; }
    }

    public class SPSelectAdjustments : StoredProcedureManager<SPSelectAdjustmentsParams>
    {
        protected override void Setup(ProcedureConfiguration<SPSelectAdjustmentsParams> configuration)
        {
            configuration.ConnectionString = "Data Source=.;Initial Catalog=Steven_Singer_db;Integrated Security=True;";
            configuration.ProcedureName = "SPSelectAdjustments";
            configuration.CanReturnCollectionOf<Steven_Singer_ADJUSTMENTS>().Maps(v => v.SKU_NO).Email();
        }
    }
}
