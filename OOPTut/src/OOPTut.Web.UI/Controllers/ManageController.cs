﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOPTut.Core.Users;

namespace OOPTut.Web.UI.Controllers
{
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // [Authorize(Roles ="Admin, TeamLeader")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateRole()
        {
            ViewBag.RoleList = await _roleManager.Roles.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(model);
                return RedirectToAction("CreateRole");
            }
            return View(model);
        }




        public async Task<IActionResult> RolesListPartial()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
    }
}