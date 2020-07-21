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
    public class CategoriesController : Controller
    {
        private readonly CategoryBusinessObject _bo = new CategoryBusinessObject();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var lst = new List<CategoryViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(CategoryViewModel.Parse(item));
            }
            ViewData["Title"] = "Categorys";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Categorys" };
            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", getOperation.Exception.Message);
            if (getOperation.Result == null) return NotFound();
            var vm = CategoryViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Category";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Categorys", "Detail" };
            return View(vm);
        }

        [HttpGet("/new")]
        public IActionResult New()
        {
            ViewData["Title"] = "Edit Category";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Categorys", "New" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToCategory();
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
            var vm = CategoryViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Edit Category";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Categorys", "Edit" };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();
                var result = getOperation.Result;
                if (vm.Name != result.Name)
                {
                    result = vm.ToCategory();
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