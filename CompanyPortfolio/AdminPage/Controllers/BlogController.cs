using AdminPage.Models;
using AdminPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public IActionResult Index()
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
                TempData["Error"] = $"Blog bilgileri getirilirken bir hata gerçekleşti. {ex.ToString()}";
                return RedirectToAction("Index", "Home");
            }
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
                TempData["Error"] = $"Blog oluşturulurken bir hata gerçekleşti. {ex.ToString()}";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Save(BlogModel blogModel)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");

                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");

                if (ModelState.IsValid)
                {
                    blogModel.UserId = userId;
                    await _blogService.AddAsync("Blogs", blogModel, userId);
                    TempData["Success"] = "Blog başarıyla kaydedildi.";
                    return RedirectToAction("Index");
                }
                return View(blogModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Blog kaydedilirken bir hata gerçekleşti. {ex.ToString()}";
                return View(blogModel);
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
                var blog = await _blogService.GetByIdAsync("Blogs", id, userId);
                if (blog == null)
                {
                    TempData["Error"] = "Blog bulunamadı.";
                    return RedirectToAction("Index");
                }
                return View(blog);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Blog güncellenirken bir hata gerçekleşti. {ex.ToString()}";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogModel blogModel)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_UserId");
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Authentication");
                if (ModelState.IsValid)
                {
                    blogModel.UserId = userId;
                    await _blogService.UpdateAsync("Blogs", blogModel.BlogId, blogModel, userId);
                    TempData["Success"] = "Blog başarıyla güncellendi.";
                    return RedirectToAction("Index");
                }
                return View(blogModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Blog güncellenirken bir hata gerçekleşti. {ex.ToString()}";
                return View(blogModel);
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
                
                await _blogService.DeleteAsync("Blogs", id, userId);

                TempData["Success"] = "Blog başarıyla silindi.";
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Blog silinirken bir hata gerçekleşti. {ex.ToString()}";
                
                return RedirectToAction("Index");
            }
        }


    }
}
