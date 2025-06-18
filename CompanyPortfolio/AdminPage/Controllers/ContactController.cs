using AdminPage.Models;
using AdminPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                var list = await _contactService.GetAllAsync("Contacts", userId);
                return View(list);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"İletişim bilgileri getirilirken bir hata meydana geldi {ex.ToString()}";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ContactModel model)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                model.UserId = userId;
                await _contactService.AddAsync("Contacts", model, userId);
                TempData["Success"] = "İletişim bilgileri başarıyla eklendi.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"İletişim bilgileri eklenirken bir hata meydana geldi {ex.ToString()}";
                return RedirectToAction("Index");
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

                var contact = await _contactService.GetByIdAsync("Contacts", id, userId);

                return View(contact);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"İletişim bilgisi bulunamadı.{ex.ToString()}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ContactModel model)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");
                model.UserId = userId;
                await _contactService.UpdateAsync("Contacts", model.ContactId ,model, userId);
                TempData["Success"] = "İletişim bilgileri başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"İletişim bilgileri güncellenirken bir hata meydana geldi {ex.ToString()}";
                return RedirectToAction("Index");
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

                await _contactService.DeleteAsync("Contacts", id, userId);

                TempData["Success"] = "İletişim bilgileri başarıyla silindi.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"İletişim bilgileri silinirken bir hata meydana geldi. {ex.ToString()}";

                return RedirectToAction("Index");
            }
        }
    }
}
