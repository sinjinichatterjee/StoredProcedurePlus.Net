using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public class StringTypeConfiguration<S> : PrimitiveTypeConfiguration<S,string> where S : class
    {
        public StringTypeConfiguration(Expression<Func<S, string>> memberSelector):base(memberSelector)
        {

        }

        internal override DbType GetDbType
        {
            get
            {
                return DbType.String;
            }
        }

        protected override string ValidateAndSet(string value)
        {
            if (IsRequired && value==null) Error.RequiredPropertyValidationError(PropertyName);

            if (value != null)
            {
                int Length = value.Length;

                if (AllowedMaxLength.HasValue && Length > AllowedMaxLength) Error.MaxLengthPropertyValidationError(PropertyName, Length, AllowedMaxLength.Value);
                if (AllowedMinLength.HasValue && Length < AllowedMinLength) Error.MinLengthPropertyValidationError(PropertyName, Length, AllowedMinLength.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<string>(AllowedValuesOnly, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<string>(AllowedValuesExcept, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value, AllowedValuesExcept);
                }
                
                if(IsTrim)
                {
                    value = value.Trim();
                }
                else
                {
                    if(IsLTrim)
                    {
                        value = value.TrimStart();
                    }

                    if (IsRTrim)
                    {
                        value = value.TrimEnd();
                    }
                }
            }

            base.ValidateAndSet(value);
            return value;
        }

        public StringTypeConfiguration<S> Out()
        {
            this.IsOut = true;
            return this;
        }

        public StringTypeConfiguration<S> HasParameterName(string name)
        {
            this.ParameterName = name;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public StringTypeConfiguration<S> Required()
        {
            this.IsRequired = true;
            return this;
        }

        uint? AllowedMaxLength = null;
        public StringTypeConfiguration<S> MaxLength(uint value)
        {
            AllowedMaxLength = value;
            return this;
        }

        uint? AllowedMinLength = null;
        public StringTypeConfiguration<S> MinLength(uint value)
        {
            AllowedMinLength = value;
            return this;
        }

        string[] AllowedValuesOnly = null;
        public StringTypeConfiguration<S> AllowedOnly(string[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        string[] AllowedValuesExcept = null;
        public StringTypeConfiguration<S> AllowedExcept(string[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }

        internal bool IsPattern = false;
        public StringTypePatternConfiguration<S> HasPattern()
        {
            IsPattern = true;   
            return (StringTypePatternConfiguration<S>)this;
        }

        internal bool IsTrim = false;
        public StringTypeConfiguration<S> Trim()
        {
            IsTrim = true;
            return this;
        }

        internal bool IsLTrim = false;
        public StringTypeConfiguration<S> LTrim()
        {
            IsLTrim = true;
            return this;
        }

        internal bool IsRTrim = false;
        public StringTypeConfiguration<S> RTrim()
        {
            IsRTrim = true;
            return this;
        }
    }
}
