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
    public class ShoppingBasketController : ControllerBase
    {
        private readonly ShoppingBasketBusinessObject _bo = new ShoppingBasketBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]ShoppingBasketViewModel vm)
        {
            var shoppingBasket = vm.ToShoppingBasket();
            var res = _bo.Create(shoppingBasket);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<ShoppingBasketViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var sbvm = new ShoppingBasketViewModel();
                sbvm.Id = res.Result.Id;
                sbvm.ProfileId = res.Result.ProfileId;
                return sbvm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<ShoppingBasketViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<ShoppingBasketViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new ShoppingBasketViewModel { Id = item.Id, ProfileId = item.ProfileId });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] ShoppingBasketViewModel shoppingBasket)
        {
            var currentRes = _bo.Read(shoppingBasket.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.ProfileId == shoppingBasket.ProfileId) return StatusCode((int)HttpStatusCode.NotModified);

            if (current.ProfileId != shoppingBasket.ProfileId) current.ProfileId = shoppingBasket.ProfileId;


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