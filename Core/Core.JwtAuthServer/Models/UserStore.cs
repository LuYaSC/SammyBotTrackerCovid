using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TC.Core.JwtAuthServer.Entities;
using System.Threading.Tasks;

namespace TC.Core.JwtAuthServer.Models
{
    public class UserStore 
        //: Microsoft.AspNet.Identity.IUserStore<CredinetWebUser, int>
    {
        private AuthContext ctx;

        public const string USER_STATE_UNLOCKED = "D";
        public const string USER_STATE_CHANGE = "C";
        public const string USER_STATE_GENERATE = "G";
        public const string USER_STATE_RESET = "R";
        public const string USER_STATE_LOCKED = "B";
        public const string USER_STATE_NEW = "N";

        public UserStore()
        {
            this.ctx = new AuthContext();
        }

        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId)
        {
            var user = this.ctx.Users.Find(userId);
            return Task.FromResult<User>(user);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        //public Empresa GetEmpresa(int id)
        //{
        //    return this.ctx.Empresas.Find(id);
        //}

        public UserDetail GetClient(string id)
        {
            return this.ctx.UserDetails.Find(id);
        }

        public Company GetCompany(int id)
        {
            return this.ctx.Companies.Find(id);
        }

        public List<User> ListByRole(int clientId, string roleName)
        {
            var role = this.ctx.Roles.Where(r => r.Name == roleName).FirstOrDefault();
            if (role == null)
            {
                throw new Exception("Invalid rolename");
            }

            var usuarios = this.ctx.Users.Include(r => r.Roles).Where(c => c.Id == clientId).ToList();
            return usuarios.Where(c => c.Roles.Any(r => r.RoleId == role.Id)).ToList();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}