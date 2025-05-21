using Microsoft.EntityFrameworkCore;
using ProjectB.Data;
using ProjectB.Model;

namespace ProjectB.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MySqlDBContext _dbContext;
        public CustomerRepository(MySqlDBContext dbContext)
        {
            
            _dbContext = dbContext;
        }
        public async Task AddCustomerAsync(CustomerModel customer)
        {
            
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task UpdateCustomerAsync(CustomerModel customer)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
