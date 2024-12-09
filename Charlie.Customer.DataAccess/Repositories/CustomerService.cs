using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlie.Customer.DataAccess.Service
{
    public class CustomerService : ICustomerService<Customer>
    {
        private readonly CustomerDbContext _context;
        private readonly DbSet<Customer> _dbSet;

        public CustomerService(CustomerDbContext context, DbSet<Customer> dbSet)
        {   
            _context = context;
            _dbSet = context.Set<Customer>();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _dbSet.Find(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbSet.ToList();
        }

        public async Task AddAsync(Customer item)
        {
            await _dbSet.Add(item);
        }

        public async Task RemoveAsync(int id)
        {
            await _dbSet.Remove(GetByIdAsync(id));
        }

        public async Task UpdateAsync(Customer item)
        {
            await _dbSet.Update(item);
        }
    }
}
