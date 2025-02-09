using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomersAPI.Data;
using CustomersAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace CustomersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  GET: api/customers - Get all customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        //  GET: api/customers/5 - Get customer by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();
            return customer;
        }

        //  POST: api/customers - create new customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Added; // ✅ מגדיר שהנתון חדש

            _context.Customers.Add(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "A concurrency issue occurred. Please try again." }); // ✅ מונע קריסה
            }

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, customer);
        }


        //  PUT: api/customers/5 - Update customer by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            // Ensuring that the provided ID matches the customer ID in the request body
            if (id != customer.CustomerID)
            {
                return BadRequest(new { message = "CustomerID in URL and body do not match." });
            }

            // Checking if the customer exists before updating
            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null)
            {
                return NotFound(new { message = "Customer not found." });
            }

            // Updating only the necessary fields to prevent overriding other data
            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.BillToCustomerID = customer.BillToCustomerID;
            existingCustomer.CustomerCategoryID = customer.CustomerCategoryID;
            existingCustomer.BuyingGroupID = customer.BuyingGroupID;
            existingCustomer.PrimaryContactPersonID = customer.PrimaryContactPersonID;
            existingCustomer.AlternateContactPersonID = customer.AlternateContactPersonID;
            existingCustomer.DeliveryMethodID = customer.DeliveryMethodID;
            existingCustomer.DeliveryCityID = customer.DeliveryCityID;
            existingCustomer.PostalCityID = customer.PostalCityID;
            existingCustomer.CreditLimit = customer.CreditLimit;
            existingCustomer.StandardDiscountPercentage = customer.StandardDiscountPercentage;
            existingCustomer.IsStatementSent = customer.IsStatementSent;
            existingCustomer.IsOnCreditHold = customer.IsOnCreditHold;
            existingCustomer.PaymentDays = customer.PaymentDays;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.FaxNumber = customer.FaxNumber;
            existingCustomer.DeliveryRun = customer.DeliveryRun;
            existingCustomer.RunPosition = customer.RunPosition;
            existingCustomer.WebsiteURL = customer.WebsiteURL;
            existingCustomer.DeliveryAddressLine1 = customer.DeliveryAddressLine1;
            existingCustomer.DeliveryAddressLine2 = customer.DeliveryAddressLine2;
            existingCustomer.DeliveryPostalCode = customer.DeliveryPostalCode;
            existingCustomer.DeliveryLocation = new Point(customer.Coordinates[0], customer.Coordinates[1]) { SRID = 4326 };
            existingCustomer.PostalAddressLine1 = customer.PostalAddressLine1;
            existingCustomer.PostalAddressLine2 = customer.PostalAddressLine2;
            existingCustomer.PostalPostalCode = customer.PostalPostalCode;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "A concurrency issue occurred. Please try again." });
            }

            return Ok(existingCustomer);
        }


        //  DELETE: api/customers/5 - Delete customer by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
