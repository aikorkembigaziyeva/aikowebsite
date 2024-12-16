using System.ComponentModel.DataAnnotations;

namespace Aiko_Hastanesi.Models
{
    public class Hoca
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AdSoyad { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "15 Karakter olmalıdır")]
        public string Telefon { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Adres { get; set; } = string.Empty;
    }
}
