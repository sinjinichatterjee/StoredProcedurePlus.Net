using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class ShortTypeConfiguration<S> : PrimitiveTypeConfiguration<S, short> where S : class
    {
        public ShortTypeConfiguration(Expression<Func<S, short>> memberSelector):base(memberSelector)
        {
        }
        internal override DbType GetDbType
        {
            get
            {
                return DbType.Int16;
            }
        }

        protected override short ValidateAndSet(short value)
        {
            if (AllowedMaxValue.HasValue && value > AllowedMaxValue) Error.MaxValuePropertyValidationError(PropertyName, value, AllowedMaxValue.Value);
            if (AllowedMinValue.HasValue && value < AllowedMinValue) Error.MinValuePropertyValidationError(PropertyName, value, AllowedMinValue.Value);

            if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
            {
                if (!Array.Exists<short>(AllowedValuesOnly, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
            }

            if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
            {
                if (Array.Exists<short>(AllowedValuesExcept, v => v.Equals(value)))
                    Error.ValueNotAllowedError(PropertyName, value, AllowedValuesExcept);
            }

            base.ValidateAndSet(value);
            return value;
        }

        public ShortTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public ShortTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        short? AllowedMaxValue = null;        
        public ShortTypeConfiguration<S> Max(short value)
        {
            AllowedMaxValue = value;
            return this;
        }

        short? AllowedMinValue = null;
        public ShortTypeConfiguration<S> Min(short value)
        {
            AllowedMinValue = value;
            return this;
        }

        short[] AllowedValuesOnly = null;
        public ShortTypeConfiguration<S> AllowedOnly(short[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        short[] AllowedValuesExcept = null;        
        public ShortTypeConfiguration<S> AllowedExcept(short[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }
    }
}
