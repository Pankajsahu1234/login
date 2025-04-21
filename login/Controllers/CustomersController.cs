using Microsoft.AspNetCore.Mvc;
using login.Data;
using login.Models.Entities;
using login.Models.Dtos;

namespace login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("signup")]
        public IActionResult SignUp(SignUpDto signUpDto)
        {
            var existingCustomer = _dbContext.Customers
                .FirstOrDefault(c => c.EmailOrPhone == signUpDto.EmailOrPhone);

            if (existingCustomer == null)
            {
                var otp = new Random().Next(100000, 999999).ToString();

                var newCustomer = new Customer
                {
                    Name = signUpDto.Name,
                    EmailOrPhone = signUpDto.EmailOrPhone,
                    Otp = otp
                };

                _dbContext.Customers.Add(newCustomer);
                _dbContext.SaveChanges();

                return Ok(new { Message = "Sign up successful. OTP sent.", OTP = otp });
            }

            return BadRequest("Customer already exists.");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var customer = _dbContext.Customers
                .FirstOrDefault(c => c.EmailOrPhone == loginDto.EmailOrPhone && c.Otp == loginDto.Otp);

            if (customer == null)
            {
                return Unauthorized("Invalid OTP or Email/Phone.");
            }

            return Ok(new { Message = "Login successful", Customer = customer });
        }
    }
}
