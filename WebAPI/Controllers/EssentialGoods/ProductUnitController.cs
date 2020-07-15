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
    public class ProductUnitController : ControllerBase
    {
        private readonly ProductUnitBusinessObject _bo = new ProductUnitBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]ProductUnitViewModel vm)
        {
            var productUnit = vm.ToProductUnit();
            var res = _bo.Create(productUnit);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductUnitViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var pvm = new ProductUnitViewModel();
                pvm.Id = res.Result.Id;
                pvm.ProductModelId = res.Result.ProductModelId;
                pvm.SerialNumber = res.Result.SerialNumber;

                return pvm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<ProductUnitViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<ProductUnitViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new ProductUnitViewModel { Id = item.Id, ProductModelId = item.ProductModelId, SerialNumber = item.SerialNumber });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] ProductUnitViewModel productUnit)
        {
            var currentRes = _bo.Read(productUnit.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.ProductModelId == productUnit.ProductModelId && current.SerialNumber == productUnit.SerialNumber) return StatusCode((int)HttpStatusCode.NotModified);

            if (current.ProductModelId != productUnit.ProductModelId) current.ProductModelId = productUnit.ProductModelId;
            if (current.SerialNumber != productUnit.SerialNumber) current.SerialNumber = productUnit.SerialNumber;


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