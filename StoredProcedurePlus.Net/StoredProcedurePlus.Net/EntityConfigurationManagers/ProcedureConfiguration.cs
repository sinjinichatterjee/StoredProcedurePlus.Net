﻿using StoredProcedurePlus.Net.ConnectionManagers;
using System;
using System.Collections.Generic;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public sealed class ProcedureConfiguration<S> where S : class
    {
        public string ConnectionStringName { get; set; }

        public string ConnectionString { get; set; }

        public string ProcedureName { get; set; }
      
        internal readonly ConnectionFactory Connection;

        internal List<NonPrimitiveEntityConfiguration> OutputSets;

        public EntityConfiguration<S> Input;
        
        public EntityConfiguration<T> CanReturn<T>() where T : class
        {
            EntityConfiguration<T> ReturnEntity = new EntityConfiguration<T>();
            OutputSets.Add(ReturnEntity);
            return ReturnEntity;
        }

        //public void CanReturn<int>()
        //{
        //    //EntityConfiguration<T> ReturnEntity = new EntityConfiguration<T>();
        //    //OutputSets.Add(ReturnEntity);
        //    //return ReturnEntity;


        //}

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