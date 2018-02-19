using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class ShortTypeNullableConfiguration<S> : PrimitiveTypeConfiguration<S, short?> where S : class
    {
        public ShortTypeNullableConfiguration(Expression<Func<S, short?>> memberSelector):base(memberSelector)
        {
        }
        internal override DbType GetDbType
        {
            get
            {
                return DbType.Int16;
            }
        }

        protected override short? ValidateAndSet(short? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value.Value, AllowedMaxValue.Value);
                if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value.Value, AllowedMinValue.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<short>(AllowedValuesOnly, v => v.Equals(value.Value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<short>(AllowedValuesExcept, v => v.Equals(value.Value)))
                        Error.ValueNotAllowedError(PropertyName, value.Value, AllowedValuesExcept);
                }
            }

            base.ValidateAndSet(value);
            return value;
        }

        public ShortTypeNullableConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public ShortTypeNullableConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public ShortTypeNullableConfiguration<S> Required()
        {
            this.IsRequired = true;
            return this;
        }


        short? AllowedMaxValue = null;        
        public ShortTypeNullableConfiguration<S> Max(short value)
        {
            AllowedMaxValue = value;
            return this;
        }

        short? AllowedMinValue = null;
        public ShortTypeNullableConfiguration<S> Min(short value)
        {
            AllowedMinValue = value;
            return this;
        }

        short[] AllowedValuesOnly = null;
        public ShortTypeNullableConfiguration<S> AllowedOnly(short[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        short[] AllowedValuesExcept = null;        
        public ShortTypeNullableConfiguration<S> AllowedExcept(short[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
