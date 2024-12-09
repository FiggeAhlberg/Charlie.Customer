﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlie.Customer.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(CustomerDbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.Find(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToList();
        }

        public async Task AddAsync(T item)
        {
            await _dbSet.Add(item);
        }

        public async Task RemoveAsync(int id)
        {
            await _dbSet.Remove(GetByIdAsync(id));
        }

        public async Task UpdateAsync(T item)
        {
            await _dbSet.Update(item);
        }
    }
}
