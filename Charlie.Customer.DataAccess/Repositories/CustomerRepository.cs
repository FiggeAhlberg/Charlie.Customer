using Microsoft.EntityFrameworkCore;

namespace Charlie.Customer.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository<Customer>
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {   
            _context = context;
        }

		public async Task AddAsync(Customer item)
		{
			_context.Customer.AddAsync(item);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Customer>> GetAllAsync()
		{
			return await _context.Customer.ToListAsync();
		}

		public async Task<Customer> GetByIdAsync(int id)
		{
			return await _context.Customer.FindAsync(id);
		}

		public async Task RemoveAsync(int id)
		{
			var customer = await GetByIdAsync(id);
			_context.Customer.Remove(customer);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Customer item)
		{
			 _context.Customer.Update(item);
			await _context.SaveChangesAsync();
		}
	}
}
