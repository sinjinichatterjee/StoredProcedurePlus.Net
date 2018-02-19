using System;
using System.Data;

namespace StoredProcedurePlus.Net.EntityManagers
{
    internal class DbDataEntityAdapter : IDataEntityAdapter
    {
        readonly IDataReader Data;

        protected DbDataEntityAdapter(IDataReader data)
        {
            Data = data;
        }

        public int FieldCount => Data.FieldCount;

        public string GetName(int ordinal)
        {
            return Data.GetName(ordinal);
        }

        public int GetOrdinal(string name)
        {
            return Data.GetOrdinal(name);
        }

        public bool IsDBNull(int ordinal)
        {
            return Data.IsDBNull(ordinal);
        }

        #region Getters

        public bool GetBool(int ordinal)
        {
            return Data.GetBoolean(ordinal);
        }

        public short GetShort(int ordinal)
        {
            return Data.GetInt16(ordinal);
        }

        public int GetInt(int ordinal)
        {
            return Data.GetInt32(ordinal);
        }

        public long GetLong(int ordinal)
        {
            return Data.GetInt64(ordinal);
        }

        public decimal GetDecimal(int ordinal)
        {
            return Data.GetDecimal(ordinal);
        }

        public double GetDouble(int ordinal)
        {
            return Data.GetDouble(ordinal);
        }

        public DateTime GetDate(int ordinal)
        {
            return Data.GetDateTime(ordinal);
        }

        public string GetString(int ordinal)
        {
            return Data.GetString(ordinal);
        }

        #endregion

        #region setters

        public void SetBool(int ordinal, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetBool(int ordinal, bool? value)
        {
            throw new NotImplementedException();
        }

        public void SetShort(int ordinal, short value)
        {
            throw new NotImplementedException();
        }

        public void SetShort(int ordinal, short? value)
        {
            throw new NotImplementedException();
        }

        public void SetInt(int ordinal, int value)
        {
            throw new NotImplementedException();
        }

        public void SetInt(int ordinal, int? value)
        {
            throw new NotImplementedException();
        }

        public void SetLong(int ordinal, long value)
        {
            throw new NotImplementedException();
        }

        public void SetLong(int ordinal, long? value)
        {
            throw new NotImplementedException();
        }

        public void SetDecimal(int ordinal, decimal value)
        {
            throw new NotImplementedException();
        }

        public void SetDecimal(int ordinal, decimal? value)
        {
            throw new NotImplementedException();
        }
        public void SetDouble(int ordinal, double? value)
        {
            throw new NotImplementedException();
        }

        public void SetDouble(int ordinal, double value)
        {
            throw new NotImplementedException();
        }

        public void SetDateTime(int ordinal, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void SetDateTime(int ordinal, DateTime? dateTime)
        {
            throw new NotImplementedException();
        }

        public void SetString(int ordinal, string value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
