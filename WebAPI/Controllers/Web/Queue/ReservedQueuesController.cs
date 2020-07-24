using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using Recodme.RD.FullStoQReborn.BusinessLayer.Queue;
using WebAPI.Models;
using WebAPI.Models.CommercialViewModel;
using WebAPI.Models.HtmlComponents;
using WebAPI.Models.PersonViewModel;
using WebAPI.Models.QueueViewModel;
using WebAPI.Support;

namespace WebAPI.Controllers.Web.Queue
{
    [Route("[controller]")]
    public class ReservedQueuesController : Controller
    {
        private readonly ReservedQueueBusinessObject _bo = new ReservedQueueBusinessObject();
        private readonly EstablishmentBusinessObject _ebo = new EstablishmentBusinessObject();
        private readonly ProfileBusinessObject _pbo = new ProfileBusinessObject();
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
                  new BreadCrumb(){Icon = "fa-users-medical", Action="Index", Controller="ReservedQueues", Text = "Reserved Queues"}
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
            var estList = new List<EstablishmentViewModel>();
            foreach (var item in filterOperation.Result)
            {
                estList.Add(EstablishmentViewModel.Parse(item));
            }
            return estList;
        }

        private async Task<EstablishmentViewModel> GetEstablishmentViewModel(Guid id)
        {
            var getOperation = await _ebo.ReadAsync(id);
            return EstablishmentViewModel.Parse(getOperation.Result);
        }

        private async Task<List<ProfileViewModel>> GetProfileViewModels(List<Guid> ids)
        {
            var filterOperation = await _pbo.FilterAsync(x => ids.Contains(x.Id));
            var profiList = new List<ProfileViewModel>();
            foreach (var item in filterOperation.Result)
            {
                profiList.Add(ProfileViewModel.Parse(item));
            }
            return profiList;
        }

        private async Task<ProfileViewModel> GetProfileViewModel(Guid id)
        {
            var getOperation = await _pbo.ReadAsync(id);
            return ProfileViewModel.Parse(getOperation.Result);
        }

        private async Task<List<CompanyViewModel>> GetCompanyViewModels(List<Guid> ids)
        {
            var filterOperation = await _cbo.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<CompanyViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(CompanyViewModel.Parse(item));
            }
            return drList;
        }
        private async Task<CompanyViewModel> GetCompanyViewModel(Guid id)
        {
            var getOperation = await _cbo.ReadAsync(id);
            return CompanyViewModel.Parse(getOperation.Result);
        }

        public void Draw(string type, string icon)
        {
            ViewData["Title"] = $"{type} - Reserved Queue";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = type, Controller = "ReservedQueues", Icon = icon, Text = type });
            ViewData["BreadCrumbs"] = crumbs;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var lst = new List<ReservedQueueViewModel>();
            foreach (var item in listOperation.Result)
            {
                var limitCheck = await _bo.TwoHourLimitReserveAsync(item.Id);
                if (limitCheck.Result && limitCheck.Success)
                    lst.Add(ReservedQueueViewModel.Parse(item));
            }

            var listEOperation = await _ebo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var elst = new List<EstablishmentViewModel>();
            foreach (var item in listEOperation.Result)
            {
                elst.Add(EstablishmentViewModel.Parse(item));
            }

            var estList = await GetEstablishmentViewModels(listOperation.Result.Select(x => x.EstablishmentId).Distinct().ToList());
            var profiList = await GetProfileViewModels(listOperation.Result.Select(x => x.ProfileId).Distinct().ToList());
            var cList = await GetCompanyViewModels(listEOperation.Result.Select(x => x.CompanyId).Distinct().ToList());
            ViewData["Companies"] = cList;
            ViewData["Establishments"] = estList;
            ViewData["Profiles"] = profiList;
            ViewData["Title"] = "Reserved Queues";
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

            var getEstOperation = await _ebo.ReadAsync(getOperation.Result.EstablishmentId);
            if (!getEstOperation.Success) return OperationErrorBackToIndex(getEstOperation.Exception);
            if (getEstOperation.Result == null) return RecordNotFound();

            var getProOperation = await _pbo.ReadAsync(getOperation.Result.ProfileId);
            if (!getProOperation.Success) return OperationErrorBackToIndex(getProOperation.Exception);
            if (getProOperation.Result == null) return RecordNotFound();

            var getEOperation = await _ebo.ReadAsync(getOperation.Result.EstablishmentId);
            if (!getEOperation.Success) return OperationErrorBackToIndex(getEOperation.Exception);
            if (getEOperation.Result == null) return RecordNotFound();

            var getCOperation = await _cbo.ReadAsync(getEOperation.Result.CompanyId);
            if (!getCOperation.Success) return OperationErrorBackToIndex(getCOperation.Exception);
            if (getCOperation.Result == null) return RecordNotFound();

            var vm = ReservedQueueViewModel.Parse(getOperation.Result);

            Draw("Details", "fa-search");
            ViewData["Company"] = CompanyViewModel.Parse(getCOperation.Result);
            ViewData["Establishment"] = EstablishmentViewModel.Parse(getEstOperation.Result);
            ViewData["Profile"] = ProfileViewModel.Parse(getProOperation.Result);
            return View(vm);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var listEstOperation = await _ebo.ListNotDeletedAsync();
            if (!listEstOperation.Success) return OperationErrorBackToIndex(listEstOperation.Exception);

            var listProOperation = await _pbo.ListNotDeletedAsync();
            if (!listProOperation.Success) return OperationErrorBackToIndex(listProOperation.Exception);

            var estList = new List<SelectListItem>();
            foreach (var item in listEstOperation.Result)
            {
                estList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Address });
            }

            var profiList = new List<SelectListItem>();
            foreach (var item in listProOperation.Result)
            {
                profiList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.VatNumber.ToString() });
            }
            ViewBag.Establishments = estList;
            ViewBag.Profiles = profiList;

            Draw("Create", "fa-plus");
            
            return View();
        }


        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservedQueueViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToReservedQueue();
                var createOperation = await _bo.CreateAsync(model);
                if (!createOperation.Success) return OperationErrorBackToIndex(createOperation.Exception);
                if (!createOperation.Result)
                {
                    TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, createOperation.Message);

                    var listEstOperation = await _ebo.ListNotDeletedAsync();
                    if (!listEstOperation.Success) return OperationErrorBackToIndex(listEstOperation.Exception);

                    var listProOperation = await _pbo.ListNotDeletedAsync();
                    if (!listProOperation.Success) return OperationErrorBackToIndex(listProOperation.Exception);

                    var estList = new List<SelectListItem>();
                    foreach (var item in listEstOperation.Result)
                    {
                        estList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Address });
                    }

                    var profiList = new List<SelectListItem>();
                    foreach (var item in listProOperation.Result)
                    {
                        profiList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.VatNumber.ToString() });
                    }
                    ViewBag.Establishments = estList;
                    ViewBag.Profiles = profiList;

                    Draw("Create", "fa-plus");
                    return View(vm);
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

            var vm = ReservedQueueViewModel.Parse(getOperation.Result);
            var listEstOperation = await _ebo.ListNotDeletedAsync();
            if (!listEstOperation.Success) return OperationErrorBackToIndex(listEstOperation.Exception);

            var listProOperation = await _pbo.ListNotDeletedAsync();
            if (!listProOperation.Success) return OperationErrorBackToIndex(listEstOperation.Exception);

            var estList = new List<SelectListItem>();
            foreach (var item in listEstOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Address };
                if (item.Id == vm.EstablishmentId) listItem.Selected = true;
                estList.Add(listItem);
            }

            var profiList = new List<SelectListItem>();
            foreach (var item in listProOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.VatNumber.ToString() };
                if (item.Id == vm.ProfileId) listItem.Selected = true;
                profiList.Add(listItem);
            }

            ViewBag.Establishments = estList;
            ViewBag.Profiles = profiList;
            Draw("Edit", "fa-edit");
            return View(vm);
        }


        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ReservedQueueViewModel vm)
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

                        vm = ReservedQueueViewModel.Parse(getOperation.Result);
                        Draw("Edit", "fa-edit");

                        return View(vm);
                    }
                    if (!updateOperation.Result)
                    {
                        TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, updateOperation.Message);
                        getOperation = await _bo.ReadAsync((Guid)id);
                        if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                        if (getOperation.Result == null) return RecordNotFound();

                        vm = ReservedQueueViewModel.Parse(getOperation.Result);
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