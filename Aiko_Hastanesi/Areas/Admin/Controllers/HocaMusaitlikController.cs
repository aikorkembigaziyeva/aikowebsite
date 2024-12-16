using Aiko_Hastanesi.Models.ViewModels;
using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiko_Hastanesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HocaMusaitlikController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HocaMusaitlikController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = Rol.Rol.Role_Admin)]
        public IActionResult Index()
        {
            List<HocaMusaitlik> objHocaMusaitlikList = _unitOfWork.HocaMusaitlik
           .GetAll(includeProperties: "Hoca")
           .ToList();
            return View(objHocaMusaitlikList);
        }
        [Authorize(Roles = Rol.Rol.Role_Admin)]
        public IActionResult Upsert(int? id)
        {
            HocaMusaitlikVM hocaMusaitlikVM = new()
            {
                HocaList = _unitOfWork.Hoca.GetAll().Select(a => new SelectListItem
                {
                    Text = a.AdSoyad,
                    Value = a.Id.ToString()
                }),

                HocaMusaitlik = new HocaMusaitlik()
            };

            if (id == null || id == 0)
            {
                hocaMusaitlikVM.HocaMusaitlik.Tarih = DateTime.Now.Date;
                return View(hocaMusaitlikVM);
            }
            else
            {
                hocaMusaitlikVM.HocaMusaitlik = _unitOfWork.HocaMusaitlik.Get(u => u.Id == id);
                return View(hocaMusaitlikVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(HocaMusaitlikVM hocaMusaitlikVM)
        {
            var existingHocaMusaitliks = _unitOfWork.HocaMusaitlik.GetAll()
                .Where(n => n.HocaID == hocaMusaitlikVM.HocaMusaitlik.HocaID &&
                            n.Tarih.Date == hocaMusaitlikVM.HocaMusaitlik.Tarih.Date &&
                            n.Id != hocaMusaitlikVM.HocaMusaitlik.Id)
                .ToList();

            bool hasOverlap = existingHocaMusaitliks.Any(existingHocaMusaitlik =>
                hocaMusaitlikVM.HocaMusaitlik.BaslangicSaati >= existingHocaMusaitlik.BaslangicSaati &&
                 hocaMusaitlikVM.HocaMusaitlik.BaslangicSaati < existingHocaMusaitlik.BitisSaati ||
                hocaMusaitlikVM.HocaMusaitlik.BitisSaati > existingHocaMusaitlik.BaslangicSaati &&
                 hocaMusaitlikVM.HocaMusaitlik.BitisSaati <= existingHocaMusaitlik.BitisSaati ||
                hocaMusaitlikVM.HocaMusaitlik.BaslangicSaati <= existingHocaMusaitlik.BaslangicSaati &&
                 hocaMusaitlikVM.HocaMusaitlik.BitisSaati >= existingHocaMusaitlik.BitisSaati);

            if (hasOverlap)
            {
                ModelState.AddModelError("", "Bu zaman aralığı, aynı doktora ait aynı tarihteki mevcut bir aralıkla çakışıyor.");
            }

            if (ModelState.IsValid)
            {
                if (hocaMusaitlikVM.HocaMusaitlik.Id == 0)
                {
                    _unitOfWork.HocaMusaitlik.Add(hocaMusaitlikVM.HocaMusaitlik);
                }
                else
                {
                    _unitOfWork.HocaMusaitlik.Update(hocaMusaitlikVM.HocaMusaitlik);
                }
                _unitOfWork.Save();
                TempData["success"] = "Time slot created/updated successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                hocaMusaitlikVM.HocaList = _unitOfWork.Hoca.GetAll().Select(a => new SelectListItem
                {
                    Text = a.AdSoyad,
                    Value = a.Id.ToString()
                });
                return View(hocaMusaitlikVM);
            }
        }

        [Authorize(Roles = Rol.Rol.Role_Admin)]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(new { success = false, message = "ID is required" });
            }
            var MusaitlikToBeDeleted = _unitOfWork.HocaMusaitlik.Get(u => u.Id == id);
            if (MusaitlikToBeDeleted == null)
            {
                return NotFound(new { success = false, message = "Record not found" });
            }
            return View(MusaitlikToBeDeleted);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var MusaitlikToBeDeleted = _unitOfWork.HocaMusaitlik.Get(u => u.Id == id);
            if (MusaitlikToBeDeleted == null)
            {
                return NotFound(new { success = false, message = "Record not found" });
            }
            _unitOfWork.HocaMusaitlik.Remove(MusaitlikToBeDeleted);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult MusaitlikProgrami()
        {
            var hocaMusaitlikList = _unitOfWork.HocaMusaitlik.GetAll(includeProperties: "Hoca")
            .GroupBy(h => h.Hoca.AdSoyad)
            .Select(group => new
            {
                HocaName = group.Key,
                Availabilities = group.Select(a => new
                {
                    Date = a.Tarih.ToString("dd/MM/yyyy"),
                    StartTime = a.BaslangicSaati.ToString("HH:mm"),
                    EndTime = a.BitisSaati.ToString("HH:mm")
                }).ToList()
            }).ToList();

            return View(hocaMusaitlikList);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<HocaMusaitlik> objHocaMusaitlikList = _unitOfWork.HocaMusaitlik
            .GetAll(includeProperties: "Hoca")
            .ToList();
            return Json(new { data = objHocaMusaitlikList });
        }


        [HttpGet]
        public IActionResult GetAllHocaMusaitlik()
        {
            try
            {
                var hocaMusaitlikList = _unitOfWork.HocaMusaitlik.GetAll(includeProperties: "Hoca,Department")
                    .Select(n => new
                    {
                        id = n.Id,
                        title = $"{n.Hoca?.AdSoyad ?? "Unknown"}",
                        start = n.Tarih.Add(n.BaslangicSaati.TimeOfDay).ToString("yyyy-MM-dd'T'HH:mm:ss"),
                        end = n.Tarih.Add(n.BitisSaati.TimeOfDay).ToString("yyyy-MM-dd'T'HH:mm:ss"),
                        HocaName = n.Hoca?.AdSoyad ?? "Unknown",
                    })
                    .ToList();

                return Json(hocaMusaitlikList);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = "Error retrieving HocaMusaitlik data", details = ex.Message });
            }
        }
        #endregion
    }
}
