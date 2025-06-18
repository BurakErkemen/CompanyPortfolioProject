using AdminPage.Models;
using AdminPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ServicesService _servicesService;

        public ServiceController(ServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                var list = await _servicesService.GetAllAsync("Services", userId);
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Hizmetler getirilirken bir hata meydana geldi {ex.ToString()}";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Save()
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ServicesModel model)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                model.UserId = userId;
                await _servicesService.AddAsync("Services", model, userId);
                TempData["Success"] = "Hizmet başarıyla eklendi.";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Hizmet eklenirken bir hata oluştu: {ex.Message}";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                var services= await _servicesService.GetByIdAsync("Services", id, userId);
                if (services == null)
                {
                    TempData["Error"] = "Hizmet bulunamadı.";
                    return RedirectToAction("Index");
                }
                return View(services);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Hizmet bilgileri getirilirken bir hata gerçekleşti {ex.ToString()}";
                return RedirectToAction("Index");
            }
            }

        [HttpPost]
        public async Task<IActionResult> Update(ServicesModel model)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                model.UserId = userId;
                await _servicesService.UpdateAsync("Services", model.ServicesId, model, userId!);
                TempData["Success"] = "Hizmet başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Hizmet güncellenirken bir hata oluştu: {ex.Message}";
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                await _servicesService.DeleteAsync("Services", id, userId);
                TempData["Success"] = "Hizmet silindi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Hizmet silinirken bir hata meydana geldi {ex.ToString()}";
                return RedirectToAction("Index","Home");
            }
        }
    }
}
