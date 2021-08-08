﻿using CustomerManagementSystem.Common.Exceptions;
using CustomerManagementSystem.Domain.CompanyA;
using CustomerManagementSystem.Infrastructure.CompanyA;
using CustomerManagementSystem.Services.CompanyA.Interfaces;
using CustomerManagementSystem.Services.CompanyA.Models;
using CustomerManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Services.CompanyA
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext m_Context;
        private readonly IScoringService m_ScoringService;
        private readonly IMernisValidationService m_ValidationService;

        public CustomerService(DataContext context, IScoringService scoringService, IMernisValidationService validationService)
        {
            m_Context = context;
            m_ScoringService = scoringService;
            m_ValidationService = validationService;
        }

        public async Task<CustomerModel> Create(CustomerModel model)
        {
            var isValid = m_ValidationService.ValidateCustomer(model.TCKN, model.Name, model.Surname, model.BirthDate.Year);
            if(!isValid)
                throw new ApiException("Invalid customer info");

            var added = new Customer
            {
                BirthDate = model.BirthDate,
                Name = model.Name,
                Surname = model.Surname,
                TCKN = model.TCKN,
                Score = m_ScoringService.CalculateScore()
            };
            m_Context.Customers.Add(added);
            await m_Context.SaveChangesAsync();
            return new CustomerModel(added);
        }

        public async Task Delete(int id)
        {
            var existing = await m_Context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if(existing == null)
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
            existing.TCKN = model.TCKN;
            existing.BirthDate = model.BirthDate;
            await m_Context.SaveChangesAsync();
        }
    }
}
