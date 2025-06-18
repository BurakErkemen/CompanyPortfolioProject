using AdminPage.Models;
using AdminPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            var projects = await _projectService.GetAllAsync("Projects", userId);
            return View(projects);
        }

        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProjectsModel model)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");


                model.UserId = userId;
                model.ProjectId = Guid.NewGuid().ToString(); // ID'yi burada üret
                await _projectService.AddAsync("Projects", model, userId);
                TempData["Success"] = "Proje başarıyla kaydedildi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.ToString();

                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Update(string projectId)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");

                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                var project = await _projectService.GetByIdAsync("Projects", projectId, userId!);

                return View(project);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Proje bilgileri getirilirken bir hata meydana geldi. {ex.ToString()}";
                return RedirectToAction("Index","Project");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProjectsModel model)
        {
            var userId = HttpContext.Session.GetString("_UserId");

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            if (ModelState.IsValid)
            {
                model.UserId = userId!;
                await _projectService.UpdateAsync("Projects", model.ProjectId, model, userId!);
                TempData["Success"] = "Proje başarıyla güncellendi.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string projectId)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            await _projectService.DeleteAsync("Projects", projectId, userId!);
            TempData["Success"] = "Proje başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
