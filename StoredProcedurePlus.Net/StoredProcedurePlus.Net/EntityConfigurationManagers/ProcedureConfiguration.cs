using StoredProcedurePlus.Net.ConnectionManagers;
using System.Collections.Generic;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{


    public sealed class ProcedureConfiguration<S> where S : class, new ()
    {
        public string ConnectionStringName { get; set; }
        public string ConnectionString { get; set; }
        public string ProcedureName { get; set; }
        public bool Mock { get; set; }

        internal readonly ConnectionFactory Connection;

        internal List<NonPrimitiveEntityConfiguration> OutputSets;

        public EntityConfiguration<S> Input;
        
        public EntityConfiguration<T> CanReturnCollectionOf<T>() where T : class, new ()
        {
            EntityConfiguration<T> ReturnEntity = new EntityConfiguration<T>();
            OutputSets.Add(ReturnEntity);
            return ReturnEntity;
        }

        public ProcedureConfiguration()
        {
            ConnectionStringName = "DbString"; // Default
            Input = new EntityConfiguration<S>();
            OutputSets = new List<NonPrimitiveEntityConfiguration>();
            Connection = new ConnectionFactory();
        }

        internal void Initialize()
        {
            Input.Initialize();

            for(int i = 0; i < OutputSets.Count; i++ )
            {
                OutputSets[i].Initialize();
            }
        }
    }
}