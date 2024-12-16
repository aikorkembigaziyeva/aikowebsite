using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Models.ViewModels;
using Aiko_Hastanesi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiko_Hastanesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NobetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public NobetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = Rol.Rol.Role_Admin)]
        public IActionResult Index()
        {
            List<Nobet> objNobetList = _unitOfWork.Nobet
           .GetAll(includeProperties: "Asistan,Bolum")
           .ToList();
            return View(objNobetList);
        }

        [Authorize(Roles = Rol.Rol.Role_Admin)]
        public IActionResult Upsert(int? id)
        {
            NobetVM assistantnobetVM = new()
            {
                AsistanList = _unitOfWork.Asistan.GetAll().Select(a => new SelectListItem
                {
                    Text = a.AdSoyad,
                    Value = a.Id.ToString()
                }),

                BolumList = _unitOfWork.Bolum.GetAll().Select(d => new SelectListItem
                {
                    Text = d.Ad,
                    Value = d.Id.ToString()
                }),

                Nobet = new Nobet()
            };

            if (id == null || id == 0)
            {
                assistantnobetVM.Nobet.Tarih = DateTime.Now.Date;
                return View(assistantnobetVM);
            }
            else
            {
                assistantnobetVM.Nobet = _unitOfWork.Nobet.Get(u => u.Id == id);
                return View(assistantnobetVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(NobetVM nobetVM)
        {
            if (ModelState.IsValid)
            {
                if (nobetVM.Nobet.Id == 0)
                {
                    _unitOfWork.Nobet.Add(nobetVM.Nobet);
                }
                else
                {
                    var existingNobet = _unitOfWork.Nobet.Get(n => n.Id == nobetVM.Nobet.Id);
                    if (existingNobet != null)
                    {
                        existingNobet.AsistanID = nobetVM.Nobet.AsistanID;
                        existingNobet.BolumID = nobetVM.Nobet.BolumID;
                        existingNobet.Tarih = nobetVM.Nobet.Tarih;
                        existingNobet.BaslangicSaati = nobetVM.Nobet.BaslangicSaati;
                        existingNobet.BitisSaati = nobetVM.Nobet.BitisSaati;

                        _unitOfWork.Nobet.Update(existingNobet);
                    }
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(nobetVM);
        }

        [Authorize(Roles = Rol.Rol.Role_Admin)]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(new { success = false, message = "ID is required" });
            }
            var NobetToBeDeleted = _unitOfWork.Nobet.Get(u => u.Id == id);
            if (NobetToBeDeleted == null)
            {
                return NotFound(new { success = false, message = "Record not found" });
            }
            return View(NobetToBeDeleted);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var NobetToBeDeleted = _unitOfWork.Nobet.Get(u => u.Id == id);
            if (NobetToBeDeleted == null)
            {
                return NotFound(new { success = false, message = "Record not found" });
            }
            _unitOfWork.Nobet.Remove(NobetToBeDeleted);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Calendar()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Nobet> objNobetList = _unitOfWork.Nobet
            .GetAll(includeProperties: "Asistan,Bolum")
            .ToList();
            return Json(new { data = objNobetList });
        }


        [HttpGet]
        public IActionResult GetAllNobet()
        {
            try
            {
                var assistantList = _unitOfWork.Nobet.GetAll(includeProperties: "Asistan,Bolum")
                    .Select(n => new
                    {
                        id = n.Id,
                        title = $"{n.Asistan?.AdSoyad ?? "Bilinmiyor"} - {n.Bolum?.Ad ?? "Bilinmiyor"}",
                        start = n.Tarih.Add(n.BaslangicSaati.TimeOfDay).ToString("yyyy-MM-dd'T'HH:mm:ss"),
                        end = n.Tarih.Add(n.BitisSaati.TimeOfDay).ToString("yyyy-MM-dd'T'HH:mm:ss"),
                        asistanName = n.Asistan?.AdSoyad ?? "Bilinmiyor",
                        departmentName = n.Bolum?.Ad ?? "Bilinmiyor",
                    })
                    .ToList();

                return Json(assistantList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Nöbet verileri alınırken hata oluştu", details = ex.Message });
            }
        }

        #endregion
    }
}
