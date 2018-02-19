using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DecimalTypeNullableConfiguration<S> : PrimitiveTypeConfiguration<S, decimal?> where S : class
    {
        public DecimalTypeNullableConfiguration(Expression<Func<S, decimal?>> memberSelector):base(memberSelector)
        {
        }
        internal override DbType GetDbType
        {
            get
            {
                return DbType.Decimal;
            }
        }

        protected override decimal? ValidateAndSet(decimal? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value.Value, AllowedMaxValue.Value);
                if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value.Value, AllowedMinValue.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<decimal>(AllowedValuesOnly, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<decimal>(AllowedValuesExcept, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesExcept);
                }
            }

            base.ValidateAndSet(value);
            return value;
        }

        internal bool IsRequired { get; private set; }
        public DecimalTypeNullableConfiguration<S> Required()
        {
            this.IsRequired = true;
            return this;
        }

        public DecimalTypeNullableConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DecimalTypeNullableConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal byte? ScaleSize = null;
        internal byte? PrecisionSize = null;
        public DecimalTypeNullableConfiguration<S> HasSize(byte scale, byte precision)
        {
            ScaleSize = scale;
            PrecisionSize = precision;
            return this;
        }


        decimal? AllowedMaxValue = null;
        public DecimalTypeNullableConfiguration<S> Max(decimal value)
        {
            AllowedMaxValue = value;
            return this;
        }

        decimal? AllowedMinValue = null;
        public DecimalTypeNullableConfiguration<S> Min(decimal value)
        {
            AllowedMinValue = value;
            return this;
        }

        decimal[] AllowedValuesOnly = null;
        public DecimalTypeNullableConfiguration<S> AllowedOnly(decimal[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        decimal[] AllowedValuesExcept = null;
        public DecimalTypeNullableConfiguration<S> AllowedExcept(decimal[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
