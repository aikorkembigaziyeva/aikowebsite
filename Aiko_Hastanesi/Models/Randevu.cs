using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Aiko_Hastanesi.Models
{
    public class Randevu
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Lütfen bir zaman dilimi seçiniz")]
        public int HocaMusaitlikID { get; set; }

        [ForeignKey(nameof(HocaMusaitlikID))]
        [ValidateNever]
        public HocaMusaitlik HocaMusaitlik { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public string? ApplicationUserId { get; set; }

        [Required(ErrorMessage = "Randevu nedeni zorunludur")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Açıklama 500 karakterden uzun olamaz")]
        public string Sebep { get; set; }
    }
}
