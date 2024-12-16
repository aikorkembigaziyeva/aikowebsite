using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Models.ViewModels;
using Aiko_Hastanesi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiko_Hastanesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Rol.Rol.Role_Admin)]
    public class BolumController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BolumController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Bolum> objBolumList = _unitOfWork.Bolum
           .GetAll(includeProperties: "Hoca").ToList();
            return View(objBolumList);
        }

        public IActionResult Upsert(int? id)
        {
            BolumVM bolumVM = new()
            {
                HocaList = _unitOfWork.Hoca.GetAll().Select(a => new SelectListItem
                {
                    Text = a.AdSoyad,
                    Value = a.Id.ToString()
                }),

                Bolum = new Bolum()
            };

            if (id == null || id == 0)
            {
                return View(bolumVM);
            }
            else
            {
                bolumVM.Bolum = _unitOfWork.Bolum.Get(u => u.Id == id);
                return View(bolumVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(BolumVM bolumVM)
        {
            if (ModelState.IsValid)
            {
                if (bolumVM.Bolum.Id == 0)
                {
                    _unitOfWork.Bolum.Add(bolumVM.Bolum);
                }
                else
                {
                    _unitOfWork.Bolum.Update(bolumVM.Bolum);
                }

                _unitOfWork.Save();

                TempData["success"] = "Bölüm başarıyla Kaydedildi";
                return RedirectToAction("Index");
            }
            else
            {
                bolumVM.HocaList = _unitOfWork.Hoca.GetAll().Select(a => new SelectListItem
                {
                    Text = a.AdSoyad,
                    Value = a.Id.ToString()
                });
                return View(bolumVM);
            }

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var bolumToDelete = _unitOfWork.Bolum.Get(u => u.Id == id, includeProperties: "Hoca");

            if (bolumToDelete == null)
            {
                return NotFound();
            }

            return View(bolumToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var bolumToDelete = _unitOfWork.Bolum.Get(u => u.Id == id);

            if (bolumToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.Bolum.Remove(bolumToDelete);
            _unitOfWork.Save();

            TempData["success"] = "Bölüm başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
