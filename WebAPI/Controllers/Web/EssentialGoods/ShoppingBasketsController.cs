using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.EssentialGoodsViewModel;
using WebAPI.Models.HtmlComponents;
using WebAPI.Models.PersonViewModel;
using WebAPI.Support;

namespace WebAPI.Controllers.Web.EssentialGoods
{
    [Route("[controller]")]
    public class ShoppingBasketsController : Controller
    {
        private readonly ShoppingBasketBusinessObject _bo = new ShoppingBasketBusinessObject();
        private readonly ProfileBusinessObject _pbo = new ProfileBusinessObject();


        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
            {
                new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
                new BreadCrumb(){Icon = "fa-shopping-basket", Action="Index", Controller="ShoppingBaskets", Text = "ShoppingBaskets"}
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

        private async Task<List<ProfileViewModel>> GetProfileViewModels(List<Guid> ids)
        {
            var filterOperation = await _pbo.FilterAsync(x => ids.Contains(x.Id));
            var cList = new List<ProfileViewModel>();
            foreach (var item in filterOperation.Result)
            {
                cList.Add(ProfileViewModel.Parse(item));
            }
            return cList;
        }

        public void Draw(string type, string icon)
        {
            ViewData["Title"] = $"{type} Product Model";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = type, Controller = "ProductModels", Icon = icon, Text = type });
            ViewData["BreadCrumbs"] = crumbs;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var lst = new List<ShoppingBasketViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(ShoppingBasketViewModel.Parse(item));
            }

            var pList = await GetProfileViewModels(listOperation.Result.Select(x => x.ProfileId).Distinct().ToList());
            ViewData["Profiles"] = pList;

            ViewData["Title"] = "ShoppingBaskets";
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

            var getROperation = await _pbo.ReadAsync(getOperation.Result.ProfileId);
            if (!getROperation.Success) return OperationErrorBackToIndex(getROperation.Exception);
            if (getROperation.Result == null) return RecordNotFound();

            var vm = ShoppingBasketViewModel.Parse(getOperation.Result);


            ViewData["Profile"] = ProfileViewModel.Parse(getROperation.Result);

            Draw("Details", "fa-search");

            return View(vm);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var listProfOperation = await _pbo.ListNotDeletedAsync();
            if (!listProfOperation.Success) return OperationErrorBackToIndex(listProfOperation.Exception);
            var pList = new List<SelectListItem>();
            foreach (var item in listProfOperation.Result)
            {
                pList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.VatNumber.ToString() });
            }
            ViewBag.Profiles = pList;
            
            Draw("Create", "fa-plus");

            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShoppingBasketViewModel vm)
        {
            var listROperation = await _pbo.ListNotDeletedAsync();
            if (!listROperation.Success) return OperationErrorBackToIndex(listROperation.Exception);
            var pList = new List<SelectListItem>();
            foreach (var item in listROperation.Result)
            {
                pList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.VatNumber.ToString() });
            }

            ViewBag.Profiles = pList;

            Draw("Create", "fa-plus");

            if (ModelState.IsValid)
            {
                var model = vm.ToShoppingBasket();
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

            var vm = ShoppingBasketViewModel.Parse(getOperation.Result);

            var listROperation = await _pbo.ListNotDeletedAsync();
            if (!listROperation.Success) return OperationErrorBackToIndex(listROperation.Exception);
            var pList = new List<SelectListItem>();
            foreach (var item in listROperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.VatNumber.ToString() };
                if (item.Id == vm.ProfileId) listItem.Selected = true;
                pList.Add(listItem);
            }
            ViewBag.Profiles = pList;

            Draw("Edit", "fa-edit");

            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ShoppingBasketViewModel vm)
        {

            var listROperation = await _pbo.ListNotDeletedAsync();
            if (!listROperation.Success) return OperationErrorBackToIndex(listROperation.Exception);
            var pList = new List<SelectListItem>();
            foreach (var item in listROperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.VatNumber.ToString() };
                if (item.Id == vm.ProfileId) listItem.Selected = true;
                pList.Add(listItem);
            }
            ViewBag.Regions = pList;

            Draw("Edit", "fa-edit");
            
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

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return RecordNotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return OperationErrorBackToIndex(deleteOperation.Exception);
            else return OperationSuccess("The record was successfuly deleted");
        }
    }
}