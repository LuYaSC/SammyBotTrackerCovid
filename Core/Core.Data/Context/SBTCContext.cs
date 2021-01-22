namespace TC.Core.Data.Context
{
    using TC.Core.Data.Attributes;
    using EntityFramework.DynamicFilters;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;

    public class SBTCContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public SBTCContext(string nameOrConnectionString = "SBTCContext") : base(nameOrConnectionString) { }

        public IPrincipal userInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("doctor");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<UserRole>().HasKey(t => new { t.RoleId, t.UserId }).ToTable("UserRoles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Filter("IsDeleted", (ILogicalDelete d) => d.IsDeleted, false);
            Precision.ConfigureModelBuilder(modelBuilder);
        }

        //public void DisableFilter<CONTEXT>(CONTEXT context, string nameFilter)
        //    where CONTEXT : DbContext
        //{
        //    DynamicFilterExtensions.DisableFilter(context, nameFilter);
        //}

        //public void EnableFilter<CONTEXT>(CONTEXT context, string nameFilter)
        //   where CONTEXT : DbContext
        //{
        //    DynamicFilterExtensions.EnableFilter(context, nameFilter);
        //}


        public DbSet<AuditGroup> AuditGroup { get; set; }

        #region Methods

        public void Detach<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Detached;
        }

        public void ExecuteCommand(string sql, params object[] parameters)
        {
            Database.ExecuteSqlCommand(sql, parameters);
        }

        public T ExecuteScalar<T>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<T>(sql, parameters).Single();
        }

        public T ExecuteScalar<T>(string sql, int timeOut, params object[] parameters)
        {
            Database.CommandTimeout = timeOut;
            try
            {
                return Database.SqlQuery<T>(sql, parameters).Single();
            }
            finally
            {
                Database.Connection.Close();
            }

        }

        public List<T> ExecuteScalarList<T>(string sql, int timeOut, params object[] parameters)
        {
            Database.CommandTimeout = timeOut;
            try
            {
                return Database.SqlQuery<T>(sql, parameters).ToList();
            }
            finally
            {
                Database.Connection.Close();
            }

        }

        public List<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void Remove<T>(T entity)
            where T : class, IBase<int>
        {
            Remove<T, int>(entity);
        }

        public void RemoveRange<T>(List<T> entities)
          where T : class, IBase<int>
        {
            foreach (var entity in entities)
            {
                Remove<T, int>(entity);
            }
        }

        public void RemoveData<T>(List<T> entities)
          where T : class //, IBase<int>
        {
            foreach (var entity in entities)
            {
                Entry(entity).State = EntityState.Deleted;
            }
        }

        public void Remove<T, TypeKey>(T entity)
            where T : class, IBase<TypeKey>
            where TypeKey : IEquatable<TypeKey>, IConvertible
        {
            if (entity == null)
            {
                return;
            }
            if (entity is IAuditEntity auditEntity)
            {
                if (auditEntity.Auditoria == null)
                {
                    auditEntity.Auditoria = new AuditGroup() { Entity = typeof(T).FullName };
                    Save<AuditGroup, int>(auditEntity.Auditoria);
                    auditEntity.GrupoDeAuditoriaId = auditEntity.Auditoria.Id;
                }
                else
                {
                    auditEntity.Auditoria = AuditGroup.FirstOrDefault(g => g.Id == auditEntity.GrupoDeAuditoriaId);
                }
                auditEntity.Auditoria.Audits.Add(new Audit()
                {
                    UserId = HttpContext.Current.User.Identity.Name,
                    Date = DateTime.Now,
                    Action = (int)AuditTypeEnum.Delete
                });
            }
            if (entity is ILogicalDelete entityLogicalDelete)
            {
                entityLogicalDelete.IsDeleted = true;
                Save<T, TypeKey>((T)entityLogicalDelete);

                return;
            }
            Entry(entity).State = EntityState.Deleted;
            SaveChanges();
        }

        public T Save<T>(T entity)
            where T : class, IBase<int>
        {
            return Save<T, int>(entity);
        }

        public T Save<T, TypeKey>(T entity)
            where T : class, IBase<TypeKey>
            where TypeKey : IEquatable<TypeKey>, IConvertible
        {
            SetEntity<T, TypeKey>(entity);
            SaveChanges();
            return entity;
        }

        public void SetEntity<TEntity>(TEntity entity)
            where TEntity : class, IBase<TEntity>
        {
            SetEntity(entity);
        }

        public void SetEntity<TEntity, TypeKey>(TEntity entity)
            where TEntity : class, IBase<TypeKey>
            where TypeKey : IEquatable<TypeKey>, IConvertible
        {
            if (entity == null)
            {
                return;
            }
            if (entity.Id.Equals(0))
            {
                Entry(entity).State = EntityState.Added;
                if (entity is IAuditEntity auditEntity)
                {
                    if (auditEntity.Auditoria == null)
                    {
                        auditEntity.Auditoria = new AuditGroup() { Entity = typeof(TEntity).FullName };
                    }

                    auditEntity.Auditoria.Audits.Add(new Audit()
                    {
                        UserId = HttpContext.Current.User.Identity.Name,
                        Date = DateTime.Now,
                        Action = (int)AuditTypeEnum.Create
                    });
                }
                if (entity is IDateCreation)
                {
                    (entity as IDateCreation).DateCreation = DateTime.Now;
                }
                if (entity is IDateModification)
                {
                    (entity as IDateModification).DateModification = DateTime.Now;
                }
                if (entity is IUserCreation<TypeKey>)
                {
                    (entity as IUserCreation<TypeKey>).UserCreation = this.userInfo.Identity.GetUserId<TypeKey>();
                }
                if (entity is IUserModification<TypeKey>)
                {
                    (entity as IUserModification<TypeKey>).UserModification = this.userInfo.Identity.GetUserId<TypeKey>();
                }
                if (entity is IUserCreation<string>)
                {
                    (entity as IUserCreation<string>).UserCreation = this.userInfo.Identity.GetUserId<string>();
                }
                if (entity is IUserModification<string>)
                {
                    (entity as IUserModification<string>).UserModification = this.userInfo.Identity.GetUserId<string>();
                }
            }
            else
            {
                UpdateEntity<TEntity, TypeKey>(entity);
            }
        }

        private void UpdateEntity<TEntity, TypeKey>(TEntity entity)
            where TEntity : class, IBase<TypeKey>
            where TypeKey : IEquatable<TypeKey>, IConvertible
        {
            DbPropertyValues currentValues = null;
            if (entity is ILogicalDelete entityLogicalDelete && !entityLogicalDelete.IsDeleted)
            {
                if (entity is IAuditEntity auditEntity)
                {
                    if (auditEntity.GrupoDeAuditoriaId == 0)
                    {
                        auditEntity.Auditoria = new AuditGroup() { Entity = typeof(TEntity).FullName };
                        Save<AuditGroup, int>(auditEntity.Auditoria);
                        auditEntity.GrupoDeAuditoriaId = auditEntity.Auditoria.Id;
                    }
                    else
                    {
                        auditEntity.Auditoria =
                            AuditGroup.FirstOrDefault(g => g.Id == auditEntity.GrupoDeAuditoriaId);
                    }
                    auditEntity.Auditoria.Audits.Add(new Audit()
                    {
                        UserId = HttpContext.Current.User.Identity.Name,
                        Date = DateTime.Now,
                        Action = (int)AuditTypeEnum.Update
                    });
                }
            }
            if (entity is IDateModification)
            {
                (entity as IDateModification).DateModification = DateTime.Now;
            }
            if (entity is IUserModification<string>)
            {
                (entity as IUserModification<string>).UserModification = this.userInfo.Identity.GetUserId<string>();
            }
            if (userInfo != null)
            {
                if (entity is IUserModification<TypeKey> && int.TryParse(userInfo.Identity.GetUserId<string>(), out int b))
                {

                    (entity as IUserModification<TypeKey>).UserModification = userInfo.Identity.GetUserId<TypeKey>(); ;
                }
            }
            DbEntityEntry<TEntity> entry = null;
            var entitySet = Set(typeof(TEntity));
            ObservableCollection<TEntity> entityCollection = entitySet.Local as ObservableCollection<TEntity>;
            TEntity attachedEntity = entityCollection.FirstOrDefault(m => m.Id.Equals(entity.Id));
            if (attachedEntity == null)
            {
                entry = Entry(entity);
                Entry(entity).State = EntityState.Modified;
            }
            else
            {
                Entry(attachedEntity).CurrentValues.SetValues(entity);

                if (Entry(attachedEntity).State != EntityState.Modified)
                {
                    Entry(attachedEntity).State = EntityState.Modified;
                }
                entry = Entry(attachedEntity);
            }
            if (entity is IAuditEntity creacion)
            {
                if (currentValues == null)
                {
                    currentValues = entry.CurrentValues.Clone();
                    entry.Reload();
                    entry.CurrentValues.SetValues(currentValues);
                }
            }
        }
        #endregion
    }
}
