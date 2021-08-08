using CustomerManagementSystem.Domain.CompanyC;
using CustomerManagementSystem.Infrastructure.CompanyC;
using CustomerManagementSystem.Services.CompanyC.Interfaces;
using CustomerManagementSystem.Services.CompanyC.Models;
using CustomerManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Services.CompanyC
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext m_Context;
        private readonly IScoringService m_ScoringService;
        private readonly IEmailValidationService m_EmailValidationService;

        public CustomerService(DataContext context, IScoringService scoringService, IEmailValidationService mailValidationService)
        {
            m_Context = context;
            m_ScoringService = scoringService;
            m_EmailValidationService = mailValidationService;
        }


        public async Task<CustomerModel> Create(CustomerModel model)
        {
            var token = m_EmailValidationService.GenerateToken();
            var added = new Customer
            {
                Name = model.Name,
                Surname = model.Surname,
                Education = model.Education,
                Email = model.Email,
                VerificationToken = token,
                Score = m_ScoringService.CalculateScore()
            };
            m_EmailValidationService.SendOTPMail(model.Email, token);
            m_Context.Customers.Add(added);
            await m_Context.SaveChangesAsync();
            return new CustomerModel(added);
        }

        public async Task Delete(int id)
        {
            var existing = await m_Context.Customers
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null)
                throw new KeyNotFoundException($"Customer with id: {id} does not exist!");

            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.Now;
            await m_Context.SaveChangesAsync();
        }

        public async Task<List<CustomerModel>> GetAll()
        {
            return await m_Context.Customers.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => new CustomerModel(x)).ToListAsync();
        }

        public async Task<CustomerModel> GetById(int id)
        {
            var existing = await m_Context.Customers.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null)
                throw new KeyNotFoundException($"Customer with id: {id} does not exist!");

            return new CustomerModel(existing);
        }

        public async Task Update(int id, CustomerModel model)
        {
            var existing = await m_Context.Customers
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null)
                throw new KeyNotFoundException($"Customer with id: {id} does not exist!");

            existing.Name = model.Name;
            existing.Surname = model.Surname;
            existing.Education = model.Education;
            existing.Email = model.Email;
            await m_Context.SaveChangesAsync();
        }

        public async Task<bool> ValidateEmail(string token, string email)
        {
            var existing = await m_Context.Customers
               .Where(x => !x.IsDeleted)
               .FirstOrDefaultAsync(x => x.Email == email);

            if (existing == null)
                throw new KeyNotFoundException($"Customer with email: {email} does not exist!");

            if(existing.VerificationToken != token)
                return false;

            existing.IsVerified = true;
            await m_Context.SaveChangesAsync();
            return true;
        }
    }
}
