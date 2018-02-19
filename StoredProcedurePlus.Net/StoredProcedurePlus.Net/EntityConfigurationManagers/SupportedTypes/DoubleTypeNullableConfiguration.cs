using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DoubleTypeNullableConfiguration<S> : PrimitiveTypeConfiguration<S, double?> where S : class
    {
        public DoubleTypeNullableConfiguration(Expression<Func<S, double?>> memberSelector):base(memberSelector)
        {
        }
        internal override DbType GetDbType
        {
            get
            {
                return DbType.Double;
            }
        }

        protected override double? ValidateAndSet(double? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value.Value, AllowedMaxValue.Value);
                if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value.Value, AllowedMinValue.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<double>(AllowedValuesOnly, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<double>(AllowedValuesExcept, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesExcept);
                }
            }

            base.ValidateAndSet(value);
            return value;
        }

        internal bool IsRequired { get; private set; }
        public DoubleTypeNullableConfiguration<S> Required()
        {
            this.IsRequired = true;
            return this;
        }

        public DoubleTypeNullableConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DoubleTypeNullableConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal byte? ScaleSize = null;
        internal byte? PrecisionSize = null;
        public DoubleTypeNullableConfiguration<S> HasSize(byte scale, byte precision)
        {
            ScaleSize = scale;
            PrecisionSize = precision;
            return this;
        }


        double? AllowedMaxValue = null;
        public DoubleTypeNullableConfiguration<S> Max(double value)
        {
            AllowedMaxValue = value;
            return this;
        }

        double? AllowedMinValue = null;
        public DoubleTypeNullableConfiguration<S> Min(double value)
        {
            AllowedMinValue = value;
            return this;
        }

        double[] AllowedValuesOnly = null;
        public DoubleTypeNullableConfiguration<S> AllowedOnly(double[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        double[] AllowedValuesExcept = null;
        public DoubleTypeNullableConfiguration<S> AllowedExcept(double[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
