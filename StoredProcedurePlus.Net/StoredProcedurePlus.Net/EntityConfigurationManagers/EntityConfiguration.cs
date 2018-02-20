using StoredProcedurePlus.Net.EntityConfigurationManagers;
using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes;
using StoredProcedurePlus.Net.EntityManagers;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public class EntityConfiguration<S> : NonPrimitiveEntityConfiguration where S : class, new()
    {
        #region Private

        private class OrdinalProxy : EntityOrdinalConfiguration
        {
            internal OrdinalProxy(List<PropertyConfiguration> parameters, IDataEntityAdapter record) : base(parameters, record) { }
        }

        private class DbParameterEntityAdapterProxy : DbParameterEntityAdapter
        {
            internal DbParameterEntityAdapterProxy(List<PropertyConfiguration> configurations) :base(configurations)
            {

            }
        }

        private class DbDataEntityAdapterProxy : DbDataEntityAdapter
        {
            internal DbDataEntityAdapterProxy(IDataReader record) : base(record)
            {

            }
        }

        private readonly List<PropertyConfiguration> Configurations = new List<PropertyConfiguration>();

        private LambdaExpression BuildExpression(Type sourceType, PropertyInfo propertyInfo)
        {
            var parameter = Expression.Parameter(sourceType, propertyInfo.Name);
            var property = Expression.Property(parameter, propertyInfo);
            var funcType = typeof(Func<,>).MakeGenericType(sourceType, propertyInfo.PropertyType);
            var lambda = Expression.Lambda(funcType, property, parameter);
            return lambda;
        }

        private void AddMapping(PropertyConfiguration configuration)
        {
            Configurations.Remove(configuration);//IT will only check agaist property name. 
            Configurations.Add(configuration);
        }

        #endregion

        protected override void InitializePropertyConfigurations()
        {
            SourceType = typeof(S);

            PropertyInfo[] Properties = SourceType.GetProperties();

            for (int i = 0; i < Properties.Length; i++)
            {
                if (!Configurations.Exists(v=>v.PropertyName == Properties[i].Name))
                {
                    if (Properties[i].PropertyType == typeof(bool))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, bool>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(bool?))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, bool?>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(int))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, int>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(int?))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, int?>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(long))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, long>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(long?))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, long?>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(decimal))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, decimal>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(decimal?))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, decimal?>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(double))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, double>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(double?))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, double?>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(DateTime))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, DateTime>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(DateTime?))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, DateTime?>>)l);
                    }
                    if (Properties[i].PropertyType == typeof(string))
                    {
                        LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                        Maps((Expression<Func<S, string>>)l);
                    }
                }
            }
        }

        #region Internal

        internal override object CreateNewDefaultInstance()
        {
            return new S();
        }

        internal IDataEntityAdapter GetAsDbParameters()
        {
            return new DbParameterEntityAdapterProxy(Configurations);
        }

        internal EntityConfiguration(){}

        override internal void Prepare(IDataEntityAdapter record)
        {
            OrdinalProvider = new OrdinalProxy(Configurations, record);
        }

        override internal DbDataEntityAdapter GetNewDataAdapter(IDataReader record)
        {
            return new DbDataEntityAdapterProxy(record);
        }

        EntityOrdinalConfiguration OrdinalProvider = null;
        override internal void Set(IDataEntityAdapter fromEntity, object toInstance)
        {
            if (OrdinalProvider == null) Error.PrepareDidnotCalled();

            S Instance = (S)toInstance;

            for(int i = 0; i < Configurations.Count; i++) 
            {
                PropertyConfiguration configuration = Configurations[i];

                int Ordinal = OrdinalProvider[configuration.PropertyName];

                if (configuration.DataType == typeof(bool))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableBooleanProperty(configuration.PropertyName);
                    }
                    else
                    {
                        BoolTypeConfiguration<S> Configuration = configuration as BoolTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetBool(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(bool?))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        BoolTypeNullableConfiguration<S> Configuration = configuration as BoolTypeNullableConfiguration<S>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        BoolTypeNullableConfiguration<S> Configuration = configuration as BoolTypeNullableConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetBool(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(short))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableShortProperty(configuration.PropertyName);
                    }
                    else
                    {
                        ShortTypeConfiguration<S> Configuration = configuration as ShortTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetShort(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(short?))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        ShortTypeNullableConfiguration<S> Configuration = configuration as ShortTypeNullableConfiguration<S>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        ShortTypeNullableConfiguration<S> Configuration = configuration as ShortTypeNullableConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetShort(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(int))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableIntProperty(configuration.PropertyName);
                    }
                    else
                    {
                        IntegerTypeConfiguration<S> Configuration = configuration as IntegerTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetInt(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(int?))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        IntegerTypeNullableConfiguration<S> Configuration = configuration as IntegerTypeNullableConfiguration<S>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        IntegerTypeNullableConfiguration<S> Configuration = configuration as IntegerTypeNullableConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetInt(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(long))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableLongProperty(configuration.PropertyName);
                    }
                    else
                    {
                        LongTypeConfiguration<S> Configuration = configuration as LongTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetLong(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(long?))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        LongTypeNullableConfiguration<S> Configuration = configuration as LongTypeNullableConfiguration<S>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        LongTypeNullableConfiguration<S> Configuration = configuration as LongTypeNullableConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetLong(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(double))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableDoubleProperty(configuration.PropertyName);
                    }
                    else
                    {
                        DoubleTypeConfiguration<S> Configuration = configuration as DoubleTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetDouble(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(double?))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        DoubleTypeNullableConfiguration<S> Configuration = configuration as DoubleTypeNullableConfiguration<S>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        DoubleTypeNullableConfiguration<S> Configuration = configuration as DoubleTypeNullableConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetDouble(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(decimal))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableDecimalProperty(configuration.PropertyName);
                    }
                    else
                    {
                        DecimalTypeConfiguration<S> Configuration = configuration as DecimalTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetDecimal(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(decimal?))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        DecimalTypeNullableConfiguration<S> Configuration = configuration as DecimalTypeNullableConfiguration<S>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        DecimalTypeNullableConfiguration<S> Configuration = configuration as DecimalTypeNullableConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetDecimal(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(DateTime))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableDateTimeProperty(configuration.PropertyName);
                    }
                    else
                    {
                        DateTimeTypeConfiguration<S> Configuration = configuration as DateTimeTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetDate(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(DateTime?))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        DateTimeTypeNullableConfiguration<S> Configuration = configuration as DateTimeTypeNullableConfiguration<S>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        DateTimeTypeNullableConfiguration<S> Configuration = configuration as DateTimeTypeNullableConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetDate(Ordinal);
                    }
                }
                else if (configuration.DataType == typeof(string))
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                        Configuration[Instance] = fromEntity.GetString(Ordinal);
                    }               
                }
            }
        }

        override internal void Get(object fromInstance, IDataEntityAdapter toEntity)
        {
            if (OrdinalProvider == null) Error.PrepareDidnotCalled();

            S Instance = (S)fromInstance;

            for (int i = 0; i < Configurations.Count; i++)
            {
                PropertyConfiguration configuration = Configurations[i];

                int Ordinal = OrdinalProvider[configuration.PropertyName];
                if (configuration.DataType == typeof(bool))
                {
                    BoolTypeConfiguration<S> Configuration = configuration as BoolTypeConfiguration<S>;
                    toEntity.SetBool(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(bool?))
                {
                    BoolTypeNullableConfiguration<S> Configuration = configuration as BoolTypeNullableConfiguration<S>;
                    toEntity.SetBool(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(short))
                {
                    ShortTypeConfiguration<S> Configuration = configuration as ShortTypeConfiguration<S>;
                    toEntity.SetShort(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(short?))
                {
                    ShortTypeNullableConfiguration<S> Configuration = configuration as ShortTypeNullableConfiguration<S>;
                    toEntity.SetShort(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(int))
                {
                    IntegerTypeConfiguration<S> Configuration = configuration as IntegerTypeConfiguration<S>;
                    toEntity.SetInt(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(int?))
                {
                    IntegerTypeNullableConfiguration<S> Configuration = configuration as IntegerTypeNullableConfiguration<S>;
                    toEntity.SetInt(Ordinal, Configuration[Instance]);
                }
                if (configuration.DataType == typeof(long))
                {
                    LongTypeConfiguration<S> Configuration = configuration as LongTypeConfiguration<S>;
                    toEntity.SetLong(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(long?))
                {
                    LongTypeNullableConfiguration<S> Configuration = configuration as LongTypeNullableConfiguration<S>;
                    toEntity.SetLong(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(double))
                {
                    DoubleTypeConfiguration<S> Configuration = configuration as DoubleTypeConfiguration<S>;
                    toEntity.SetDouble(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(double?))
                {
                    DoubleTypeNullableConfiguration<S> Configuration = configuration as DoubleTypeNullableConfiguration<S>;
                    toEntity.SetDouble(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(decimal))
                {
                    DecimalTypeConfiguration<S> Configuration = configuration as DecimalTypeConfiguration<S>;
                    toEntity.SetDecimal(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(decimal?))
                {
                    DecimalTypeNullableConfiguration<S> Configuration = configuration as DecimalTypeNullableConfiguration<S>;
                    toEntity.SetDecimal(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(DateTime))
                {
                    DateTimeTypeConfiguration<S> Configuration = configuration as DateTimeTypeConfiguration<S>;
                    toEntity.SetDateTime(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(DateTime?))
                {
                    DateTimeTypeNullableConfiguration<S> Configuration = configuration as DateTimeTypeNullableConfiguration<S>;
                    toEntity.SetDateTime(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == typeof(string))
                {
                    StringTypeConfiguration<S> Configuration = configuration as StringTypeConfiguration<S>;
                    toEntity.SetString(Ordinal, Configuration[Instance]);
                }
            }
        }

        #endregion

        #region Public
        public BoolTypeConfiguration<S> Maps(Expression<Func<S, bool>> memberSelector)
        {
            BoolTypeConfiguration<S> Configuration = new BoolTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public BoolTypeNullableConfiguration<S> Maps(Expression<Func<S, bool?>> memberSelector)
        {
            BoolTypeNullableConfiguration<S> Configuration = new BoolTypeNullableConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public ShortTypeConfiguration<S> Maps(Expression<Func<S, short>> memberSelector)
        {
            ShortTypeConfiguration<S> Configuration = new ShortTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public ShortTypeNullableConfiguration<S> Maps(Expression<Func<S, short?>> memberSelector)
        {
            ShortTypeNullableConfiguration<S> Configuration = new ShortTypeNullableConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public IntegerTypeConfiguration<S> Maps(Expression<Func<S, int>> memberSelector)
        {
            IntegerTypeConfiguration<S> Configuration = new IntegerTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public IntegerTypeNullableConfiguration<S> Maps(Expression<Func<S, int?>> memberSelector)
        {
            IntegerTypeNullableConfiguration<S> Configuration = new IntegerTypeNullableConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public LongTypeConfiguration<S> Maps(Expression<Func<S, long>> memberSelector)
        {
            LongTypeConfiguration<S> Configuration = new LongTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public LongTypeNullableConfiguration<S> Maps(Expression<Func<S, long?>> memberSelector)
        {
            LongTypeNullableConfiguration<S> Configuration = new LongTypeNullableConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DecimalTypeConfiguration<S> Maps(Expression<Func<S, decimal>> memberSelector)
        {
            DecimalTypeConfiguration<S> Configuration = new DecimalTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DecimalTypeNullableConfiguration<S> Maps(Expression<Func<S, decimal?>> memberSelector)
        {
            DecimalTypeNullableConfiguration<S> Configuration = new DecimalTypeNullableConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DoubleTypeConfiguration<S> Maps(Expression<Func<S, double>> memberSelector)
        {
            DoubleTypeConfiguration<S> Configuration = new DoubleTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DoubleTypeNullableConfiguration<S> Maps(Expression<Func<S, double?>> memberSelector)
        {
            DoubleTypeNullableConfiguration<S> Configuration = new DoubleTypeNullableConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DateTimeTypeConfiguration<S> Maps(Expression<Func<S, DateTime>> memberSelector)
        {
            DateTimeTypeConfiguration<S> Configuration = new DateTimeTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DateTimeTypeNullableConfiguration<S> Maps(Expression<Func<S, DateTime?>> memberSelector)
        {
            DateTimeTypeNullableConfiguration<S> Configuration = new DateTimeTypeNullableConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public StringTypeConfiguration<S> Maps(Expression<Func<S, string>> memberSelector)
        {
            StringTypeConfiguration<S> Configuration = new StringTypeConfiguration<S>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        #endregion
    }
}