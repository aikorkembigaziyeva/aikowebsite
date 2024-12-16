using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiko_Hastanesi.Models.ViewModels
{
    public class RandevuVM
    {
        public Randevu Randevu { get; set; }

        public IEnumerable<SelectListItem>? HocaMusaitlikList { get; set; }

        public RandevuVM()
        {
            Randevu = new Randevu();
        }
    }
}
