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
    public class BrandsController : Controller
    {
        private readonly BrandBusinessObject _bo = new BrandBusinessObject();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var lst = new List<BrandViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(BrandViewModel.Parse(item));
            }
            ViewData["Title"] = "Brands";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Brands" };
            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", getOperation.Exception.Message);
            if (getOperation.Result == null) return NotFound();
            var vm = BrandViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Brand";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Brands", "Detail" };
            return View(vm);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            ViewData["Title"] = "Edit Brand";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Brands", "New" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToBrand();
                var createOperation = await _bo.CreateAsync(model);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = BrandViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Edit Brand";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Brands", "Edit" };
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BrandViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();
                var result = getOperation.Result;
                if (vm.Name != result.Name)
                {
                    result = vm.ToBrand();
                    var updateOperation = await _bo.UpdateAsync(result);
                    if (!updateOperation.Success) return View("Error", new ErrorViewModel() { RequestId = updateOperation.Exception.Message });
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}