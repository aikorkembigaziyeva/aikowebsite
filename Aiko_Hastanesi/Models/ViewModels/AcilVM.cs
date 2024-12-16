using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiko_Hastanesi.Models.ViewModels
{
    public class AcilVM
    {
        public Acil Acil { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> BolumList { get; set; }
        //public AcilVM()
        //{
        //    Acil = new Acil
        //    {
        //        Status = "Pending"
        //    };
        //}
    }
}
