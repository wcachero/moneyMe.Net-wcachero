using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Model
{
    public class BlacklistedPhoneNumbers
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string PhoneNumber { get; set; }
        public DateTimeOffset DateAdded { get; set; } =DateTimeOffset.Now;
        public bool isActive { get; set; }
    }
}
