using Microsoft.EntityFrameworkCore;
using Charlie.Customer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charlie.Customer.DataAccess
{
	public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        public DbSet<CustomerModel> Customer{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CustomerModel>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
