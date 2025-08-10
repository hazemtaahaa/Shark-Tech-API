using System.ComponentModel.DataAnnotations.Schema;

namespace Shark_Tech.DAL
{
    public class Address : BaseEntity<Guid>
    {

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        
        public string UserId { get; set; } // Foreign key to AppUser
        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; } // Navigation property to AppUser
        // Optional: You can add methods or additional properties as needed
        public override string ToString()
        {
            return $"{Street}, {City}, {State}, {ZipCode}, {Country}";
        }
    }
}