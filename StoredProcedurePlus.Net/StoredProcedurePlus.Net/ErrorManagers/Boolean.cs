using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        #region MaxValuePropertyValidationError

        #endregion

        #region MinValuePropertyValidationError

        #endregion

        #region CannotSetNullToNotNullableIntProperty
        internal static void CannotSetNullToNotNullableBooleanProperty(string propertyName)
        {
            throw new InvalidOperationException("Cannot set null value into not nullable boolean property : " + propertyName);
        }
        #endregion

        #region ValueNotAllowedError
        internal static void ValueNotAllowedError(string propertyName, bool value, bool allowedValuesOnly)
        {
            throw new InvalidOperationException("Cannot set given value " + value + " into property : " + propertyName + " where allowed value is only " +  allowedValuesOnly);
        }
        #endregion
    }
}
