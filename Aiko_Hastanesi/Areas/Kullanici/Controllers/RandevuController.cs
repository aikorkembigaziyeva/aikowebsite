using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Models.ViewModels;
using Aiko_Hastanesi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiko_Hastanesi.Areas.Kullanici.Controllers
{
    [Area("Kullanici")]
    public class RandevuController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public RandevuController(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var hocaMusaitlikList = _unitOfWork.HocaMusaitlik
            .GetAll(includeProperties: "Hoca");

            return View(hocaMusaitlikList);
        }

        [HttpGet]
        public IActionResult AllRandevuListesi()
        {
            var randevular = _unitOfWork.Randevu.GetAll(
                includeProperties: "HocaMusaitlik,HocaMusaitlik.Hoca,ApplicationUser"
            );

            return View(randevular);
        }


        [HttpGet]
        public IActionResult Create(int hocaId)
        {
            var hocaMusaitlik = _unitOfWork.HocaMusaitlik
                .GetAll(includeProperties: "Hoca")
                .FirstOrDefault(h => h.Id == hocaId);

            if (hocaMusaitlik == null)
            {
                return NotFound("Seçilen zaman dilimi mevcut değil.");
            }

            var model = new RandevuVM
            {
                Randevu = new Randevu
                {
                    HocaMusaitlikID = hocaId,
                    HocaMusaitlik = hocaMusaitlik
                },
                HocaMusaitlikList = _unitOfWork.HocaMusaitlik
                    .GetAll(includeProperties: "Hoca")
                    .Select(h => new SelectListItem
                    {
                        Value = h.Id.ToString(),
                        Text = $"{h.Hoca.AdSoyad} - {h.Tarih.ToShortDateString()} ({h.BaslangicSaati.ToShortTimeString()})"
                    })
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RandevuVM model)
        {
            ModelState.Remove("HocaMusaitlikList");

            if (!ModelState.IsValid)
            {
                model.HocaMusaitlikList = _unitOfWork.HocaMusaitlik
                    .GetAll(includeProperties: "Hoca")
                    .Select(h => new SelectListItem
                    {
                        Value = h.Id.ToString(),
                        Text = $"{h.Hoca.AdSoyad} - {h.Tarih.ToShortDateString()} ({h.BaslangicSaati.ToShortTimeString()})"
                    });
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("Randevu oluşturmak için giriş yapmanız gerekiyor.");
            }

            var hocaMusaitlik = _unitOfWork.HocaMusaitlik
                .Get(h => h.Id == model.Randevu.HocaMusaitlikID, includeProperties: "Hoca");

            if (hocaMusaitlik == null)
            {
                ModelState.AddModelError("", "Seçilen zaman aralığı artık mevcut değil.");

                model.HocaMusaitlikList = _unitOfWork.HocaMusaitlik
                    .GetAll(includeProperties: "Hoca")
                    .Select(h => new SelectListItem
                    {
                        Value = h.Id.ToString(),
                        Text = $"{h.Hoca.AdSoyad} - {h.Tarih.ToShortDateString()} ({h.BaslangicSaati.ToShortTimeString()})"
                    });

                return View(model);
            }

            var randevu = new Randevu
            {
                HocaMusaitlikID = model.Randevu.HocaMusaitlikID,
                ApplicationUserId = user.Id,
                Sebep = model.Randevu.Sebep
            };

            try
            {
                _unitOfWork.Randevu.Add(randevu);
                _unitOfWork.Save();

                TempData["success"] = "Randevu başarıyla alındı!";

                return RedirectToAction("RandevuListesi");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Randevu oluşturulurken bir hata oluştu.");

                model.HocaMusaitlikList = _unitOfWork.HocaMusaitlik
                    .GetAll(includeProperties: "Hoca")
                    .Select(h => new SelectListItem
                    {
                        Value = h.Id.ToString(),
                        Text = $"{h.Hoca.AdSoyad} - {h.Tarih.ToShortDateString()} ({h.BaslangicSaati.ToShortTimeString()})"
                    });

                return View(model);
            }

        }


        [HttpGet]
        public async Task<IActionResult> RandevuListesi()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var randevuList = _unitOfWork.Randevu.GetAll(
                r => r.ApplicationUserId == user.Id,
                includeProperties: "HocaMusaitlik,HocaMusaitlik.Hoca");

            return View(randevuList);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var randevu = _unitOfWork.Randevu.Get(
                r => r.ID == id,
                includeProperties: "HocaMusaitlik,HocaMusaitlik.Hoca,ApplicationUser"
            );

            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        [HttpGet]
        public IActionResult Cancel(int id)
        {
            var randevu = _unitOfWork.Randevu.Get(r => r.ID == id);

            if (randevu == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;

            if (!User.IsInRole("Admin") && !User.IsInRole("Asistan"))
            {
                if (randevu.ApplicationUserId != user.Id)
                {
                    return Forbid();
                }
            }

            try
            {
                _unitOfWork.Randevu.Remove(randevu);
                _unitOfWork.Save();

                TempData["success"] = "Randevu başarıyla iptal edildi.";

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("AllRandevuListesi");
                }

                return RedirectToAction("RandevuListesi");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Randevuyu iptal ederken bir hata oluştu.";

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("AllRandevuListesi");
                }

                return RedirectToAction("RandevuListesi");
            }
        }
    }
}