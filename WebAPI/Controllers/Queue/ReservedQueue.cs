using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.Queue;
using System;
using System.Collections.Generic;
using System.Net;
using WebAPI.Models.QueueViewModel;

namespace WebAPI.Controllers.Queue
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservedQueueController : ControllerBase
    {
        private readonly ReservedQueueBusinessObject _bo = new ReservedQueueBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]ReservedQueueViewModel vm)
        {
            var reservedQueue = vm.ToReservedQueue();
            var res = _bo.Create(reservedQueue);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<ReservedQueueViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var rqvm = new ReservedQueueViewModel();
                rqvm.Id = res.Result.Id;
                rqvm.EstablishmentId = res.Result.EstablishmentId;
                rqvm.ProfileId = res.Result.ProfileId;               
                return rqvm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<ReservedQueueViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<ReservedQueueViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new ReservedQueueViewModel
                {
                    Id = item.Id,
                    EstablishmentId = item.EstablishmentId,
                    ProfileId = item.ProfileId
                });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] ReservedQueueViewModel reservedQueue)
        {
            var currentRes = _bo.Read(reservedQueue.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.EstablishmentId == reservedQueue.EstablishmentId && current.ProfileId == reservedQueue.ProfileId)
                return StatusCode((int)HttpStatusCode.NotModified);

            if (current.EstablishmentId != reservedQueue.EstablishmentId) current.EstablishmentId = reservedQueue.EstablishmentId;
            if (current.ProfileId != reservedQueue.ProfileId) current.ProfileId = reservedQueue.ProfileId;
          

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
