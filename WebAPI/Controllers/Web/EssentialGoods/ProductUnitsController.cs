﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using WebAPI.Models;
using WebAPI.Models.CommercialViewModel;
using WebAPI.Models.EssentialGoodsViewModel;
using WebAPI.Models.HtmlComponents;
using WebAPI.Support;

namespace WebAPI.Controllers.Web.EssentialGoods
{
    [Route("[controller]")]
    public class ProductUnitsController : Controller
    {
        private readonly ProductUnitBusinessObject _bo = new ProductUnitBusinessObject();
        private readonly EstablishmentBusinessObject _ebo = new EstablishmentBusinessObject();
        private readonly ProductModelBusinessObject _pmbo = new ProductModelBusinessObject();
        private readonly ShoppingBasketBusinessObject _sbbo = new ShoppingBasketBusinessObject();


        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fa-shish-kebab", Action="Index", Controller="Dishes", Text = "Dishes"}
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


        private async Task<List<EstablishmentViewModel>> GetEstablishmentViewModels(List<Guid> ids)
        {
            var filterOperation = await _ebo.FilterAsync(x => ids.Contains(x.Id));
            var eList = new List<EstablishmentViewModel>();
            foreach (var item in filterOperation.Result)
            {
                eList.Add(EstablishmentViewModel.Parse(item));
            }
            return eList;
        }

        private async Task<EstablishmentViewModel> GetEstablishmentViewModel(Guid id)
        {
            var getOperation = await _ebo.ReadAsync(id);
            return EstablishmentViewModel.Parse(getOperation.Result);
        }

        private async Task<List<ProductModelViewModel>> GetProductViewModels(List<Guid> ids)
        {
            var filterOperation = await _pmbo.FilterAsync(x => ids.Contains(x.Id));
            var eList = new List<ProductModelViewModel>();
            foreach (var item in filterOperation.Result)
            {
                eList.Add(ProductModelViewModel.Parse(item));
            }
            return eList;
        }

        private async Task<ProductModelViewModel> GetProductModelViewModel(Guid id)
        {
            var getOperation = await _pmbo.ReadAsync(id);
            return ProductModelViewModel.Parse(getOperation.Result);
        }

        private async Task<List<ShoppingBasketViewModel>> GetShoppingBasketViewModels(List<Guid> ids)
        {
            var filterOperation = await _sbbo.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<ShoppingBasketViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(ShoppingBasketViewModel.Parse(item));
            }
            return drList;
        }

        private async Task<ShoppingBasketViewModel> GetShoppingBasketViewModel(Guid id)
        {
            var getOperation = await _sbbo.ReadAsync(id);
            return ShoppingBasketViewModel.Parse(getOperation.Result);
        }

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
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "ProductUnits", "Details" };
            return View(vm);
        }

        [HttpGet("/create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Edit ProductUnit";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "ProductUnits", "Create" };
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