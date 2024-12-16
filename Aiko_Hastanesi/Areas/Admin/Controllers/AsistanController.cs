using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Aiko_Hastanesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Rol.Rol.Role_Admin)]
    public class AsistanController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AsistanController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Asistan> objAsistanList = _unitOfWork.Asistan.GetAll().ToList();
            return View(objAsistanList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Asistan obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Asistan.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Asistan created successfully";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Asistan? doctorFromDb = _unitOfWork.Asistan.Get(u => u.Id == id);

            if (doctorFromDb == null)
            {
                return NotFound();
            }
            return View(doctorFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Asistan obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Asistan.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Asistan updated successfully";
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

            Asistan? asistanFromDb = _unitOfWork.Asistan.Get(u => u.Id == id);

            if (asistanFromDb == null)
            {
                return NotFound();
            }
            return View(asistanFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Asistan? obj = _unitOfWork.Asistan.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Asistan.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Asistan deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
