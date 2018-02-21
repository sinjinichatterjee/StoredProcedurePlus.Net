using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.EntityManagers;
using System;
using System.Collections.Generic;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers
{
    internal class EntityOrdinalConfiguration
    {
        readonly Dictionary<string, Tuple<int, PropertyConfiguration>> Index;

        protected EntityOrdinalConfiguration(List<PropertyConfiguration> Parameters, IDataEntityAdapter record)
        {
            if (Parameters == null) return;

            if (record == null) return;

            Index = new Dictionary<string, Tuple<int, PropertyConfiguration>>();

            for (int i = 0; i < Parameters.Count; i++)
            {
                Index.Add(Parameters[i].PropertyName, new Tuple<int, PropertyConfiguration>(record.GetOrdinal(Parameters[i].ParameterName), Parameters[i]));
            }
        }

        internal int this[string name]
        {
            get
            {
                return Index[name].Item1;
            }
        }

        internal PropertyConfiguration GetConfiguration(string name)
        {
            return Index[name].Item2;
        }
    }
}
