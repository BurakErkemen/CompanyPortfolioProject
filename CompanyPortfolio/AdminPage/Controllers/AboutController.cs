using AdminPage.Models;
using AdminPage.Service;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class AboutController : Controller
    {
        private readonly AboutServices _aboutServices;

        public AboutController(AboutServices aboutServices)
        {
            _aboutServices = aboutServices;
        }
        private string GetUserId()
        {
            var id = HttpContext.Session.GetString("_UserId");
            return id ?? "";
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = GetUserId(); // örnek olarak
            var all = await _aboutServices.GetAllAsync("about", userId);

            var about = all.FirstOrDefault();
            return View(about ?? new AboutModel());
        }
        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            var about = await _aboutServices.GetByUserIdAsync("about", userId);
            return View(about ?? new AboutModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(AboutModel model)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            model.UserId = userId;

            if (!string.IsNullOrEmpty(model.AboutId))
                await _aboutServices.UpdateAsync("about", model.AboutId, model, userId);
            else
            {
                model.CreatedAt = Timestamp.GetCurrentTimestamp();
                await _aboutServices.AddAsync("about", model, userId);
            }

            TempData["Success"] = "Hakkımda bilgisi başarıyla kaydedildi.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Authentication");
            }

            // Kullanıcıya ait about verisini getir
            var about = await _aboutServices.GetByUserIdAsync("about", userId);

            // Eğer veri yoksa boş model dön
            return View(about ?? new AboutModel { UserId = userId });
        }


        [HttpPost]
        public async Task<IActionResult> Update(AboutModel model)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Authentication");
            }


            // Firestore'dan mevcut kaydı al (gerekirse kontrol için)
            var existingAbout = await _aboutServices.GetByIdAsync("about", model.AboutId, userId);
            if (existingAbout == null)
            {
                ModelState.AddModelError("", "Güncellenecek kayıt bulunamadı.");
                return View(model);
            }

            // Güncelleme işlemi
            model.UserId = userId;
            model.CreatedAt = Timestamp.GetCurrentTimestamp();

            await _aboutServices.UpdateAsync("about", model.AboutId, model, userId);

            TempData["Success"] = "Hakkımda bilgisi başarıyla güncellendi.";
            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                await _aboutServices.DeleteAsync("about", id, userId);
                TempData["Success"] = "Hakkımda bilgisi silindi.";
            }
            return RedirectToAction("Index");
        }
    }
}
