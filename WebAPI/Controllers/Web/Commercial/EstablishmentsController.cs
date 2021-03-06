﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using WebAPI.Models;
using WebAPI.Models.CommercialViewModel;
using WebAPI.Models.HtmlComponents;
using WebAPI.Support;

namespace WebAPI.Controllers.Web.Commercial
{
    [Route("[controller]")]
    public class EstablishmentsController : Controller
    {
        private readonly EstablishmentBusinessObject _bo = new EstablishmentBusinessObject();
        private readonly RegionBusinessObject _rbo = new RegionBusinessObject();
        private readonly CompanyBusinessObject _cbo = new CompanyBusinessObject();


        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                  new BreadCrumb(){Icon = "fas fa-store", Action="Index", Controller="Establishments", Text = "Establishments"}
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

        private async Task<List<RegionViewModel>> GetRegionViewModels(List<Guid> ids)
        {
            var filterOperation = await _rbo.FilterAsync(x => ids.Contains(x.Id));
            var rList = new List<RegionViewModel>();
            foreach (var item in filterOperation.Result)
            {
                rList.Add(RegionViewModel.Parse(item));
            }
            return rList;
        }

        private async Task<List<CompanyViewModel>> GetCompanyViewModels(List<Guid> ids)
        {
            var filterOperation = await _cbo.FilterAsync(x => ids.Contains(x.Id));
            var cList = new List<CompanyViewModel>();
            foreach (var item in filterOperation.Result)
            {
                cList.Add(CompanyViewModel.Parse(item));
            }
            return cList;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var lst = new List<EstablishmentViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(EstablishmentViewModel.Parse(item));
            }

            var rList = await GetRegionViewModels(listOperation.Result.Select(x => x.RegionId).Distinct().ToList());
            var cList = await GetCompanyViewModels(listOperation.Result.Select(x => x.CompanyId).Distinct().ToList());
            ViewData["Regions"] = rList;
            ViewData["Companies"] = cList;

            ViewData["Title"] = "Establishments";
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

            var getROperation = await _rbo.ReadAsync(getOperation.Result.RegionId);
            if (!getROperation.Success) return OperationErrorBackToIndex(getROperation.Exception);
            if (getROperation.Result == null) return RecordNotFound();

            var getCOperation = await _cbo.ReadAsync(getOperation.Result.CompanyId);
            if (!getCOperation.Success) return OperationErrorBackToIndex(getCOperation.Exception);
            if (getCOperation.Result == null) return RecordNotFound();

            var vm = EstablishmentViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "Establishment";

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "Establishments", Icon = "fa-search", Text = "Detail" });

            ViewData["Region"] = RegionViewModel.Parse(getROperation.Result);
            ViewData["Company"] = CompanyViewModel.Parse(getCOperation.Result);
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        public void Draw(string type, string icon)
        {
            ViewData["Title"] = $"{type} Establishment";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = type, Controller = "Establishments", Icon = icon, Text = type });
            ViewData["BreadCrumbs"] = crumbs;
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var listROperation = await _rbo.ListNotDeletedAsync();
            if (!listROperation.Success) return OperationErrorBackToIndex(listROperation.Exception);
            var rList = new List<SelectListItem>();
            foreach (var item in listROperation.Result)
            {
                rList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            ViewBag.Regions = rList;

            var listCOperation = await _cbo.ListNotDeletedAsync();
            if (!listCOperation.Success) return OperationErrorBackToIndex(listCOperation.Exception);
            var cList = new List<SelectListItem>();
            foreach (var item in listCOperation.Result)
            {
                cList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            ViewBag.Companies = cList;

            Draw("Create", "fa-plus");
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstablishmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToModel();
                var createOperation = await _bo.CreateAsync(model);
                if (!createOperation.Success) return OperationErrorBackToIndex(createOperation.Exception);
                if (!createOperation.Result)
                {
                    TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, createOperation.Message);

                    var listCOperation = await _cbo.ListNotDeletedAsync();
                    if (!listCOperation.Success) return OperationErrorBackToIndex(listCOperation.Exception);
                    var cList = new List<SelectListItem>();
                    foreach (var item in listCOperation.Result)
                    {
                        cList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
                    }

                    var listROperation = await _rbo.ListNotDeletedAsync();
                    if (!listROperation.Success) return OperationErrorBackToIndex(listROperation.Exception);
                    var rList = new List<SelectListItem>();
                    foreach (var item in listROperation.Result)
                    {
                        rList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
                    }

                    ViewBag.Regions = rList;
                    ViewBag.Companies = cList;
                    Draw("Create", "fa-plus");
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

            var vm = EstablishmentViewModel.Parse(getOperation.Result);

            var listROperation = await _rbo.ListNotDeletedAsync();
            if (!listROperation.Success) return OperationErrorBackToIndex(listROperation.Exception);
            var rList = new List<SelectListItem>();
            foreach (var item in listROperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                if (item.Id == vm.RegionId) listItem.Selected = true;
                rList.Add(listItem);
            }
            ViewBag.Regions = rList;

            var listCOperation = await _cbo.ListNotDeletedAsync();
            if (!listCOperation.Success) return OperationErrorBackToIndex(listCOperation.Exception);
            var cList = new List<SelectListItem>();
            foreach (var item in listCOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                if (item.Id == vm.CompanyId) listItem.Selected = true;
                cList.Add(listItem);
            }
            ViewBag.Companies = cList;

            Draw("Edit", "fa-edit");
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EstablishmentViewModel vm)
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
                        getOperation = await _bo.ReadAsync((Guid)id);
                        if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                        if (getOperation.Result == null) return RecordNotFound();

                        var listROperation = await _rbo.ListNotDeletedAsync();
                        if (!listROperation.Success) return OperationErrorBackToIndex(listROperation.Exception);
                        var rList = new List<SelectListItem>();
                        foreach (var item in listROperation.Result)
                        {
                            var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                            if (item.Id == vm.RegionId) listItem.Selected = true;
                            rList.Add(listItem);
                        }
                        ViewBag.Regions = rList;

                        var listCOperation = await _cbo.ListNotDeletedAsync();
                        if (!listCOperation.Success) return OperationErrorBackToIndex(listCOperation.Exception);
                        var cList = new List<SelectListItem>();
                        foreach (var item in listCOperation.Result)
                        {
                            var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                            if (item.Id == vm.CompanyId) listItem.Selected = true;
                            cList.Add(listItem);
                        }
                        ViewBag.Companies = cList;

                        vm = EstablishmentViewModel.Parse(getOperation.Result);

                        Draw("Edit", "fa-edit");
                        return View(vm);
                    }
                    if (!updateOperation.Result)
                    {
                        TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, updateOperation.Message);
                        getOperation = await _bo.ReadAsync((Guid)id);
                        if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                        if (getOperation.Result == null) return RecordNotFound();
                        var listROperation = await _rbo.ListNotDeletedAsync();
                        if (!listROperation.Success) return OperationErrorBackToIndex(listROperation.Exception);
                        var rList = new List<SelectListItem>();
                        foreach (var item in listROperation.Result)
                        {
                            var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                            if (item.Id == vm.RegionId) listItem.Selected = true;
                            rList.Add(listItem);
                        }
                        ViewBag.Regions = rList;

                        var listCOperation = await _cbo.ListNotDeletedAsync();
                        if (!listCOperation.Success) return OperationErrorBackToIndex(listCOperation.Exception);
                        var cList = new List<SelectListItem>();
                        foreach (var item in listCOperation.Result)
                        {
                            var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                            if (item.Id == vm.CompanyId) listItem.Selected = true;
                            cList.Add(listItem);
                        }
                        ViewBag.Companies = cList;

                        vm = EstablishmentViewModel.Parse(getOperation.Result);

                        Draw("Edit", "fa-edit");
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