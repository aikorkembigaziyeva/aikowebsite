using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Aiko_Hastanesi.Models
{
    public class Nobet
    {
        [Key]
        public int Id { get; set; }

        public int AsistanID { get; set; }

        [ForeignKey(nameof(AsistanID))]
        [ValidateNever]
        public Asistan Asistan { get; set; }

        public int BolumID { get; set; }

        [ForeignKey(nameof(BolumID))]
        [ValidateNever]
        public Bolum Bolum { get; set; }

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
