// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EMP.Sts.Models;
using System.Security.Claims;

namespace IdentityServer4.Quickstart.UI
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            IIdentityServerInteractionService interaction,
            IWebHostEnvironment environment,
            ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager)
        {
            // IWebHostEnvironment env;
            // env.IsDevelopment();

            _interaction = interaction;
            _environment = environment;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier) // will give the user's userId
            // var userName =  User.FindFirstValue(ClaimTypes.Name) // will give the user's userName
            // var userId =  User.FindFirst(ClaimTypes.NameIdentifier);
            // var userName =  User.FindFirstValue(ClaimTypes.Name);

            var userId = User.FindFirst(c =>
            {
                return c.Type == ClaimTypes.NameIdentifier;
            });
            var userName = User.FindFirst(c =>
            {
                return c.Type == ClaimTypes.Name;
            });


            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            string userEmail = applicationUser?.Email; // will give the user's Email

            return View();
            // if (_environment.IsDevelopment())
            // {
            //     // only show in development
            //     return View();
            // }

            // _logger.LogInformation("Homepage is disabled in production. Returning 404.");
            // return NotFound();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;

                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return View("Error", vm);
        }
    }
}