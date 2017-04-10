﻿using System;
using System.Linq.Expressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.Core
{
    public abstract class PrimitiveTypeConfiguration<S,T>: PropertyConfiguration where S : class
    {
        readonly EntityAccessor<S, T> Accessor;

        protected PrimitiveTypeConfiguration(Expression<Func<S, T>> memberSelector)
        {
            Accessor = EntityAccessor<S>.Create(memberSelector);
            PropertyName = Accessor.PropertyName;
            ParameterName = PropertyName;
            DataType = Accessor.DataType;
        }

        public T this[S instance]
        {
            get
            {
                T Result = Accessor[instance];
                return Result;
            }
            set
            {
                Accessor[instance] = ValidateAndSet(value);
            }
        }

        protected virtual T ValidateAndSet(T value)
        {
            return value;
        }
    }
}