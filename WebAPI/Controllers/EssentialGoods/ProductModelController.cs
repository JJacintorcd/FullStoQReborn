using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using WebAPI.Models.EssentialGoodsViewModel;

namespace WebAPI.Controllers.EssentialGoods
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductModelController : ControllerBase
    {
        private readonly ProductModelBusinessObject _bo = new ProductModelBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]ProductModelViewModel vm)
        {
            var productModel = vm.ToProductModel();
            var res = _bo.Create(productModel);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductModelViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var pvm = new ProductModelViewModel();
                pvm.Id = res.Result.Id;
                pvm.Name = res.Result.Name;
                pvm.IsReserved = res.Result.IsReserved;
                pvm.BarCode = res.Result.BarCode;
                pvm.Price = res.Result.Price;
                pvm.Weight = res.Result.Weight;
                pvm.BrandId = res.Result.BrandId;
                pvm.CategoryId = res.Result.CategoryId;
                return pvm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<ProductModelViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<ProductModelViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new ProductModelViewModel { Id = item.Id, Name = item.Name });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] ProductModelViewModel productModel)
        {
            var currentRes = _bo.Read(productModel.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.Name == productModel.Name && current.IsReserved == productModel.IsReserved && current.BarCode == productModel.BarCode && current.Price == productModel.Price && current.Weight == productModel.Weight && current.BrandId == productModel.BrandId && current.CategoryId == productModel.CategoryId) return StatusCode((int)HttpStatusCode.NotModified);

            if (current.Name != productModel.Name) current.Name = productModel.Name;
            if (current.IsReserved != productModel.IsReserved) current.IsReserved = productModel.IsReserved;
            if (current.BarCode != productModel.BarCode) current.BarCode = productModel.BarCode;
            if (current.Price != productModel.Price) current.Price = productModel.Price;
            if (current.Weight != productModel.Weight) current.Weight = productModel.Weight;
            if (current.BrandId != productModel.BrandId) current.BrandId = productModel.BrandId;
            if (current.CategoryId != productModel.CategoryId) current.CategoryId = productModel.CategoryId;


            var updateResult = _bo.Update(current);
            if (!updateResult.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var result = _bo.Delete(id);
            if (result.Success) return Ok();
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}