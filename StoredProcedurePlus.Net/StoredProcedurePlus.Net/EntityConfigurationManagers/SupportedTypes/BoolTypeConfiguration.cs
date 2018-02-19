using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class BoolTypeConfiguration<S> : PrimitiveTypeConfiguration<S, bool> where S : class
    {
        public BoolTypeConfiguration(Expression<Func<S, bool>> memberSelector):base(memberSelector)
        {
        }
        internal override DbType GetDbType
        {
            get
            {
                return DbType.Boolean;
            }
        }

        protected override bool ValidateAndSet(bool value)
        {
            if (AllowedValuesOnly != value)
            {
                Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
            }

            base.ValidateAndSet(value);
            return value;
        }

        public BoolTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public BoolTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        bool AllowedValuesOnly = true;
        public BoolTypeConfiguration<S> AllowedOnly(bool values)
        {
            AllowedValuesOnly = values;
            return this;
        }
    }
}
