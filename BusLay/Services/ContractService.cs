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
using System.Threading.Tasks;

namespace BusLay.Services
{
    public class ContractService
    {
        private readonly DataContext dbContext;
        private readonly InsuranceService insuranceService;

        public ContractService(DataContext dbContext, InsuranceService insuranceService)
        {
            this.dbContext = dbContext;
            this.insuranceService = insuranceService;
        }

        public ContractView CreateContract(ContractForm form)
        {
            List<Insurance> insurances = new List<Insurance>();
            foreach (var item in form.InsurancesId)
            {
                insurances.Add(dbContext.Insuranses.FirstOrDefault(ins => ins.Id == item));
            }

            var newContract = dbContext.Contracts.Add(
                new Contract()
                {
                    CustomerId = form.CustomerId,
                    Details = form.Details,
                    FinalPrice = form.FinalPrice,
                    Duration = form.Duration,
                    Insuranses = insurances,
                    SignUpDate = DateTime.UtcNow,
                    ValidityDate = form.ValidityDate.ToDateTime(),
                });
            dbContext.SaveChanges();
            return newContract.Entity.ToView();
        }

        public List<ContractView> GetByDuration(ContractFindForm form)
        {
            return dbContext.Contracts
                .Where(c => c.Duration > form.MinDuration && c.Duration < form.MaxDuration)
                .Select(a => a.ToView())
                .ToList();
        }
        public List<ContractView> GetByPrice(ContractFindForm findForm)
        {
            return dbContext.Contracts
                .Where(c => c.FinalPrice > findForm.MinFinalPice && c.FinalPrice < findForm.MaxFinalPice)
                .Select(a => a.ToView())
                .ToList();
        }
        public List<ContractView> GetByMaxPrice(ContractFindForm findForm)
        {
            return dbContext.Contracts
                .Where(c => c.FinalPrice < findForm.MaxFinalPice)
                .Select(a => a.ToView())
                .ToList();
        }
        public List<ContractView> GetByMinPrice(ContractFindForm findForm)
        {
            return dbContext.Contracts
                .Where(c => c.FinalPrice > findForm.MinFinalPice)
                .Select(a => a.ToView())
                .ToList();
        }
        public bool Any(Func<Contract, bool> p)
        {
            return dbContext.Contracts.Any(p);
        }

        public async Task<List<ContractView>> GetAllAsync(int customerId)
        {
            return await Task.Run(() =>
            {
                var contracts = dbContext.Contracts.Where(a => a.CustomerId == customerId).Select(a => a.ToView()).ToList();

                return contracts;
            });
        }

        public async Task<Contract> FindOneAsync(Expression<Func<Contract, bool>> predicate = null)
        {
            return await Task.Run(() =>
            {
                return dbContext.Contracts.Where(predicate).Include(a => a.Insuranses).FirstOrDefaultAsync();
            });
        }

        public Contract Change(ContractForm form)
        {
            List<Insurance> insurances = new List<Insurance>();
            var contract = FindOneAsync(c => c.CustomerId == form.CustomerId);
            contract.Result.Details = form.Details.Length != 0 ? form.Details : contract.Result.Details;
            contract.Result.SignUpDate = DateTime.UtcNow;
            if (form.ValidityDate is not null)
            {
                contract.Result.ValidityDate = form.ValidityDate.ToDateTime();
            }
            if (form.Duration != 0)
            {
                contract.Result.Duration = form.Duration;
            }
            if (form.FinalPrice != 0)
            {
                contract.Result.FinalPrice = form.FinalPrice;
            }
            if (form.InsurancesId is not null)
            {
                foreach (var item in form.InsurancesId)
                {
                    var insurance = insuranceService.FindOneAsync(a => a.Id == item);
                    insurances.Add(insurance.Result);
                }
                contract.Result.Insuranses = insurances;
            }
            dbContext.SaveChanges();
            return contract.Result;
        }

        public bool RemoveContract(ContractForm form)
        {
            var contract = dbContext.Contracts.FirstOrDefault(a => a.CustomerId == form.CustomerId);
            if (contract is null)
                return false;
            dbContext.Remove(contract);
            dbContext.SaveChanges();
            return true;
        }
    }
}
