﻿using AdminPage.Models;
using AdminPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace AdminPage.Controllers
{
    public class TeamController : Controller
    {
        private readonly TeamService _teamService;

        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            var list = await _teamService.GetAllAsync("Team", userId);
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            var team = await _teamService.GetByUserIdAsync("Team", userId);
            return View(team ?? new TeamModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(TeamModel model)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");

            model.UserId = userId;
            await _teamService.AddAsync("Team", model, userId);
            TempData["Success"] = "Ekip üyesi başarıyla eklendi.";
            return RedirectToAction("Index","Team");
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            var team = await _teamService.GetByIdAsync("Team", id, userId!);
            if (team == null)
                return RedirectToAction("Index");

            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TeamModel model)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Authentication");


            model.UserId = userId;
            await _teamService.UpdateAsync("Team", model.TeamId, model, userId);
            TempData["Success"] = "Ekip üyesi başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                await _teamService.DeleteAsync("Team", id, userId);
            }
            return RedirectToAction("Index");
        }
    }
}
