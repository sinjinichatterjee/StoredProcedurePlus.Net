using System;

namespace StoredProcedurePlus.Net.EntityManagers
{
    interface IDataEntityAdapter
    {
        int FieldCount { get; }
        string GetName(int ordinal);
        int GetOrdinal(string name);

        bool IsDBNull(int ordinal);
        bool GetBool(int ordinal);        
        short GetShort(int ordinal);
        int GetInt(int ordinal);
        long GetLong(int ordinal);
        decimal GetDecimal(int ordinal);
        double GetDouble(int ordinal);
        DateTime GetDate(int ordinal);
        string GetString(int ordinal);

        void SetBool(int ordinal, bool value);
        void SetBool(int ordinal, bool? value);       
        void SetShort(int ordinal, short value);
        void SetShort(int ordinal, short? value);
        void SetInt(int ordinal, int value);
        void SetInt(int ordinal, int? value);
        void SetLong(int ordinal, long value);
        void SetLong(int ordinal, long? value);
        void SetDecimal(int ordinal, decimal value);
        void SetDecimal(int ordinal, decimal? value);
        void SetDouble(int ordinal, double value);
        void SetDouble(int ordinal, double? value);
        void SetDateTime(int ordinal, DateTime value);
        void SetDateTime(int ordinal, DateTime? value);
        void SetString(int ordinal, string value);
    }
}
