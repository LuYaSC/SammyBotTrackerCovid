using TC.Core.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace TC.Core.Business
{
    public interface IBaseBusiness<T, TypeKey, CONTEXT>
        where T : IBase<TypeKey>
        where TypeKey : IEquatable<TypeKey>
        where CONTEXT : DbContext
    {
        void Dispose();

        T Get(TypeKey id);

        IQueryable<T> List();

        void Remove(TypeKey id);

        T Save(T entity);

        Result<string> WarmUp();
    }
}
