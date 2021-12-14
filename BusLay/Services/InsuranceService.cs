using BusLay.Context;
using BusLay.Extentions;
using BusLay.Forms;
using BusLay.View;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusLay.Services
{
    public class InsuranceService
    {
        private readonly DataContext context;

        public InsuranceService(DataContext context)
        {
            this.context = context;
        }

        public InsuranceView AddInsurance(InsuranceForm form)
        {
            var result = context.Insuranses.Add(new Insurance
            {
                InsurName = form.InsurName,
                InsurType = form.InsurType,
                Price = form.Price,
            });
            context.SaveChanges();
            return result.Entity.ToView();
        }

        public bool Any(Func<Insurance, bool> p)
        {
            return context.Insuranses.Any(p);
        }

        public List<Insurance> GetAll()
        {

            var insur = context.Insuranses.AsQueryable();
            return insur.ToList();
        }

        public bool Remove(InsuranceForm form)
        {
            var insurance = context.Insuranses.FirstOrDefault(f => f.InsurName.Contains(form.InsurName.Trim()));
            context.Insuranses.Remove(insurance);
            context.SaveChanges();
            return true;
        }
        public InsuranceView ChangeInsuranse(InsuranceForm form)
        {
            var insur = context.Insuranses.FirstOrDefault(f => f.InsurName.Contains(form.InsurName.Trim()));
            if (insur is null)
                return new InsuranceView()
                {
                    Error = "insurance does not exist"
                };
            if (form.InsurType.Length != 0)
            {
                insur.InsurType = form.InsurType;
            }
            if (form.InsurName.Length != 0)
            {
                insur.InsurName = form.InsurName;
            }
            if (form.Price != 0)
            {
                insur.Price = form.Price;
            }
            context.SaveChanges();
            return insur.ToView();
        }

        public Task<List<InsuranceView>> FindAsync(Expression<Func<Insurance, bool>> predicate)
        {
            List<InsuranceView> insuranceViews = new List<InsuranceView>();
            var query = context.Insuranses.AsQueryable();
            foreach (var item in query)
            {
                insuranceViews.Add(item.ToView());
            }
            return Task.FromResult(insuranceViews);
        }

        public async Task<Insurance> FindOneAsync(Expression<Func<Insurance, bool>> predicate = null)
        {
            return await Task.Run(() =>
            {
                return context.Insuranses.Where(predicate).FirstOrDefaultAsync();
            });
        }


        public List<InsuranceView> GetByMaxPrice(InsuranceFindForm form)
        {
            return context.Insuranses
                .Where(c => c.Price < form.MaxPrice)
                .Select(a => a.ToView())
                .ToList();
        }

        public InsuranceView CheckRating(InsuranceForm insuranceForm, int customerId)
        {
            var customer = context.Customers.FirstOrDefault(c => c.Id == customerId);
            var insurance = context.Insuranses.FirstOrDefault(i => i.InsurName.Contains(insuranceForm.InsurName));
            if (customer.Rating < 30)
            {
                insurance.Price *= 1.2;
                return insurance.ToView();
            }
            if (customer.Rating > 50)
            {
                insurance.Price *= 0.9;
            }
            return insurance.ToView();
        }

        public List<InsuranceView> GetByMinPrice(InsuranceFindForm findForm)
        {
            return context.Insuranses
                .Where(c => c.Price > findForm.MinPrice)
                .Select(a => a.ToView())
                .ToList();
        }

        public List<InsuranceView> GetByInsurType(InsuranceFindForm findForm)
        {
            return context.Insuranses
                 .Where(c => c.InsurType.Contains(findForm.InsurType))
                 .Select(a => a.ToView())
                 .ToList();
        }

        public List<InsuranceView> GetByInsurName(InsuranceFindForm findForm)
        {
            return context.Insuranses
                 .Where(c => c.InsurName.Contains(findForm.InsurName))
                 .Select(a => a.ToView())
                 .ToList();
        }




    }
}
