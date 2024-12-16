using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiko_Hastanesi.Models.ViewModels
{
    public class BolumVM
    {
        public Bolum Bolum { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> HocaList { get; set; }
    }
}
