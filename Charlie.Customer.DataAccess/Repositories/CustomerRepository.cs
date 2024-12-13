using Charlie.Customer.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Charlie.Customer.DataAccess.Repositories
{
	public class CustomerRepository : ICustomerRepository<CustomerModel>
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {   
            _context = context;
        }

		public async Task AddAsync(CustomerModel item)
		{
			_context.Customer.AddAsync(item);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<CustomerModel>> GetAllAsync()
		{
			return await _context.Customer.ToListAsync();
		}

		public async Task<CustomerModel> GetByIdAsync(int id)
		{
			return await _context.Customer.FindAsync(id);
		}

		public async Task RemoveAsync(int id)
		{
			var customer = await GetByIdAsync(id);
			_context.Customer.Remove(customer);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(CustomerModel item)
		{
			 _context.Customer.Update(item);
			await _context.SaveChangesAsync();
		}
	}
}
