using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models.EssentialGoodsViewModel;
using WebAPI.Models.HtmlComponents;
using WebAPI.Support;

namespace WebAPI.Controllers.Web.EssentialGoods
{
    [Route("[controller]")]
    public class BrandsController : Controller
    {

        private readonly BrandBusinessObject _bo = new BrandBusinessObject();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fas fa-certificate", Action="Index", Controller="Brands", Text = "Brands"}
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var lst = new List<BrandViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(BrandViewModel.Parse(item));
            }

            ViewData["Title"] = "Brands";
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

            var vm = BrandViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Brand";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Create", Controller = "Brands", Icon = "fa-search", Text = "Detail" });

            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "New Brand";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Create", Controller = "Brands", Icon = "fa-plus", Text = "Create" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToModel();
                var createOperation = await _bo.CreateAsync(model);
                if (!createOperation.Success) return OperationErrorBackToIndex(createOperation.Exception);
                if (!createOperation.Result)
                {
                    TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, createOperation.Message);
                    ViewData["Title"] = "Create Company";
                    var crumbs = GetCrumbs();
                    crumbs.Add(new BreadCrumb() { Action = "Create", Controller = "Brands", Icon = "fa-plus", Text = "Create" });
                    ViewData["BreadCrumbs"] = crumbs;

                    return View();
                }
                else return OperationSuccess("The record was successfuly created");
            }
            return View(vm);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return RecordNotFound();

            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var vm = BrandViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Edit Brand";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Brands", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BrandViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);
                if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                if (getOperation.Result == null) return RecordNotFound();
                var result = getOperation.Result;
                if (!vm.CompareToModel(result))
                {
                    result = vm.ToModel(result);
                    var updateOperation = await _bo.UpdateAsync(result);
                    if (!updateOperation.Success)
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