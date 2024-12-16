using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Aiko_Hastanesi.Models
{
    public class Acil
    {
        public int Id { get; set; }

        [Required]
        public string Aciklama { get; set; }

        [Required]
        public string Konum { get; set; }

        [Required]
        public string AcilTipi { get; set; }

        [Required]
        public int BolumId { get; set; }

        [ForeignKey(nameof(BolumId))]
        [ValidateNever]
        public Bolum Bolum { get; set; }

        public DateTime OluşturulmaTarihi { get; set; } = DateTime.Now;
    }
}
