using DomainEntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.CustomerService;

namespace OnionArchitecture.Controllers
{
    public class CustomerController : ControllerBase
    {
        #region Private Fields
        private readonly ICustomerService _customerService;
        #endregion

        #region CTOR & Init
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        #endregion

        [HttpGet(nameof(GetCustomer))]
        public IActionResult GetCustomer(int id)
        {
            var result = _customerService.GetCustomerById(id);
            if (result == null)
            {
                return BadRequest("No records found");
            }
            return Ok(result);
        }

        [HttpGet(nameof(GetAllCustomers))]
        public IActionResult GetAllCustomers()
        {
            var result = _customerService.GetAllCustomers();
            if (result == null)
            {
                return BadRequest("No records found");
            }
            return Ok(result);
        }

        [HttpPost(nameof(InsertCustomer))]
        public IActionResult InsertCustomer(Customer customer)
        {
            _customerService.InsertCustomer(customer);
            return Ok("Customer inserted");
        }

        [HttpPut(nameof(UpdateCustomer))]
        public IActionResult UpdateCustomer(Customer customer)
        {
            _customerService.UpdateCustomer(customer);
            return Ok("Customer updated");
        }

        [HttpDelete("Customer deleted")]
        public IActionResult DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
            return Ok("Customer deleted");
        }
    }
}
