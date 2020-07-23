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


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var lst = new List<ReservedQueueViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(ReservedQueueViewModel.Parse(item));
            }

            var estList = await GetEstablishmentViewModels(listOperation.Result.Select(x => x.EstablishmentId).Distinct().ToList());
            var profiList = await GetProfileViewModels(listOperation.Result.Select(x => x.ProfileId).Distinct().ToList());
            ViewData["Establishments"] = estList;
            ViewData["Profiles"] = profiList;
            ViewData["Title"] = "ReservedQueues";
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

            var vm = ReservedQueueViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "ReservedQueue";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "ReservedQueues", Icon = "fa-search", Text = "Detail" });
            ViewData["Establishment"] = EstablishmentViewModel.Parse(getEstOperation.Result);
            ViewData["Profile"] = ProfileViewModel.Parse(getProOperation.Result);
            ViewData["BreadCrumbs"] = crumbs;
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
            ViewData["Title"] = "New ReservedQueue";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Create", Controller = "ReservedQueues", Icon = "fa-plus", Text = "Create" });
            ViewData["BreadCrumbs"] = crumbs;
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
            foreach (var item in listEstOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Address };
                if (item.Id == vm.ProfileId) listItem.Selected = true;
                profiList.Add(listItem);
            }

            ViewBag.Establishments = estList;
            ViewBag.Profiles = profiList;
            ViewData["Title"] = "Edit Reserved Queues";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "ReservedQueues", Icon = "fa-edit", Text = "Edit" });
            ViewData["BreadCrumbs"] = crumbs;
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