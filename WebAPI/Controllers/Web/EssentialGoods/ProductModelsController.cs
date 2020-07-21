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
    public class ProductModelsController : Controller
    {
        private readonly ProductModelBusinessObject _bo = new ProductModelBusinessObject();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var lst = new List<ProductModelViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(ProductModelViewModel.Parse(item));
            }
            ViewData["Title"] = "ProductModels";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "ProductModels" };
            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", getOperation.Exception.Message);
            if (getOperation.Result == null) return NotFound();
            var vm = ProductModelViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "ProductModel";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "ProductModels", "Detail" };
            return View(vm);
        }

        [HttpGet("/new")]
        public IActionResult New()
        {
            ViewData["Title"] = "Edit ProductModel";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "ProductModels", "New" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModelViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToProductModel();
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
            var vm = ProductModelViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Edit ProductModel";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "ProductModels", "Edit" };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductModelViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();
                var result = getOperation.Result;
                if (vm.Name != result.Name && vm.BarCode != result.BarCode && vm.BrandId != result.BrandId && vm.CategoryId != result.CategoryId && vm.Price != result.Price && vm.Weight != result.Weight)
                {
                    result = vm.ToProductModel();
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