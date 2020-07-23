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

            var drList = await GetEstablishmentViewModels(listOperation.Result.Select(x => x.EstablishmentId).Distinct().ToList());
            ViewData["Establishments"] = drList;
            ViewData["Title"] = "StoreQueues";
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

            var vm = StoreQueueViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "StoreQueue";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "StoreQueues", Icon = "fa-search", Text = "Details" });
            ViewData["Establishment"] = EstablishmentViewModel.Parse(getDrOperation.Result);
            ViewData["BreadCrumbs"] = crumbs;
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
            ViewData["Title"] = "Create StoreQueue";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Create", Controller = "StoreQueues", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
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
            ViewData["Title"] = "Edit StoreQueue";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "StoreQueues", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
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