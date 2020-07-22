using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Net;
using WebAPI.Models.EssentialGoodsViewModel;

namespace WebAPI.Controllers.Api.EssentialGoods
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandBusinessObject _bo = new BrandBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]BrandViewModel vm)
        {
            var brand = vm.ToModel();
            var res = _bo.Create(brand);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<BrandViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var bvm = new BrandViewModel();
                bvm.Id = res.Result.Id;
                bvm.Name = res.Result.Name;
                return bvm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<BrandViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<BrandViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new BrandViewModel { Id = item.Id, Name = item.Name });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] BrandViewModel brand)
        {
            var currentRes = _bo.Read(brand.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.Name == brand.Name) return StatusCode((int)HttpStatusCode.NotModified);

            if (current.Name != brand.Name) current.Name = brand.Name;


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