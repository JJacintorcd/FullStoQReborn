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
    public class StoreQueueController : ControllerBase
    {
        private readonly StoreQueueBusinessObject _bo = new StoreQueueBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]StoreQueueViewModel vm)
        {
            var storeQueue = vm.ToStoreQueue();
            var res = _bo.Create(storeQueue);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<StoreQueueViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var sqvm = new StoreQueueViewModel();
                sqvm.Id = res.Result.Id;
                sqvm.EstablishmentId= res.Result.EstablishmentId;
                sqvm.Quantity = res.Result.Quantity;
                return sqvm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<StoreQueueViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<StoreQueueViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new StoreQueueViewModel
                {
                    Id = item.Id,
                    EstablishmentId = item.EstablishmentId,
                    Quantity = item.Quantity
                });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] StoreQueueViewModel storeQueue)
        {
            var currentRes = _bo.Read(storeQueue.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.EstablishmentId == storeQueue.EstablishmentId && current.Quantity == storeQueue.Quantity)
                return StatusCode((int)HttpStatusCode.NotModified);

            if (current.EstablishmentId != storeQueue.EstablishmentId) current.EstablishmentId = storeQueue.EstablishmentId;
            if (current.Quantity != storeQueue.Quantity) current.Quantity = storeQueue.Quantity;


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
