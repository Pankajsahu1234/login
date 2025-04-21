namespace login.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? OTP { get; set; }
        public DateTime? OTPGeneratedAt { get; set; }
    }
}
