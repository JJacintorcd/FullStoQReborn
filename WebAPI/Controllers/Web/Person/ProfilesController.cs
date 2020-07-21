﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using WebAPI.Models;
using WebAPI.Models.PersonViewModel;

namespace WebAPI.Controllers.Web.Person
{
    [Route("[controller]")]
    public class ProfilesController : Controller
    {
        private readonly ProfileBusinessObject _bo = new ProfileBusinessObject();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var lst = new List<ProfileViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(ProfileViewModel.Parse(item));
            }
            ViewData["Title"] = "Profiles";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Profiles" };
            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", getOperation.Exception.Message);
            if (getOperation.Result == null) return NotFound();
            var vm = ProfileViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Profile";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Profiles", "Detail" };
            return View(vm);
        }

        [HttpGet("/new")]
        public IActionResult New()
        {
            ViewData["Title"] = "Edit Profile";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Profiles", "New" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToProfile();
                var createOperation = await _bo.CreateAsync(model);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [HttpGet("/edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = ProfileViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Edit Profile";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Profiles", "Edit" };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();
                var result = getOperation.Result;
                if (vm.BirthDate != result.BirthDate && vm.FirstName != result.FirstName && vm.LastName != result.LastName && vm.PhoneNumber != result.PhoneNumber && vm.VatNumber != result.VatNumber)
                {
                    result = vm.ToProfile();
                    var updateOperation = await _bo.UpdateAsync(result);
                    if (!updateOperation.Success) return View("Error", new ErrorViewModel() { RequestId = updateOperation.Exception.Message });
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}