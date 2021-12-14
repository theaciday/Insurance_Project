using BusLay.Context;
using BusLay.Forms;
using BusLay.View;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Modules.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLay.Services
{
    public class AuthService
    {
        private readonly DataContext context;
        private readonly JwtService jwtService;
        public AuthService(DataContext context, JwtService jwtService)
        {
            this.context = context;
            this.jwtService = jwtService;
        }

        public Session GetSession(Func<Session, bool> pred)
        {
            return context.Sessions.Include(e => e.Customer).FirstOrDefault(pred);
        }

        public TokensView Login(AuthForm form)
        {
            var customer = context.Customers.FirstOrDefault(cus => cus.Username == form.UserName.Trim());
            return CreateTokens(customer);
        }

        public Session CreateSession(Customer customer)
        {
            var sessions = context.Sessions.Where(u => u.CustomerId == customer.Id).ToList();
            if (sessions.Any())
                context.Sessions.RemoveRange(sessions);
            var newSession = context.Sessions.Add(new Session
            {
                CustomerId = customer.Id,
                AddedDate = DateTime.UtcNow,
                RefreshToken = Guid.NewGuid()
            });
            context.SaveChanges();
            return newSession.Entity;
        }

        public TokensView CreateTokens(Customer customer)
        {
            var session = CreateSession(customer);

            return new TokensView
            {
                AccessToken = jwtService.GenerateAccessToken(customer, session.Id),
                RefreshToken = session.RefreshToken
            };
        }
        public void Logout(int accountId)
        {
            var session = context.Sessions.FirstOrDefault(sess => sess.CustomerId == accountId);
            context.Sessions.Remove(session);
            context.SaveChanges();
        }
    }
}
