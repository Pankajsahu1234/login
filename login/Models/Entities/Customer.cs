﻿namespace login.Models.Entities
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string EmailOrPhone { get; set; }
        public string Otp { get; set; }
    }
}
