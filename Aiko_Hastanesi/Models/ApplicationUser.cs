using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Aiko_Hastanesi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? AdSoyad { get; set; }
        public string? Telefon { get; set; }
    }
}
