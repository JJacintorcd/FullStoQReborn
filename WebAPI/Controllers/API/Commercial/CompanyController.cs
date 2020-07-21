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
    public class CompanyController : ControllerBase
    {
        private readonly CompanyBusinessObject _bo = new CompanyBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]CompanyViewModel vm)
        {
            var company = vm.ToCompany();
            var res = _bo.Create(company);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<CompanyViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var cvm = new CompanyViewModel();
                cvm.Id = res.Result.Id;
                cvm.Name = res.Result.Name;
                return cvm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<CompanyViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<CompanyViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new CompanyViewModel { Id = item.Id, Name = item.Name });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] CompanyViewModel company)
        {
            var currentRes = _bo.Read(company.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.Name == company.Name) return StatusCode((int)HttpStatusCode.NotModified);

            if (current.Name != company.Name) current.Name = company.Name;


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
