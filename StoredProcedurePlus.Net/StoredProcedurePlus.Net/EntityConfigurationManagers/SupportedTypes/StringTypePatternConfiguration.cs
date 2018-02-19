using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public class StringTypePatternConfiguration<S> : StringTypeConfiguration<S> where S : class
    {
        public StringTypePatternConfiguration(Expression<Func<S, string>> memberSelector):base(memberSelector)
        {

        }

        protected override string ValidateAndSet(string value)
        {
          if (value != null)
            {
                if (StringPattern != null && StringPattern.Length > 0)
                {
                    try
                    {
                        bool IsMatch = Regex.IsMatch(
                            value, 
                            StringPattern,
                            PatternOption, 
                            TimeSpan.FromMilliseconds(PaternTimeOut)
                        );

                        if(!IsMatch)
                        {
                            Error.PatternMathcingFailed(PropertyName, value, StringPattern);
                        }
                    }
                    catch (RegexMatchTimeoutException)
                    {
                        Error.PatternMathcingTimeOut(PropertyName, value, StringPattern, PaternTimeOut);
                    }
                    catch(Exception ex)
                    {
                        Error.PatternMatchingError(PropertyName, value, StringPattern, ex);
                    }
                }
            }

            base.ValidateAndSet(value);
            return value;
        }


        string StringPattern=null;
        public StringTypePatternConfiguration<S> Pattern(string regX)
        {
            StringPattern = regX;
            return this;
        }

        double PaternTimeOut = 200; //Default
        public StringTypePatternConfiguration<S> Timeout(double timeout)
        {
            PaternTimeOut = timeout;
            return this;
        }

        RegexOptions PatternOption = RegexOptions.IgnoreCase; //Default
        public StringTypePatternConfiguration<S> Option(RegexOptions option)
        {
            PatternOption = option;
            return this;
        }

        public StringTypePatternConfiguration<S> Email()
        {
            StringPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            return this;
        }

        public StringTypePatternConfiguration<S> WebSite()
        {
            StringPattern = @"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            return this;
        }

        public StringTypePatternConfiguration<S> FtpSite()
        {
            StringPattern = @"(ftp):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            return this;
        }
    }
}
