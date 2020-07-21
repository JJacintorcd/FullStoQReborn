using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using WebAPI.Models;
using WebAPI.Models.CommercialViewModel;

namespace WebAPI.Controllers.Web.Commercial
{
    [Route("[controller]")]
    public class EstablishmentsController : Controller
    {
        private readonly EstablishmentBusinessObject _bo = new EstablishmentBusinessObject();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var lst = new List<EstablishmentViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(EstablishmentViewModel.Parse(item));
            }
            ViewData["Title"] = "Establishments";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Establishments" };
            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", getOperation.Exception.Message);
            if (getOperation.Result == null) return NotFound();
            var vm = EstablishmentViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Establishment";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Establishments", "Detail" };
            return View(vm);
        }

        [HttpGet("/new")]
        public IActionResult New()
        {
            ViewData["Title"] = "Edit Establishment";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Establishments", "New" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstablishmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToEstablishment();
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
            var vm = EstablishmentViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Edit Establishment";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Establishments", "Edit" };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EstablishmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();
                var result = getOperation.Result;
                if (vm.Address != result.Address && vm.ClosingDays != result.ClosingDays && vm.ClosingHours != result.ClosingHours && vm.OpeningHours != result.OpeningHours && vm.CompanyId != result.CompanyId && vm.RegionId != result.RegionId)
                {
                    result = vm.ToEstablishment();
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