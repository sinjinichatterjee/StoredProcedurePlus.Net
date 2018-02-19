using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace StoredProcedurePlus.Net.EntityManagers
{
    internal class DbParameterEntityAdapter : IDataEntityAdapter
    {
        const string PARAMETERPREFIX = "@";

        readonly Dictionary<int, Tuple<string, DbParameter>> Parameters;

        protected DbParameterEntityAdapter(List<PropertyConfiguration> values)
        {
            if (values != null)
            {
                Parameters = new Dictionary<int, Tuple<string, DbParameter>>();

                for (int i = 0; i < values.Count; i++)
                {
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = string.Concat(PARAMETERPREFIX, values[i].ParameterName);
                    parameter.DbType = values[i].GetDbType;
                    parameter.Direction = values[i].IsOut ? System.Data.ParameterDirection.Output : System.Data.ParameterDirection.Input;
                    Parameters.Add(i, new Tuple<string, DbParameter>(values[i].PropertyName, parameter));
                }
            }
        }

        internal DbParameter this[int ordinal]
        {
            get
            {
                return Parameters[ordinal].Item2;
            }
        }

        public int FieldCount => Parameters.Count;

        public string GetName(int ordinal)
        {
            return Parameters[ordinal].Item1;
        }

        public int GetOrdinal(string name)
        {
            for(int i = 0; i< Parameters.Count; i++)
            {
                if (Parameters[i].Item1 == name) return i;
            }

            throw new IndexOutOfRangeException();
        }

        public bool IsDBNull(int ordinal)
        {
            if(Parameters[ordinal].Item2.Value == DBNull.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Getters

        public bool GetBool(int ordinal)
        {
            return bool.Parse(Parameters[ordinal].Item2.Value.ToString());
        }

        public short GetShort(int ordinal)
        {
            return short.Parse(Parameters[ordinal].Item2.Value.ToString());
        }

        public int GetInt(int ordinal)
        {
            return int.Parse(Parameters[ordinal].Item2.Value.ToString());
        }

        public long GetLong(int ordinal)
        {
            return long.Parse(Parameters[ordinal].Item2.Value.ToString());
        }

        public decimal GetDecimal(int ordinal)
        {
            return decimal.Parse(Parameters[ordinal].Item2.Value.ToString());
        }

        public double GetDouble(int ordinal)
        {
            return double.Parse(Parameters[ordinal].Item2.Value.ToString());
        }

        public DateTime GetDate(int ordinal)
        {
            return DateTime.Parse(Parameters[ordinal].Item2.Value.ToString());
        }

        public string GetString(int ordinal)
        {
            return Parameters[ordinal].Item2.Value.ToString();
        }

        #endregion

        #region Setters

        public void SetBool(int ordinal, bool value)
        {
            Parameters[ordinal].Item2.Value = value;
        }

        public void SetBool(int ordinal, bool? value)
        {
            if (value.HasValue)
            {
                Parameters[ordinal].Item2.Value = value.Value;
            }
            else
            {
                Parameters[ordinal].Item2.Value = DBNull.Value;
            }
        }

        public void SetShort(int ordinal, short value)
        {
            Parameters[ordinal].Item2.Value = value;
        }

        public void SetShort(int ordinal, short? value)
        {
            if (value.HasValue)
            {
                Parameters[ordinal].Item2.Value = value.Value;
            }
            else
            {
                Parameters[ordinal].Item2.Value = DBNull.Value;
            }
        }

        public void SetInt(int ordinal, int value)
        {
            Parameters[ordinal].Item2.Value = value;
        }

        public void SetInt(int ordinal, int? value)
        {
            if (value.HasValue)
            {
                Parameters[ordinal].Item2.Value = value.Value;
            }
            else
            {
                Parameters[ordinal].Item2.Value = DBNull.Value;
            }
        }

        public void SetLong(int ordinal, long value)
        {
            Parameters[ordinal].Item2.Value = value;
        }

        public void SetLong(int ordinal, long? value)
        {
            if (value.HasValue)
            {
                Parameters[ordinal].Item2.Value = value.Value;
            }
            else
            {
                Parameters[ordinal].Item2.Value = DBNull.Value;
            }
        }

        public void SetDecimal(int ordinal, decimal value)
        {
            Parameters[ordinal].Item2.Value = value;
        }

        public void SetDecimal(int ordinal, decimal? value)
        {
            if (value.HasValue)
            {
                Parameters[ordinal].Item2.Value = value.Value;
            }
            else
            {
                Parameters[ordinal].Item2.Value = DBNull.Value;
            }
        }

        public void SetDouble(int ordinal, double value)
        {
            Parameters[ordinal].Item2.Value = value;
        }

        public void SetDouble(int ordinal, double? value)
        {
            if (value.HasValue)
            {
                Parameters[ordinal].Item2.Value = value.Value;
            }
            else
            {
                Parameters[ordinal].Item2.Value = DBNull.Value;
            }
        }

        public void SetDateTime(int ordinal, DateTime value)
        {
            Parameters[ordinal].Item2.Value = value;
        }

        public void SetDateTime(int ordinal, DateTime? value)
        {
            if (value.HasValue)
            {
                Parameters[ordinal].Item2.Value = value.Value;
            }
            else
            {
                Parameters[ordinal].Item2.Value = DBNull.Value;
            }
        }

        public void SetString(int ordinal, string value)
        {
            Parameters[ordinal].Item2.Value = value ?? (object)DBNull.Value;
        }

        #endregion
    }
}
