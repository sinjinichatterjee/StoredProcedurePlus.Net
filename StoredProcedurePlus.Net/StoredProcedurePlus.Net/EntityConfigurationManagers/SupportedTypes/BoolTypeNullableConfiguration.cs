using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class BoolTypeNullableConfiguration<S> : PrimitiveTypeConfiguration<S, bool?> where S : class
    {
        public BoolTypeNullableConfiguration(Expression<Func<S, bool?>> memberSelector):base(memberSelector)
        {
        }
        internal override DbType GetDbType
        {
            get
            {
                return DbType.Boolean;
            }
        }

        protected override bool? ValidateAndSet(bool? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedValuesOnly != value)
                {
                    Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }
            }

            base.ValidateAndSet(value);
            return value;
        }

        public BoolTypeNullableConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public BoolTypeNullableConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public BoolTypeNullableConfiguration<S> Required()
        {
            this.IsRequired = true;
            return this;
        }

        bool AllowedValuesOnly = true;
        public BoolTypeNullableConfiguration<S> AllowedOnly(bool values)
        {
            AllowedValuesOnly = values;
            return this;
        }
    }
}
