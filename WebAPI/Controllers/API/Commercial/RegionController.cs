using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using System;
using System.Collections.Generic;
using System.Net;
using WebAPI.Models.CommercialViewModel;

namespace WebAPI.Controllers.Api.Commercial
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly RegionBusinessObject _bo = new RegionBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]RegionViewModel vm)
        {
            var region = vm.ToModel();
            var res = _bo.Create(region);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<RegionViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var pvm = new RegionViewModel();
                pvm.Id = res.Result.Id;
                pvm.Name = res.Result.Name;
                return pvm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<RegionViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<RegionViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new RegionViewModel { Id = item.Id, Name = item.Name });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] RegionViewModel region)
        {
            var currentRes = _bo.Read(region.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.Name == region.Name) return StatusCode((int)HttpStatusCode.NotModified);

            if (current.Name != region.Name) current.Name = region.Name;


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
