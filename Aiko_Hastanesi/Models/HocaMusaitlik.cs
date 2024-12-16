using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Aiko_Hastanesi.Models
{
    public class HocaMusaitlik
    {
        [Key]
        public int Id { get; set; }

        public int HocaID { get; set; }

        [ForeignKey(nameof(HocaID))]
        [ValidateNever]
        public Hoca Hoca { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Tarih { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime BaslangicSaati { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime BitisSaati { get; set; }
    }
}
