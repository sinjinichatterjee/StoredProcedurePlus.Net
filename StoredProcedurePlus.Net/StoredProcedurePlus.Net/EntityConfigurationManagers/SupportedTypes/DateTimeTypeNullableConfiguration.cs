using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DateTimeTypeNullableConfiguration<S> : PrimitiveTypeConfiguration<S, DateTime?> where S : class
    {
        public DateTimeTypeNullableConfiguration(Expression<Func<S, DateTime?>> memberSelector):base(memberSelector)
        {
        }

        internal override DbType GetDbType
        {
            get
            {
                return DbType.DateTime2;
            }
        }

        protected override DateTime? ValidateAndSet(DateTime? value)
        {
            if (IsRequired && !value.HasValue) Error.RequiredPropertyValidationError(PropertyName);

            if (value.HasValue)
            {
                if (AllowedMaxDate.HasValue && value > AllowedMaxDate) Error.MaxDatePropertyValidationError(PropertyName, value.Value, AllowedMaxDate.Value);
                if (AllowedMinDate.HasValue && value < AllowedMinDate) Error.MinDatePropertyValidationError(PropertyName, value.Value, AllowedMinDate.Value);

                if (AllowedDatesOnly != null && AllowedDatesOnly.Length > 0)
                {
                    if (!Array.Exists<DateTime>(AllowedDatesOnly, v => v.Equals(value)))
                        Error.DateNotAllowedError(PropertyName, value.Value, AllowedDatesOnly);
                }

                if (AllowedDatesExcept != null && AllowedDatesExcept.Length > 0)
                {
                    if (Array.Exists<DateTime>(AllowedDatesExcept, v => v.Equals(value)))
                        Error.DateNotAllowedError(PropertyName, value.Value, AllowedDatesExcept);
                }
            }

            base.ValidateAndSet(value);
            return value;
        }

        public DateTimeTypeNullableConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DateTimeTypeNullableConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public DateTimeTypeNullableConfiguration<S> Required()
        {
            this.IsRequired = true;
            return this;
        }

        DateTime? AllowedMaxDate = null;
        public DateTimeTypeNullableConfiguration<S> Max(DateTime value)
        {
            AllowedMaxDate = value;
            return this;
        }

        DateTime? AllowedMinDate = null;
        public DateTimeTypeNullableConfiguration<S> Min(DateTime value)
        {
            AllowedMinDate = value;
            return this;
        }

        DateTime[] AllowedDatesOnly = null;
        public DateTimeTypeNullableConfiguration<S> AllowedOnly(DateTime[] values)
        {
            AllowedDatesOnly = values;
            return this;
        }

        DateTime[] AllowedDatesExcept = null;
        public DateTimeTypeNullableConfiguration<S> AllowedExcept(DateTime[] values)
        {
            AllowedDatesExcept = values;
            return this;
        }

        string AllowedDateTimeFormat = null;
        public DateTimeTypeNullableConfiguration<S> Format(string value)
        {
            AllowedDateTimeFormat = value;
            return this;
        }
    }
}
