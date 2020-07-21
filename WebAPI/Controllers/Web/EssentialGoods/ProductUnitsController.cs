using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using WebAPI.Models;
using WebAPI.Models.EssentialGoodsViewModel;

namespace WebAPI.Controllers.Web.EssentialGoods
{
    [Route("[controller]")]
    public class ProductUnitsController : Controller
    {
        private readonly ProductUnitBusinessObject _bo = new ProductUnitBusinessObject();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var lst = new List<ProductUnitViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(ProductUnitViewModel.Parse(item));
            }
            ViewData["Title"] = "ProductUnits";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "ProductUnits" };
            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", getOperation.Exception.Message);
            if (getOperation.Result == null) return NotFound();
            var vm = ProductUnitViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "ProductUnit";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "ProductUnits", "Detail" };
            return View(vm);
        }

        [HttpGet("/new")]
        public IActionResult New()
        {
            ViewData["Title"] = "Edit ProductUnit";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "ProductUnits", "New" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductUnitViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToProductUnit();
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
            var vm = ProductUnitViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Edit ProductUnit";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "ProductUnits", "Edit" };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductUnitViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();
                var result = getOperation.Result;
                if (vm.IsReserved != result.IsReserved && vm.SerialNumber != result.SerialNumber && vm.ProductModelId != result.ProductModelId)
                {
                    result = vm.ToProductUnit();
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