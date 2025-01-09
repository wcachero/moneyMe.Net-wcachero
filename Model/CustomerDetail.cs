using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class CustomerDetail
    {
        [Key]
        public Guid Id { get; set; } =new Guid();
        public decimal RequiredAmount { get; set; }
        public int Term {  get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateofBirth { get; set; } = DateTimeOffset.Now;
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Product { get; set; }
        public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.Now;
    }
}
