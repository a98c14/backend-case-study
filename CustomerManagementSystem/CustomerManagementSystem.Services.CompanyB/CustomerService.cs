using CustomerManagementSystem.Domain.CompanyB;
using CustomerManagementSystem.Infrastructure.CompanyB;
using CustomerManagementSystem.Services.CompanyB.Interfaces;
using CustomerManagementSystem.Services.CompanyB.Models;
using CustomerManagementSystem.Services.Interfaces;
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
        private readonly IOneTimePinService m_OneTimePinService;

        public CustomerService(DataContext context, IScoringService scoringService, IOneTimePinService otpService)
        {
            m_Context = context;
            m_ScoringService = scoringService;
            m_OneTimePinService = otpService;
        }

        public async Task<CustomerModel> Create(CustomerModel model)
        {
            var verificatonToken = m_OneTimePinService.GenerateToken();
            var added = new Customer
            {
                Name = model.Name,
                Surname = model.Surname,
                Education = model.Education,
                GSM = model.GSM,
                VerificationToken = verificatonToken,
                Score = m_ScoringService.CalculateScore()
            };

            // Send the token to customer and set the verified to true when it is returned
            m_OneTimePinService.SendOtpMessage(model.GSM, verificatonToken);

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
            return await m_Context.Customers.AsNoTracking().Select(x => new CustomerModel(x)).ToListAsync();
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
            existing.GSM = model.GSM;
            await m_Context.SaveChangesAsync();
        }

        public async Task<bool> ValidateGSM(string token, string gsm)
        {
            var existing = await m_Context.Customers
               .Where(x => !x.IsDeleted)
               .FirstOrDefaultAsync(x => x.GSM == gsm);

            if (existing == null)
                throw new KeyNotFoundException($"Customer with GSM: {gsm} does not exist!");

            if (existing.VerificationToken != token)
                return false;

            existing.IsVerified = true;
            await m_Context.SaveChangesAsync();
            return true;
        }
    }
}
