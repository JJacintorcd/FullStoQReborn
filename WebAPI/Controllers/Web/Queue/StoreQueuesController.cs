using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.BusinessLayer.Queue;
using WebAPI.Models;
using WebAPI.Models.CommercialViewModel;
using WebAPI.Models.HtmlComponents;
using WebAPI.Models.QueueViewModel;
using WebAPI.Support;

namespace WebAPI.Controllers.Web.Queue
{
    [Route("[controller]")]
    public class StoreQueuesController : Controller
    {
        private readonly StoreQueueBusinessObject _bo = new StoreQueueBusinessObject();
        private readonly EstablishmentBusinessObject _ebo = new EstablishmentBusinessObject();
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
                  new BreadCrumb(){Icon = "fa-users-class", Action="Index", Controller="StoreQueues", Text = "Store Queues"}
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
            ViewData["Title"] = $"{type} - Store Queue";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = type, Controller = "StoreQueues", Icon = icon, Text = type });
            ViewData["BreadCrumbs"] = crumbs;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var lst = new List<StoreQueueViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(StoreQueueViewModel.Parse(item));
            }

            var listEOperation = await _ebo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var elst = new List<EstablishmentViewModel>();
            foreach (var item in listEOperation.Result)
            {
                elst.Add(EstablishmentViewModel.Parse(item));
            }

            var drList = await GetEstablishmentViewModels(listOperation.Result.Select(x => x.EstablishmentId).Distinct().ToList());
            var cList = await GetCompanyViewModels(listEOperation.Result.Select(x => x.CompanyId).Distinct().ToList());
            ViewData["Companies"] = cList;
            ViewData["Establishments"] = drList;
            ViewData["Title"] = "Store Queues";
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

            var getDrOperation = await _ebo.ReadAsync(getOperation.Result.EstablishmentId);
            if (!getDrOperation.Success) return OperationErrorBackToIndex(getDrOperation.Exception);
            if (getDrOperation.Result == null) return RecordNotFound();

            var getCOperation = await _cbo.ReadAsync(getDrOperation.Result.CompanyId);
            if (!getCOperation.Success) return OperationErrorBackToIndex(getCOperation.Exception);
            if (getCOperation.Result == null) return RecordNotFound();

            var vm = StoreQueueViewModel.Parse(getOperation.Result);
            ViewData["Company"] = CompanyViewModel.Parse(getCOperation.Result);
            ViewData["Establishment"] = EstablishmentViewModel.Parse(getDrOperation.Result);
            Draw("Details", "fa-search");
            return View(vm);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var listDrOperation = await _ebo.ListNotDeletedAsync();
            if (!listDrOperation.Success) return OperationErrorBackToIndex(listDrOperation.Exception);

            var drList = new List<SelectListItem>();
            foreach (var item in listDrOperation.Result)
            {
                drList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Address });
            }
            ViewBag.Establishments = drList;
            Draw("Create", "fa-plus");
            return View();
        }


        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreQueueViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToStoreQueue();
                var createOperation = await _bo.CreateAsync(model);
                if (!createOperation.Success) return OperationErrorBackToIndex(createOperation.Exception);

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

            var vm = StoreQueueViewModel.Parse(getOperation.Result);
            var listDrOperation = await _ebo.ListNotDeletedAsync();
            if (!listDrOperation.Success) return OperationErrorBackToIndex(listDrOperation.Exception);

            var drList = new List<SelectListItem>();
            foreach (var item in listDrOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Address };
                if (item.Id == vm.EstablishmentId) listItem.Selected = true;
                drList.Add(listItem);
            }
            ViewBag.Establishments = drList;
            Draw("Edit", "fa-edit");
            return View(vm);
        }


        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StoreQueueViewModel vm)
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