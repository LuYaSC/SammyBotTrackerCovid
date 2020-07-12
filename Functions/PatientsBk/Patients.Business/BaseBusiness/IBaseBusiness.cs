using SBTC.Functions.Patients.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace SBTC.Functions.Patients.Business.BaseBusiness
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
