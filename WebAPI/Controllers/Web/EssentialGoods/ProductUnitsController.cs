using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using WebAPI.Models;
using WebAPI.Models.CommercialViewModel;
using WebAPI.Models.EssentialGoodsViewModel;
using WebAPI.Models.HtmlComponents;
using WebAPI.Models.PersonViewModel;
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
        private readonly CompanyBusinessObject _cbo = new CompanyBusinessObject();
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
                  new BreadCrumb(){Icon = "fa-box-open", Action="Index", Controller="ProductUnits", Text = "Product Units"}
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

        private IActionResult OperationErrorBackToIndex(string exception)
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

        private async Task<List<ProductModelViewModel>> GetProductModelViewModels(List<Guid> ids)
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

        private async Task<List<ProfileViewModel>> GetProfileViewModels(List<Guid> ids)
        {
            var filterOperation = await _pbo.FilterAsync(x => ids.Contains(x.Id));
            var drList = new List<ProfileViewModel>();
            foreach (var item in filterOperation.Result)
            {
                drList.Add(ProfileViewModel.Parse(item));
            }
            return drList;
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

            var lst = new List<ProductUnitViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(ProductUnitViewModel.Parse(item));
            }

            var listEOperation = await _ebo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var elst = new List<EstablishmentViewModel>();
            foreach (var item in listEOperation.Result)
            {
                elst.Add(EstablishmentViewModel.Parse(item));
            }

            var listSBOperation = await _sbbo.ListNotDeletedAsync();
            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

            var sblst = new List<ShoppingBasketViewModel>();
            foreach (var item in listSBOperation.Result)
            {
                sblst.Add(ShoppingBasketViewModel.Parse(item));
            }

            var eList = await GetEstablishmentViewModels(listOperation.Result.Select(x => x.EstablishmentId).Distinct().ToList());
            var sbList = await GetShoppingBasketViewModels(listOperation.Result.Select(x => x.ShoppingBasketId).Distinct().ToList());
            var pmList = await GetProductModelViewModels(listOperation.Result.Select(x => x.ProductModelId).Distinct().ToList());
            var cList = await GetCompanyViewModels(listEOperation.Result.Select(x => x.CompanyId).Distinct().ToList());
            var pList = await GetProfileViewModels(listSBOperation.Result.Select(x => x.ProfileId).Distinct().ToList());
            ViewData["Profiles"] = pList;
            ViewData["Companies"] = cList;
            ViewData["Establishments"] = eList;
            ViewData["ShoppingBaskets"] = sbList;
            ViewData["ProductModels"] = pmList;

            ViewData["Title"] = "Product Units";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            return View(lst);
        }
        public void Draw(string type, string icon)
        {
            ViewData["Title"] = $"{type} Product Unit";
            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = type, Controller = "ProductUnits", Icon = icon, Text = type });
            ViewData["BreadCrumbs"] = crumbs;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);

            if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
            if (getOperation.Result == null) return RecordNotFound();

            var getEOperation = await _ebo.ReadAsync(getOperation.Result.EstablishmentId);
            if (!getEOperation.Success) return OperationErrorBackToIndex(getEOperation.Exception);
            if (getEOperation.Result == null) return RecordNotFound();

            var getSBOperation = await _sbbo.ReadAsync(getOperation.Result.ShoppingBasketId);
            if (!getSBOperation.Success) return OperationErrorBackToIndex(getSBOperation.Exception);
            if (getSBOperation.Result == null) return RecordNotFound();

            var getPMOperation = await _pmbo.ReadAsync(getOperation.Result.ProductModelId);
            if (!getPMOperation.Success) return OperationErrorBackToIndex(getPMOperation.Exception);
            if (getPMOperation.Result == null) return RecordNotFound();

            var getPOperation = await _pbo.ReadAsync(getSBOperation.Result.ProfileId);
            if (!getPOperation.Success) return OperationErrorBackToIndex(getPOperation.Exception);
            if (getPOperation.Result == null) return RecordNotFound();

            var getCOperation = await _cbo.ReadAsync(getEOperation.Result.CompanyId);
            if (!getCOperation.Success) return OperationErrorBackToIndex(getCOperation.Exception);
            if (getCOperation.Result == null) return RecordNotFound();

            var vm = ProductUnitViewModel.Parse(getOperation.Result);
            ViewData["Profile"] = ProfileViewModel.Parse(getPOperation.Result);
            ViewData["Company"] = CompanyViewModel.Parse(getCOperation.Result);
            ViewData["Establishment"] = EstablishmentViewModel.Parse(getEOperation.Result);
            ViewData["ShoppingBasket"] = ShoppingBasketViewModel.Parse(getSBOperation.Result);
            ViewData["ProductModel"] = ProductModelViewModel.Parse(getPMOperation.Result);

            Draw("Details","fa-search");

            return View(vm);
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreateAsync()
        {
            var listEOperation = await _ebo.ListNotDeletedAsync();
            if (!listEOperation.Success) return OperationErrorBackToIndex(listEOperation.Exception);

            var eList = new List<SelectListItem>();
            foreach (var item in listEOperation.Result)
            {
                var company = await _cbo.ReadAsync(item.CompanyId);
                eList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = company.Result.Name + ", " + item.Address });
            }

            var listSBOperation = await _sbbo.ListNotDeletedAsync();
            if (!listSBOperation.Success) return OperationErrorBackToIndex(listSBOperation.Exception);

            var sbList = new List<SelectListItem>();
            foreach (var item in listSBOperation.Result)
            {
                var profile = await _pbo.ReadAsync(item.ProfileId);
                sbList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = profile.Result.VatNumber.ToString() });
            }

            var listPMOperation = await _pmbo.ListNotDeletedAsync();
            if (!listPMOperation.Success) return OperationErrorBackToIndex(listPMOperation.Exception);

            var pmList = new List<SelectListItem>();
            foreach (var item in listPMOperation.Result)
            {
                pmList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }

            ViewBag.Establishments = eList;
            ViewBag.ShoppingBaskets = sbList;
            ViewBag.ProductModels = pmList;

            Draw("Create", "fa-plus");

            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductUnitViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToModel();
                var createOperation = await _bo.CreateAsync(model);
                if (!createOperation.Success)
                {
                    TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, createOperation.Message);

                    var listEOperation = await _ebo.ListNotDeletedAsync();
                    if (!listEOperation.Success) return OperationErrorBackToIndex(listEOperation.Exception);

                    var eList = new List<SelectListItem>();
                    foreach (var item in listEOperation.Result)
                    {
                        var company = await _cbo.ReadAsync(item.CompanyId);
                        eList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = company.Result.Name + ", " + item.Address });
                    }

                    var listSBOperation = await _sbbo.ListNotDeletedAsync();
                    if (!listSBOperation.Success) return OperationErrorBackToIndex(listSBOperation.Exception);

                    var sbList = new List<SelectListItem>();
                    foreach (var item in listSBOperation.Result)
                    {
                        var profile = await _pbo.ReadAsync(item.ProfileId);
                        sbList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = profile.Result.VatNumber.ToString() });
                    }

                    var listPMOperation = await _pmbo.ListNotDeletedAsync();
                    if (!listPMOperation.Success) return OperationErrorBackToIndex(listPMOperation.Exception);

                    var pmList = new List<SelectListItem>();
                    foreach (var item in listPMOperation.Result)
                    {
                        pmList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
                    }

                    ViewBag.Establishments = eList;
                    ViewBag.ShoppingBaskets = sbList;
                    ViewBag.ProductModels = pmList;

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

            var vm = ProductUnitViewModel.Parse(getOperation.Result);

            var listEOperation = await _ebo.ListNotDeletedAsync();
            if (!listEOperation.Success) return OperationErrorBackToIndex(listEOperation.Exception);

            var eList = new List<SelectListItem>();
            foreach (var item in listEOperation.Result)
            {
                var company = await _cbo.ReadAsync(item.CompanyId);
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = company.Result.Name + ", " + item.Address};
                if (item.Id == vm.EstablishmentId) listItem.Selected = true;
                eList.Add(listItem);
            }
            ViewBag.Establishments = eList;

            var listSBOperation = await _sbbo.ListNotDeletedAsync();
            if (!listSBOperation.Success) return OperationErrorBackToIndex(listSBOperation.Exception);

            var sbList = new List<SelectListItem>();
            foreach (var item in listSBOperation.Result)
            {
                var profile = await _pbo.ReadAsync(item.ProfileId);
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = profile.Result.VatNumber.ToString() };
                if (item.Id == vm.ProductModelId) listItem.Selected = true;
                sbList.Add(listItem);
            }
            ViewBag.ShoppingBaskets = sbList;

            var listPMOperation = await _pmbo.ListNotDeletedAsync();
            if (!listPMOperation.Success) return OperationErrorBackToIndex(listPMOperation.Exception);

            var pmList = new List<SelectListItem>();
            foreach (var item in listPMOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                if (item.Id == vm.ShoppingBasketId) listItem.Selected = true;
                pmList.Add(listItem);
            }
            ViewBag.ProductModels = pmList;

            Draw("Edit", "fa-edit");

            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductUnitViewModel vm)
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

                        vm = ProductUnitViewModel.Parse(getOperation.Result);

                        var listEOperation = await _ebo.ListNotDeletedAsync();
                        if (!listEOperation.Success) return OperationErrorBackToIndex(listEOperation.Exception);

                        var eList = new List<SelectListItem>();
                        foreach (var item in listEOperation.Result)
                        {
                            var company = await _cbo.ReadAsync(item.CompanyId);
                            var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = company.Result.Name + ", " + item.Address };
                            if (item.Id == vm.EstablishmentId) listItem.Selected = true;
                            eList.Add(listItem);
                        }
                        ViewBag.Establishments = eList;

                        var listSBOperation = await _sbbo.ListNotDeletedAsync();
                        if (!listSBOperation.Success) return OperationErrorBackToIndex(listSBOperation.Exception);

                        var sbList = new List<SelectListItem>();
                        foreach (var item in listSBOperation.Result)
                        {
                            var profile = await _pbo.ReadAsync(item.ProfileId);
                            var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = profile.Result.VatNumber.ToString() };
                            if (item.Id == vm.ProductModelId) listItem.Selected = true;
                            sbList.Add(listItem);
                        }
                        ViewBag.ShoppingBaskets = sbList;

                        Draw("Edit", "fa-edit");

                        return View(vm);
                    }
                    if (!updateOperation.Result)
                    {
                        TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, updateOperation.Message);

                        getOperation = await _bo.ReadAsync((Guid)id);
                        if (!getOperation.Success) return OperationErrorBackToIndex(getOperation.Exception);
                        if (getOperation.Result == null) return RecordNotFound();

                        vm = ProductUnitViewModel.Parse(getOperation.Result);

                        var listEOperation = await _ebo.ListNotDeletedAsync();
                        if (!listEOperation.Success) return OperationErrorBackToIndex(listEOperation.Exception);

                        var eList = new List<SelectListItem>();
                        foreach (var item in listEOperation.Result)
                        {
                            var company = await _cbo.ReadAsync(item.CompanyId);
                            var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = company.Result.Name + ", " + item.Address };
                            if (item.Id == vm.EstablishmentId) listItem.Selected = true;
                            eList.Add(listItem);
                        }
                        ViewBag.Establishments = eList;

                        var listSBOperation = await _sbbo.ListNotDeletedAsync();
                        if (!listSBOperation.Success) return OperationErrorBackToIndex(listSBOperation.Exception);

                        var sbList = new List<SelectListItem>();
                        foreach (var item in listSBOperation.Result)
                        {
                            var profile = await _pbo.ReadAsync(item.ProfileId);
                            var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = profile.Result.VatNumber.ToString() };
                            if (item.Id == vm.ProductModelId) listItem.Selected = true;
                            sbList.Add(listItem);
                        }
                        ViewBag.ShoppingBaskets = sbList;

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
            if (id == null) return NotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}