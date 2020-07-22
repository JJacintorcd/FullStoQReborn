//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
//using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
//using WebAPI.Models.EssentialGoodsViewModel;
//using WebAPI.Models.HtmlComponents;
//using WebAPI.Support;

//namespace WebAPI.Controllers.Api.EssentialGoods
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductUnitController : ControllerBase
//    {
//        private readonly ProductUnitBusinessObject _bo = new ProductUnitBusinessObject();
//        private readonly ProductModelBusinessObject _pmbo = new ProductModelBusinessObject();
//        private readonly EstablishmentBusinessObject _ebo = new EstablishmentBusinessObject();
//        private readonly ShoppingBasketBusinessObject _spbo = new ShoppingBasketBusinessObject();

//        private string GetDeleteRef()
//        {
//            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
//        }

//        private List<BreadCrumb> GetCrumbs()
//        {
//            return new List<BreadCrumb>()
//                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
//                  new BreadCrumb(){Icon = "fa-user-cog", Action="Administration", Controller="Home", Text = "Administration"},
//                  new BreadCrumb(){Icon = "fa-shish-kebab", Action="Index", Controller="Dishes", Text = "Dishes"}
//                };
//        }

//        private IActionResult RecordNotFound()
//        {
//            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Information, "The record was not found");
//            return RedirectToAction(nameof(Index));
//        }

//        private IActionResult OperationErrorBackToIndex(Exception exception)
//        {
//            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, exception);
//            return RedirectToAction(nameof(Index));
//        }

//        private IActionResult OperationSuccess(string message)
//        {
//            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Success, message);
//            return RedirectToAction(nameof(Index));
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            var listOperation = await _bo.ListNotDeletedAsync();
//            if (!listOperation.Success) return OperationErrorBackToIndex(listOperation.Exception);

//            var lst = new List<ProdctUnitViewModel>();
//            foreach (var item in listOperation.Result)
//            {
//                lst.Add(ProdctUnitViewModel.Parse(item));
//            }

//            var drList = await GetDietaryRestrictionViewModels(listOperation.Result.Select(x => x.DietaryRestrictionId).Distinct().ToList());
//            ViewData["DietaryRestrictions"] = drList;
//            ViewData["Title"] = "Dishes";
//            ViewData["BreadCrumbs"] = GetCrumbs();
//            ViewData["DeleteHref"] = GetDeleteRef();

//            return View(lst);
//        }

//        [HttpPost]
//        public ActionResult Create([FromBody]ProductUnitViewModel vm)
//        {
//            var productUnit = vm.ToProductUnit();
//            var res = _bo.Create(productUnit);
//            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
//        }

//        [HttpGet("{id}")]
//        public ActionResult<ProductUnitViewModel> Get(Guid id)
//        {
//            var res = _bo.Read(id);
//            if (res.Success)
//            {
//                if (res.Result == null) return NotFound();
//                var puvm = new ProductUnitViewModel();
//                puvm.Id = res.Result.Id;
//                puvm.ProductModelId = res.Result.ProductModelId;
//                puvm.SerialNumber = res.Result.SerialNumber;

//                return puvm;
//            }

//            else return StatusCode((int)HttpStatusCode.InternalServerError);
//        }

//        [HttpGet]
//        public ActionResult<List<ProductUnitViewModel>> List()
//        {
//            var res = _bo.List();
//            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
//            var list = new List<ProductUnitViewModel>();
//            foreach (var item in res.Result)
//            {
//                list.Add(new ProductUnitViewModel { Id = item.Id, ProductModelId = item.ProductModelId, SerialNumber = item.SerialNumber });
//            }
//            return list;
//        }

//        [HttpPut]
//        public ActionResult Update([FromBody] ProductUnitViewModel productUnit)
//        {
//            var currentRes = _bo.Read(productUnit.Id);
//            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
//            var current = currentRes.Result;
//            if (current == null) return NotFound();

//            if (current.ProductModelId == productUnit.ProductModelId && current.SerialNumber == productUnit.SerialNumber) return StatusCode((int)HttpStatusCode.NotModified);

//            if (current.ProductModelId != productUnit.ProductModelId) current.ProductModelId = productUnit.ProductModelId;
//            if (current.SerialNumber != productUnit.SerialNumber) current.SerialNumber = productUnit.SerialNumber;


//            var updateResult = _bo.Update(current);
//            if (!updateResult.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
//            return Ok();
//        }

//        [HttpDelete("{id}")]
//        public ActionResult Delete(Guid id)
//        {
//            var result = _bo.Delete(id);
//            if (result.Success) return Ok();
//            return StatusCode((int)HttpStatusCode.InternalServerError);
//        }
//    }
//}