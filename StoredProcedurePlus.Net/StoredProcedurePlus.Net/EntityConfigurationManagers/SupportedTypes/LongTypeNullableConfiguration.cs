using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class LongTypeNullableConfiguration<S> : PrimitiveTypeConfiguration<S, long?> where S : class
    {
        public LongTypeNullableConfiguration(Expression<Func<S, long?>> memberSelector):base(memberSelector)
        {
        }
        internal override DbType GetDbType
        {
            get
            {
                return DbType.Int64;
            }
        }

        protected override long? ValidateAndSet(long? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value.Value, AllowedMaxValue.Value);
                if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value.Value, AllowedMinValue.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<long>(AllowedValuesOnly, v => v.Equals(value.Value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<long>(AllowedValuesExcept, v => v.Equals(value.Value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesExcept);
                }
            }

            base.ValidateAndSet(value);
            return value;
        }

        public LongTypeNullableConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public LongTypeNullableConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public LongTypeNullableConfiguration<S> Required()
        {
            this.IsRequired = true;
            return this;
        }


        long? AllowedMaxValue = null;        
        public LongTypeNullableConfiguration<S> Max(long value)
        {
            AllowedMaxValue = value;
            return this;
        }

        long? AllowedMinValue = null;
        public LongTypeNullableConfiguration<S> Min(long value)
        {
            AllowedMinValue = value;
            return this;
        }

        long[] AllowedValuesOnly = null;
        public LongTypeNullableConfiguration<S> AllowedOnly(long[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        long[] AllowedValuesExcept = null;        

        public LongTypeNullableConfiguration<S> AllowedExcept(long[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
