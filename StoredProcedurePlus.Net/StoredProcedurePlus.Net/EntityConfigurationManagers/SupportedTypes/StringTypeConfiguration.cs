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
                if (StringPattern != null && StringPattern.Length > 0)
                {
                    try
                    {
                        bool IsMatch = Regex.IsMatch(
                            value,
                            StringPattern,
                            Pattern_Option,
                            TimeSpan.FromMilliseconds(PaternTimeOut)
                        );

                        if (!IsMatch)
                        {
                            Error.PatternMathcingFailed(PropertyName, value, StringPattern);
                        }
                    }
                    catch (RegexMatchTimeoutException)
                    {
                        Error.PatternMathcingTimeOut(PropertyName, value, StringPattern, PaternTimeOut);
                    }
                    catch (Exception ex)
                    {
                        if (DefinedPattern == PreDefinedPatterns.None)
                        {
                            Error.PatternMatchingError(PropertyName, value, StringPattern, ex);
                        }
                        else
                        {
                            Error.PatternMatchingError(PropertyName, value, DefinedPattern.ToString());
                        }
                    }
                }

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

        #region Pattern validation

        string StringPattern = null;
        public StringTypeConfiguration<S> Pattern(string regX)
        {
            StringPattern = regX;
            return this;
        }

        double PaternTimeOut = 200; //Default
        public StringTypeConfiguration<S> PatternMatchingTimeout(double timeout)
        {
            PaternTimeOut = timeout;
            return this;
        }

        RegexOptions Pattern_Option = RegexOptions.IgnoreCase; //Default
        public StringTypeConfiguration<S> PatternOption(RegexOptions option)
        {
            Pattern_Option = option;
            return this;
        }

        enum PreDefinedPatterns
        {
            None,
            Email,
            Ftp,
            Website
        }

        PreDefinedPatterns DefinedPattern = PreDefinedPatterns.None;

        public StringTypeConfiguration<S> Email()
        {
            DefinedPattern = PreDefinedPatterns.Email;
            StringPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            return this;
        }

        public StringTypeConfiguration<S> WebSite()
        {
            DefinedPattern = PreDefinedPatterns.Website;
            StringPattern = @"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            return this;
        }

        public StringTypeConfiguration<S> FtpSite()
        {
            DefinedPattern = PreDefinedPatterns.Ftp;
            StringPattern = @"(ftp):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            return this;
        }
        #endregion
    }
}
