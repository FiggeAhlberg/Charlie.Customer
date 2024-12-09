﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlie.Customer.Service
{
    public interface ICustomerService
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T item);
        Task RemoveAsync(int id);
        Task UpdateAsync(T item);
       
    }
}
