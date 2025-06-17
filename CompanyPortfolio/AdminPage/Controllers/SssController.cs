using AdminPage.Models;
using AdminPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class SssController : Controller
    {
        private readonly SssService _sssService;

        public SssController(SssService sssService)
        {
            _sssService = sssService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            var list = await _sssService.GetActiveSssAsync(userId);
            return View(list);
        }

        [HttpGet]
        public IActionResult Save()
        {
            return View(new SssModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(SssModel model)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");


            model.UserId = userId;
            await _sssService.AddAsync("SSS", model, userId);

            TempData["Success"] = "S.S.S. başarıyla eklendi.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            var model = await _sssService.GetByIdAsync("SSS", id, userId!);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SssModel model)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            if (!ModelState.IsValid)
                return View(model);

            model.UserId = userId;
            await _sssService.UpdateAsync("SSS", model.SssId, model, userId);

            TempData["Success"] = "S.S.S. başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                await _sssService.DeleteAsync("SSS", id, userId);
                TempData["Success"] = "S.S.S. başarıyla silindi.";
            }

            return RedirectToAction("Index");
        }
    }
}
