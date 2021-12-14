using BusLay.Context;
using BusLay.Extentions;
using BusLay.Forms;
using BusLay.View;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modules.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusLay.Services
{
    public class CustomerService
    {
        private readonly DataContext context;
        private readonly IPasswordHasher<object> hasher;
        private readonly JwtService service;
        public CustomerService(DataContext context, JwtService service)
        {
            hasher = new PasswordHasher<object>();
            this.context = context;
            this.service = service;
        }

        public Session GetSession(Func<Session, bool> predicate)
        {
            return context.Sessions.Include(e => e.Customer).FirstOrDefault(predicate);
        }

        public bool Any(Func<Customer, bool> p)
        {
            return context.Customers.Any(p);
        }
        public Task<List<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate)
        {
            var query = context.Customers.AsQueryable();
            if (predicate is not null)
            {
                return query.Where(predicate).ToListAsync();
            }
            return query.ToListAsync();
        }

        public bool IsPasswordMatch(string password, int accountId)
        {
            var account = context.Customers.FirstOrDefault(acc => acc.Id == accountId);
            var result = hasher.VerifyHashedPassword(null, account.Password, password);
            if (result == PasswordVerificationResult.Success)
                return true;
            return false;
        }

        public async Task<Customer> FindOneAsync(Expression<Func<Customer, bool>> predicate = null)
        {
            return await Task.Run(() =>
            {
                return context.Customers.Where(predicate).Include(a => a.Roles).FirstOrDefaultAsync();
            });
        }

        public void Remove(int customerId)
        {
            var customer = context.Customers.FirstOrDefault(c => c.Id == customerId);
            context.Customers.Remove(customer);
            context.SaveChanges();
        }

        public async Task<Customer> FindeOneAsync(Expression<Func<Customer, bool>> predicate = null)
        {
            return await Task.Run(() =>
            {
                return context.Customers.FirstOrDefaultAsync(predicate);
            });
        }

        public Customer Create(CustomerForm form)
        {
            var entry = context.Customers.Add(new Customer
            {
                Password = hasher.HashPassword(null, form.Password),
                DOB = form.DOB,
                FirstName = form.FirstName,
                LastName = form.LastName,
                Rating = form.Rating,
                Username = form.UserName
            });
            var roles = context.Roles.Where(x => form.Roles.Contains(x.Name.Trim().ToLower())).ToList();
            if (roles.Any())
            {
                entry.Entity.Roles = roles;
            }
            context.SaveChanges();
            return entry.Entity;
        }

        public async Task<bool> RegisterAsync(RegisterForm form)
        {
            Random random = new Random();
            return await Task.Run(() =>
            {
                var newAccount = context.Customers.Add(new Customer
                {
                    DOB = form.DOB,
                    FirstName = form.FirstName,
                    LastName = form.LastName,
                    Rating = random.Next(10, 100),
                    Username = form.UserName,
                    Password = hasher.HashPassword(null, form.Password),
                });

                return false;
            });
        }
        public List<Customer> GetAll()
        {
            var accounts = context.Customers.AsQueryable().Include(a => a.Roles);
            return accounts.ToList();
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

        public Customer Change(ChangeCustomerForm form)
        {
            try
            {
                var acc = context.Customers.Include(a => a.Roles).FirstOrDefault(acc => acc.Id == form.Id);
                acc.FirstName = form.FirstName;
                acc.LastName = form.LastName;
                acc.Username = form.Username;
                var result = context.SaveChanges();
                return acc;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CustomerView> GetByRating(CustomerFindForm form)
        {
            return context.Customers
                .Where(c => c.Rating > form.MinRating && c.Rating < form.MaxRating)
                .Select(a => a.ToView())
                .ToList();
        }

        public List<CustomerView> GetByName(CustomerFindForm findForm)
        {
            return context.Customers
                .Where(c => c.FirstName.Contains(findForm.FirstName))
                .Select(a => a.ToView())
                .ToList();
        }

        public List<CustomerView> GetByDOB(CustomerFindForm findForm)
        {
            return context.Customers
                .Where(c => c.DOB > findForm.MinDOB && c.DOB < findForm.MaxDOB)
                .Select(a => a.ToView())
                .ToList();
        }
    }
}
