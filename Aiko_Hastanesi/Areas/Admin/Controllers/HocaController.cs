using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Aiko_Hastanesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Rol.Rol.Role_Admin)]
    public class HocaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HocaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Hoca> objHocaList = _unitOfWork.Hoca.GetAll().ToList();
            return View(objHocaList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Hoca obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Hoca.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Hoca created successfully";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Hoca? doctorFromDb = _unitOfWork.Hoca.Get(u => u.Id == id);

            if (doctorFromDb == null)
            {
                return NotFound();
            }
            return View(doctorFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Hoca obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Hoca.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Hoca updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Hoca? hocaFromDb = _unitOfWork.Hoca.Get(u => u.Id == id);

            if (hocaFromDb == null)
            {
                return NotFound();
            }
            return View(hocaFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Hoca? obj = _unitOfWork.Hoca.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Hoca.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Hoca deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
