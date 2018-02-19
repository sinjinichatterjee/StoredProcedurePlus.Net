using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public sealed class DateTimeTypeConfiguration<S> : PrimitiveTypeConfiguration<S, DateTime> where S : class
    {
        public DateTimeTypeConfiguration(Expression<Func<S, DateTime>> memberSelector):base(memberSelector)
        {
        }

        internal override DbType GetDbType
        {
            get
            {
                return DbType.DateTime2;
            }
        }

        protected override DateTime ValidateAndSet(DateTime value)
        {
            if (AllowedMaxDate.HasValue && value > AllowedMaxDate) Error.MaxDatePropertyValidationError(PropertyName, value, AllowedMaxDate.Value);
            if (AllowedMinDate.HasValue && value < AllowedMinDate) Error.MinDatePropertyValidationError(PropertyName, value, AllowedMinDate.Value);

            if(AllowedDatesOnly!=null && AllowedDatesOnly.Length > 0)
            {
                if(!Array.Exists<DateTime>(AllowedDatesOnly, v => v.Equals(value)))
                    Error.DateNotAllowedError(PropertyName, value, AllowedDatesOnly);
            }

            if (AllowedDatesExcept != null && AllowedDatesExcept.Length > 0)
            {
                if (Array.Exists<DateTime>(AllowedDatesExcept, v => v.Equals(value)))
                    Error.DateNotAllowedError(PropertyName, value, AllowedDatesExcept);
            }

            base.ValidateAndSet(value);
            return value;
        }

        public DateTimeTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public DateTimeTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }


        DateTime? AllowedMaxDate = null;
        public DateTimeTypeConfiguration<S> Max(DateTime value)
        {
            AllowedMaxDate = value;
            return this;
        }

        DateTime? AllowedMinDate = null;
        public DateTimeTypeConfiguration<S> Min(DateTime value)
        {
            AllowedMinDate = value;
            return this;
        }

        DateTime[] AllowedDatesOnly = null;
        public DateTimeTypeConfiguration<S> AllowedOnly(DateTime[] values)
        {
            AllowedDatesOnly = values;
            return this;
        }

        DateTime[] AllowedDatesExcept = null;
        public DateTimeTypeConfiguration<S> AllowedExcept(DateTime[] values)
        {
            AllowedDatesExcept = values;
            return this;
        }

        string AllowedDateTimeFormat = null;
        public DateTimeTypeConfiguration<S> Format(string value)
        {
            AllowedDateTimeFormat = value;
            return this;
        }
    }
}
