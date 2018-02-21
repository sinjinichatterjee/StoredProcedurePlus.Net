using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static Exception RequiredPropertyValidationError(string propertyName)
        {
            throw new ArgumentException("Value cannot be null", propertyName);
        }    

        internal static Exception MaxLengthPropertyValidationError(string propertyName, int actualLength, uint allowedLength)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has length {1} but the maximum allowed length is {2}", 
                propertyName, actualLength, allowedLength));
        }

        internal static Exception MinLengthPropertyValidationError(string propertyName, int actualLength, uint allowedLength)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has length {1} but the minimum allowed length is {2}",
                propertyName, actualLength, allowedLength));
        }

        internal static void ValueNotAllowedError(string propertyName, string value, string[] allowedValuesOnly)
        {
            throw new InvalidOperationException("Cannot set given value " + value + " into property : " + propertyName + " where allowed values are only " + string.Join(", ", allowedValuesOnly));
        }

        internal static void PatternMathcingFailed(string propertyName, string value, string stringPattern)
        {
            throw new InvalidOperationException("Cannot set given value " + value + " into property : " + propertyName + " where allowed pattern is only " + stringPattern);
        }

        internal static void PatternMathcingTimeOut(string propertyName, string value, string stringPattern, double paternTimeOut)
        {
            throw new InvalidOperationException(
                "Cannot set given value " + value 
                + " into property : "
                + propertyName
                + " Pattern matching timed out for given timeout " 
                + paternTimeOut 
                + "ms. against the pattern : " 
                + stringPattern);
        }

        internal static void PatternMatchingError(string propertyName, string value, string stringPattern, Exception ex)
        {
            throw new InvalidOperationException(
                "Cannot set given value " + value
                + " into property : "
                + propertyName
                + " Pattern matching error "
                + ex.Message
                + ", against the pattern : "
                + stringPattern);
        }

        internal static void PatternMatchingError(string propertyName, string value, string patternName)
        {
            throw new InvalidOperationException(
                "Cannot set given value " + value
                + " into property : "
                + propertyName
                + ", against the pattern : "
                + patternName);
        }
    }
}
