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
                  new BreadCrumb(){Icon = "fa-shish-kebab", Action="Index", Controller="ReservedQueuees", Text = "ReservedQueuees"}
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

            var drList = await GetEstablishmentViewModels(listOperation.Result.Select(x => x.EstablishmentId).Distinct().ToList());
            ViewData["Establishments"] = drList;
            ViewData["Title"] = "ReservedQueuees";
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

            var vm = ReservedQueueViewModel.Parse(getOperation.Result);
            ViewData["Title"] = "ReservedQueue";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "ReservedQueuees", Icon = "fa-search", Text = "Detail" });
            ViewData["Establishment"] = EstablishmentViewModel.Parse(getDrOperation.Result);
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            var listDrOperation = await _ebo.ListNotDeletedAsync();
            if (!listDrOperation.Success) return OperationErrorBackToIndex(listDrOperation.Exception);

            var drList = new List<SelectListItem>();
            foreach (var item in listDrOperation.Result)
            {
                drList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Address });
            }
            ViewBag.Establishments = drList;
            ViewData["Title"] = "New ReservedQueue";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "ReservedQueuees", Icon = "fa-plus", Text = "New" });
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }


        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ReservedQueueViewModel vm)
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
            ViewData["Title"] = "Edit Course";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "ReservedQueuees", Icon = "fa-edit", Text = "Edit" });
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