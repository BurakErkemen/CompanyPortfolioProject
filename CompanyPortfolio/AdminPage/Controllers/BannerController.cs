using AdminPage.Models;
using AdminPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class BannerController : Controller
    {
        private readonly BannerService _bannerService;

        public BannerController(BannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var userId = HttpContext.Session.GetString("_UserId");

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            var banner = await _bannerService.GetAllAsync("Banners", userId);
            return View(banner);
        }


        [HttpGet]
        public async Task<IActionResult> Save()
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Banner oluşturulurken hata gerçekleşti. {ex.ToString()}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(BannerModel model)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");
                model.UserId = userId;
                model.BannerId = Guid.NewGuid().ToString(); // ID'yi burada üret
                await _bannerService.AddAsync("Banners", model, userId);
                TempData["Success"] = "Banner başarıyla kaydedildi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.ToString();
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

                var model = await _bannerService.GetByIdAsync("Banners", id, userId);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Banner bilgileri getirilirken hata gerçekleşti. {ex.ToString()}";
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(BannerModel model)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");

                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                model.UserId = userId;

                await _bannerService.UpdateAsync("Banners", model.BannerId, model, userId);

                TempData["Success"] = "Banner başarıyla güncellendi.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.ToString();

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

                await _bannerService.DeleteAsync("Banners", id, userId);

                TempData["Success"] = "Banner başarıyla silindi.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Banner silinirken bir hata gerçekleşti! {ex.ToString()}";
                return RedirectToAction("Index");
            }
        }


    }
}
