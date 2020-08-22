﻿namespace TC.Core.Business
{
    using TC.Core.Data;
    using TC.Core.Data.Context;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Linq;
    using System.Security.Principal;

    public abstract class BaseBusiness
    {
        public BaseBusiness(IConfiguration configuration, IPrincipal userInfo)
        {
            this.configuration = configuration;
        }

        protected IConfiguration configuration;
        protected IPrincipal UserInfo;
    }

    public abstract class BaseBusiness<T, CONTEXT> : BaseBusiness<T, int, CONTEXT>
        where T : class, IBase<int>
        where CONTEXT : SBTCContext
    {
        public BaseBusiness(CONTEXT context, IPrincipal userInfo, IConfiguration configuration = null)
            : base(context, userInfo, configuration)
        { }
    }

    public abstract class BaseBusiness<T, TypeKey, CONTEXT> : BaseBusiness, IBaseBusiness<T, TypeKey, CONTEXT>
        where T : class, IBase<TypeKey>
        where TypeKey : IEquatable<TypeKey>, IConvertible
        where CONTEXT : SBTCContext
    {
        public BaseBusiness(CONTEXT context, IPrincipal userInfo, IConfiguration configuration = null)
            : base(configuration, userInfo)
        {
            this.Context = context;
            this.Context.userInfo = userInfo;
            this.UserInfo = userInfo;
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public virtual T Get(TypeKey id)
        {
            var entity = this.Context.Set<T>().Find(id);
            return entity;
        }

        public virtual IQueryable<T> List()
        {
            var isLogical = typeof(T).GetInterfaces().Contains(typeof(ILogicalDelete));
            if (!isLogical)
            {
                return this.Context.Set<T>();
            }
            else
            {
                var returnCol = this.Context.Set<T>().Where(x => (x as BaseLogicalDelete<TypeKey>).IsDeleted == false);
                return returnCol;
            }
        }

        public virtual void Remove(TypeKey id)
        {
            var entity = this.Get(id);
            this.Context.Remove<T, TypeKey>(entity);
        }

        public virtual T Save(T entity)
        {
            this.Validate(entity);
            return this.Context.Save<T, TypeKey>(entity);
        }

        protected virtual void Validate(T entity)
        {
            
        }

        public virtual Result<string> WarmUp()
        {
            var data = Context.Set<T>().FirstOrDefault();
            return Result<string>.SetOk(string.Empty);
        }

        protected CONTEXT Context;
    }
}
