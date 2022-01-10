using DomainEntityLayer.Models;
using RepositoryLayer.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.CustomerService
{
    public class CustomerService : ICustomerService
    {
        #region Private Fields
        private IRepository<Customer> _repository;
        #endregion

        #region CTOR & Init
        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }
        #endregion

        public void DeleteCustomer(int id)
        {
            Customer customer = _repository.Get(id);
            _repository.Delete(customer);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _repository.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return _repository.Get(id);
        }

        public void InsertCustomer(Customer customer)
        {
            _repository.Insert(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _repository.Update(customer);
        }
    }
}
