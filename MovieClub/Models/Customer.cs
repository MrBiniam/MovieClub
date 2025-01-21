using MovieClub.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;



namespace MovieClub.Models
{


    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public bool IsBlocked { get; set; } = false; // Default to not blocked

        // Foreign key to link with ASP.NET Identity user
        public string ApplicationUserId { get; set; }
        public IdentityUser ApplicationUser { get; set; } // Navigation property
    }
}