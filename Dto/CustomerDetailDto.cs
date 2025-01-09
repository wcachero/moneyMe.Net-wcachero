using System;

namespace WebApplication2.Model
{
    public class CustomerDetailDto
    {
        public decimal RequiredAmount { get; set; }
        public int Term { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateofBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Product { get; set; }
    }
}
