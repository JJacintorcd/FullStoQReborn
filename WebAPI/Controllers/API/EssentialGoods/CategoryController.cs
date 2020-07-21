using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using WebAPI.Models.EssentialGoodsViewModel;

namespace WebAPI.Controllers.Api.EssentialGoods
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryBusinessObject _bo = new CategoryBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]CategoryViewModel vm)
        {
            var category = vm.ToCategory();
            var res = _bo.Create(category);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var rvm = new CategoryViewModel();
                rvm.Id = res.Result.Id;
                rvm.Name = res.Result.Name;
                return rvm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<CategoryViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<CategoryViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new CategoryViewModel { Id = item.Id, Name = item.Name });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] CategoryViewModel category)
        {
            var currentRes = _bo.Read(category.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.Name == category.Name) return StatusCode((int)HttpStatusCode.NotModified);

            if (current.Name != category.Name) current.Name = category.Name;


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