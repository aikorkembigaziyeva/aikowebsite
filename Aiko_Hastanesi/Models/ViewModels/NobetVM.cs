using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiko_Hastanesi.Models.ViewModels
{
    public class NobetVM
    {
        public Nobet Nobet { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> AsistanList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> BolumList { get; set; }
    }
}
