using CustomerManagementSystem.Domain.CompanyB;
using CustomerManagementSystem.Infrastructure.CompanyB;
using CustomerManagementSystem.Services.CompanyB.Interfaces;
using CustomerManagementSystem.Services.CompanyB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Services.CompanyB
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext m_Context;
        private readonly IScoringService m_ScoringService;

        public CustomerService(DataContext context, IScoringService scoringService)
        {
            m_Context = context;
            m_ScoringService = scoringService;
        }

        public async Task<CustomerModel> Create(CustomerModel model)
        {
            var added = new Customer
            {
                Name = model.Name,
                Surname = model.Surname,
                Education = model.Education,
                GSM = model.GSM,
                Score = m_ScoringService.CalculateScore()
            };
            m_Context.Customers.Add(added);
            await m_Context.SaveChangesAsync();
            return new CustomerModel(added);
        }

        public async Task Delete(int id)
        {
            var existing = await m_Context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
                throw new KeyNotFoundException($"Customer with id: {id} does not exist!");

            m_Context.Customers.Remove(existing);
            await m_Context.SaveChangesAsync();
        }

        public async Task<List<CustomerModel>> GetAll()
        {
            return await m_Context.Customers.AsNoTracking().Select(x => new CustomerModel(x)).ToListAsync();
        }

        public async Task<CustomerModel> GetById(int id)
        {
            var existing = await m_Context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
                throw new KeyNotFoundException($"Customer with id: {id} does not exist!");

            return new CustomerModel(existing);
        }

        public async Task Update(int id, CustomerModel model)
        {
            var existing = await m_Context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
                throw new KeyNotFoundException($"Customer with id: {id} does not exist!");

            existing.Name = model.Name;
            existing.Surname = model.Surname;
            existing.Education = model.Education;
            existing.GSM = model.GSM;
            await m_Context.SaveChangesAsync();
        }
    }
}
