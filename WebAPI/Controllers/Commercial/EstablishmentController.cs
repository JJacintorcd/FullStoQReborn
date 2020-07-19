using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using System;
using System.Collections.Generic;
using System.Net;
using WebAPI.Models.CommercialViewModel;

namespace WebAPI.Controllers.Commercial
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        private readonly EstablishmentBusinessObject _bo = new EstablishmentBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]EstablishmentViewModel vm)
        {
            var establishment = vm.ToEstablishment();
            var res = _bo.Create(establishment);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<EstablishmentViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var evm = new EstablishmentViewModel();
                evm.Id = res.Result.Id;
                evm.Address = res.Result.Address;
                evm.ClosingDays = res.Result.ClosingDays;
                evm.ClosingHours = res.Result.ClosingHours;
                evm.OpeningHours = res.Result.OpeningHours;
                evm.RegionId = res.Result.RegionId;
                evm.CompanyId = res.Result.CompanyId;
                return evm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<EstablishmentViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<EstablishmentViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new EstablishmentViewModel
                {
                    Id = item.Id,
                    Address = item.Address,
                    ClosingDays = item.ClosingDays,
                    ClosingHours = item.ClosingHours,
                    OpeningHours = item.OpeningHours,
                    RegionId = item.RegionId,
                    CompanyId = item.CompanyId
                });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] EstablishmentViewModel establishment)
        {
            var currentRes = _bo.Read(establishment.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.Address == establishment.Address && current.ClosingDays == establishment.ClosingDays &&
                current.ClosingHours == establishment.ClosingHours && current.OpeningHours == establishment.OpeningHours &&
                current.RegionId == establishment.RegionId && current.CompanyId == establishment.CompanyId)
                return StatusCode((int)HttpStatusCode.NotModified);

            if (current.Address != establishment.Address) current.Address = establishment.Address;
            if (current.ClosingDays != establishment.ClosingDays) current.ClosingDays = establishment.ClosingDays;
            if (current.ClosingHours != establishment.ClosingHours) current.ClosingHours = establishment.ClosingHours;
            if (current.OpeningHours != establishment.OpeningHours) current.OpeningHours = establishment.OpeningHours;
            if (current.RegionId != establishment.RegionId) current.RegionId = establishment.RegionId;
            if (current.CompanyId != establishment.CompanyId) current.CompanyId = establishment.CompanyId;

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
