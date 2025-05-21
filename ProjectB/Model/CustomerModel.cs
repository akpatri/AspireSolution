using System.ComponentModel.DataAnnotations;

namespace ProjectB.Model
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^[1-9][0-9]{9}$")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

