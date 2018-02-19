using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class LongTypeConfiguration<S> : PrimitiveTypeConfiguration<S, long> where S : class
    {
        public LongTypeConfiguration(Expression<Func<S, long>> memberSelector):base(memberSelector)
        {
        }
        internal override DbType GetDbType
        {
            get
            {
                return DbType.Int64;
            }
        }

        protected override long ValidateAndSet(long value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value, AllowedMaxValue.Value);
            if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value, AllowedMinValue.Value);

            if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
            {
                if (!Array.Exists<long>(AllowedValuesOnly, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
            }

            if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
            {
                if (Array.Exists<long>(AllowedValuesExcept, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesExcept);
            }

            base.ValidateAndSet(value);
            return value;
        }

        public LongTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public LongTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        long? AllowedMaxValue = null;        
        public LongTypeConfiguration<S> Max(long value)
        {
            AllowedMaxValue = value;
            return this;
        }

        long? AllowedMinValue = null;
        public LongTypeConfiguration<S> Min(long value)
        {
            AllowedMinValue = value;
            return this;
        }

        long[] AllowedValuesOnly = null;
        public LongTypeConfiguration<S> AllowedOnly(long[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        long[] AllowedValuesExcept = null;        
        public LongTypeConfiguration<S> AllowedExcept(long[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
