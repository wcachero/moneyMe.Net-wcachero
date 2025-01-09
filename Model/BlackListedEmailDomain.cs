using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication2.Model
{
    public class BlackListedEmailDomain
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string DomainName { get; set; } = "";
        public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.Now;
        public bool isActive { get; set; }
    }
}
