using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using WebAPI.Models;
using WebAPI.Models.EssentialGoodsViewModel;
using WebAPI.Models.HtmlComponents;
using WebAPI.Support;

namespace WebAPI.Controllers.Web.EssentialGoods
{
    [Route("[controller]")]
    public class ProductModelsController : Controller
    {
        private readonly ProductModelBusinessObject _bo = new ProductModelBusinessObject();
        private readonly BrandBusinessObject _bro = new BrandBusinessObject();
        private readonly CategoryBusinessObject _cro = new CategoryBusinessObject();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fa-hat-chef", Action="Index", Controller="Courses", Text = "Courses"}
                };
        }

        private IActionResult RecordNotFound()
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Information, "The record was not found");
            return RedirectToAction(nameof(Index));
        }

        private IActionResult OperationErrorBackToIndex(Exception exception)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, exception);
            return RedirectToAction(nameof(Index));
        }

        private IActionResult OperationSuccess(string message)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Success, message);
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<BrandViewModel>> GetBrandViewModels(List<Guid> ids)
        {
            var filterOperation = await _bro.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<BrandViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(BrandViewModel.Parse(item));
            }
            return drList;
        }

        private async Task<BrandViewModel> GetBrandViewModel(Guid id)
        {
            var getOperation = await _bro.ReadAsync(id);
            return BrandViewModel.Parse(getOperation.Result);
        }

        private async Task<List<CategoryViewModel>> GetCategoryViewModels(List<Guid> ids)
        {
            var filterOperation = await _cro.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<CategoryViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(CategoryViewModel.Parse(item));
            }
            return drList;
        }

        private async Task<CategoryViewModel> GetCategoryViewModel(Guid id)
        {
            var getOperation = await _cro.ReadAsync(id);
            return CategoryViewModel.Parse(getOperation.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var lst = new List<ProductModelViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(ProductModelViewModel.Parse(item));
            }

            var bList = await GetBrandViewModels(listOperation.Result.Select(x => x.BrandId).Distinct().ToList());
            ViewData["DietaryRestrictions"] = bList;
            var cList = await GetCategoryViewModels(listOperation.Result.Select(x => x.CategoryId).Distinct().ToList());
            ViewData["DietaryRestrictions"] = cList;
            ViewData["Title"] = "ProductModels";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();

            return View(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);

            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var getCOperation = await _cro.ReadAsync(getOperation.Result.BrandId);
            if (!getCOperation.Success) return OperationErrorBackToIndex(getCOperation.Exception);
            if (getCOperation.Result == null) return RecordNotFound();

            var getBOperation = await _bro.ReadAsync(getOperation.Result.BrandId);
            if (!getBOperation.Success) return OperationErrorBackToIndex(getBOperation.Exception);
            if (getBOperation.Result == null) return RecordNotFound();

            var vm = ProductModelViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "ProductModel";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "ProductModels", Icon = "fa-search", Text = "Detail" });
            ViewData["Brand"] = BrandViewModel.Parse(getBOperation.Result);
            ViewData["Category"] = CategoryViewModel.Parse(getCOperation.Result);

            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            var listBOperation = await _bro.ListNotDeletedAsync();
            if (!listBOperation.Success) return OperationErrorBackToIndex(listBOperation.Exception);

            var listCOperation = await _cro.ListNotDeletedAsync();
            if (!listCOperation.Success) return OperationErrorBackToIndex(listCOperation.Exception);


            var bList = new List<SelectListItem>();
            foreach (var item in listBOperation.Result)
            {
                bList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }

            var cList = new List<SelectListItem>();
            foreach (var item in listCOperation.Result)
            {
                cList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }

            ViewBag.Brands = bList;
            ViewBag.Categories = cList;

            ViewData["Title"] = "Edit ProductModel";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Courses", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;

            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ProductModelViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToModel();
                var createOperation = await _bo.CreateAsync(model);
                if (!createOperation.Success) return OperationErrorBackToIndex(createOperation.Exception);
                else return OperationSuccess("The record was successfuly created");
            }
            return View(vm);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var vm = ProductModelViewModel.Parse(getOperation.Result);

            var listBOperation = await _bro.ListNotDeletedAsync();
            if (!listBOperation.Success) return OperationErrorBackToIndex(listBOperation.Exception);
            var listCOperation = await _cro.ListNotDeletedAsync();
            if (!listCOperation.Success) return OperationErrorBackToIndex(listCOperation.Exception);

            var bList = new List<SelectListItem>();
            foreach (var item in listBOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                if (item.Id == vm.BrandId) listItem.Selected = true;
                bList.Add(listItem);
            }
            ViewBag.Brands = bList;

            var cList = new List<SelectListItem>();
            foreach (var item in listCOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                if (item.Id == vm.BrandId) listItem.Selected = true;
                cList.Add(listItem);
            }
            ViewBag.Categories = cList;

            ViewData["Title"] = "Edit Course";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Courses", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;

            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductModelViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                if (!vm.CompareToModel(result))
                {
                    result = vm.ToModel();
                    var updateOperation = await _bo.UpdateAsync(result); if (!updateOperation.Success)
                    {

                        TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, updateOperation.Exception);
                    return View(vm);
                    }
                    else return OperationSuccess("The record was successfuly updated");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return OperationErrorBackToIndex(deleteOperation.Exception);
            else return OperationSuccess("The record was successfuly deleted");
        }
    }
}