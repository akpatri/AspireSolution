using ProjectB.Model;

namespace ProjectB.Repository
{
    public interface ICustomerRepository
    {
        Task<CustomerModel> GetCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();
        Task AddCustomerAsync(CustomerModel customer);
        Task UpdateCustomerAsync(CustomerModel customer);
        Task DeleteCustomerAsync(int id);
    }
}
