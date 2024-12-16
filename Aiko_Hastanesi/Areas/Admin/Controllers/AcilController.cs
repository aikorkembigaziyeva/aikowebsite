using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Models.ViewModels;
using Aiko_Hastanesi.Repository.IRepository;
using Aiko_Hastanesi.Rol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiko_Hastanesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Rol.Rol.Role_Admin)]
    public class AcilController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AcilController> _logger;
        public AcilController(
            IUnitOfWork unitOfWork,
            IEmailSender emailService,
            ILogger<AcilController> logger)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Acil> objAcilList = _unitOfWork.Acil
                .GetAll(includeProperties: "Bolum")
                .ToList();
            return View(objAcilList);
        }

        public IActionResult Upsert(int? id)
        {
            AcilVM acilVM = new()
            {
                BolumList = _unitOfWork.Bolum.GetAll().Select(d => new SelectListItem
                {
                    Text = d.Ad,
                    Value = d.Id.ToString()
                }),
                Acil = new Acil()
            };

            if (id == null || id == 0)
            {
                return View(acilVM);
            }
            else
            {
                acilVM.Acil = _unitOfWork.Acil.Get(u => u.Id == id);
                return View(acilVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpsertAsync(AcilVM acilVM)
        {
            if (ModelState.IsValid)
            {
                if (acilVM.Acil.Id == 0)
                {
                    acilVM.Acil.OluşturulmaTarihi = DateTime.Now;
                    _unitOfWork.Acil.Add(acilVM.Acil);

                    await SendEmergencyNotificationEmails(acilVM);
                }
                else
                {
                    var existingAcil = _unitOfWork.Acil.Get(u => u.Id == acilVM.Acil.Id);
                    if (existingAcil != null)
                    {
                        existingAcil.Aciklama = acilVM.Acil.Aciklama;
                        existingAcil.Konum = acilVM.Acil.Konum;
                        existingAcil.AcilTipi = acilVM.Acil.AcilTipi;
                        existingAcil.BolumId = acilVM.Acil.BolumId;
                        existingAcil.OluşturulmaTarihi = DateTime.Now;
                        _unitOfWork.Acil.Update(existingAcil);
                    }
                }

                _unitOfWork.Save();
                TempData["success"] = "Acil Başarıyla Kaydedildi!";
                return RedirectToAction("Index");
            }
            else
            {
                acilVM.BolumList = _unitOfWork.Bolum.GetAll().Select(d => new SelectListItem
                {
                    Text = d.Ad,
                    Value = d.Id.ToString()
                });
                return View(acilVM);
            }
        }

        private async Task SendEmergencyNotificationEmails(AcilVM acilVM)
        {
            try
            {
                var assistantEmails = _unitOfWork.Asistan.GetAll()
                    .Select(a => a.Email)
                    .Where(email => !string.IsNullOrWhiteSpace(email))
                    .Distinct()
                    .ToList();

                _logger.LogInformation($"Found {assistantEmails.Count} assistant emails to notify");

                if (!assistantEmails.Any())
                {
                    _logger.LogWarning("No assistant emails found to send emergency notification");
                    return;
                }

                var bolum = _unitOfWork.Bolum.Get(b => b.Id == acilVM.Acil.BolumId);
                var subject = $"Yeni Acil Durum - {acilVM.Acil.AcilTipi}";
                var body = $@"
                    <h2>Yeni Acil Durum Bildirildi</h2>
                    <p><strong>Açıklama:</strong> {acilVM.Acil.Aciklama}</p>
                    <p><strong>Konum:</strong> {acilVM.Acil.Konum}</p>
                    <p><strong>Acil Tipi:</strong> {acilVM.Acil.AcilTipi}</p>
                    <p><strong>Bölüm:</strong> {bolum?.Ad}</p>
                    <p><strong>Oluşturulma Tarihi:</strong> {acilVM.Acil.OluşturulmaTarihi}</p>
                ";

                var emailTasks = assistantEmails.Select(async email =>
                {
                    try
                    {
                        await _emailSender.SendEmailAsync(email, subject, body);
                        _logger.LogInformation($"Successfully sent emergency email to {email}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Failed to send emergency email to {email}");
                    }
                });

                await Task.WhenAll(emailTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in sending emergency notification emails");
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var acilToDelete = _unitOfWork.Acil.Get(u => u.Id == id, includeProperties: "Bolum");

            if (acilToDelete == null)
            {
                return NotFound();
            }

            return View(acilToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var acilToDelete = _unitOfWork.Acil.Get(u => u.Id == id);

            if (acilToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.Acil.Remove(acilToDelete);
            _unitOfWork.Save();

            TempData["success"] = "Acil Durumu başarıyla silindi.";
            return RedirectToAction("Index");
        }

    }
}
