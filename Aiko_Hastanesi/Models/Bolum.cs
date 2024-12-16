using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Aiko_Hastanesi.Models
{
    public class Bolum
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ad { get; set; }
        [Required]
        public int YatakSayisi { get; set; }
        public int HocaId{ get; set; }

        [ForeignKey(nameof(HocaId))]
        [ValidateNever]
        public Hoca Hoca { get; set; }
    }
}
