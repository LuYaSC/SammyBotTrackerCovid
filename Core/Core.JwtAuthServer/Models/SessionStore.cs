namespace TC.Core.JwtAuthServer.Models
{
    using TC.Core.JwtAuthServer.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;

    public enum OperationTypeSession
    {
        Success = 6,
        Failed,
        Generate,
        Change,
        Locked = 12
    }
    public class SessionStore
    {
        private AuthContext ctx;

        public const string TYPE_SESSION_ERROR = "E";
        public const string TYPE_SESSION_UNLOCKED = "D";
        public const string TYPE_SESSION_CHANGE = "C";
        public const string TYPE_SESSION_GENERATE = "G";
        public const string TYPE_SESSION_RESET = "R";
        public const string TYPE_SESSION_LOCKED = "B";
        public const string TYPE_SESSION_LOGIN = "I";
        public const string TYPE_SESSION_NEW = "N";

        public SessionStore()
        {
            this.ctx = new AuthContext();
        }

        public void Dispose()
        {
            this.ctx.Dispose();
        }

        public Session SaveSession(User user, string operation, string IpClient, bool IPMovil = false)
        {
            SessionStore sessionStore = new SessionStore();
            Session session = new Session();
            try
            {
                session.UserId = user.Id;
                session.Password = user.PasswordHash;
                session.Ip = IPMovil ? "APP_MOVIL" : string.IsNullOrEmpty(IpClient) ? "NOT_IP" : IpClient;
                session.Action = operation;
                session.CardNumber = user.UserName;
                session.UserCreation = "ADMIN";
                session.UserModification = "ADMIN";
                session.DateCreation = DateTime.Now;
                this.ctx.Sessions.Add(session);
                this.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                session.UserId = user.Id;
                session.Password = user.PasswordHash;
                session.Ip = "NOT_IP";
                session.Action = operation;
                session.CardNumber = user.UserName;
                session.UserCreation = "ADMIN";
                session.UserModification = "ADMIN";
                session.DateCreation = DateTime.Now;
                this.ctx.Sessions.Add(session);
                this.ctx.SaveChanges();
            }
            return session;
        }

        public ExchangeRate FindCurrentExchangeRate()
        {
            return this.ctx.ExchangeRates.Where(f => f.Date == this.ctx.ExchangeRates.Max(f2 => f2.Date)).FirstOrDefault();
        }

        public IEnumerable<Session> GetHistoyPasswordChange(User user, int numberToVerify)
        {
            var listLastPassword = this.ctx.Sessions.Where(r => r.User.UserName == user.UserName && (r.Action == TYPE_SESSION_CHANGE || r.Action == TYPE_SESSION_GENERATE))
                .OrderBy(r => r.DateCreation)
                .ToList()
                .Take(numberToVerify);

            return listLastPassword;
        }

        public bool BlockUser(User user)
        {
            user.State = "B";
            this.ctx.SaveChanges();
            return true;
        }
    }
}