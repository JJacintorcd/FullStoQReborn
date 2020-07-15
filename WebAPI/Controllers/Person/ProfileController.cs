using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using WebAPI.Models.PersonViewModel;

namespace WebAPI.Controllers.Person
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileBusinessObject _bo = new ProfileBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]ProfileViewModel vm)
        {
            var profile = vm.ToProfile();
            var res = _bo.Create(profile);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<ProfileViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var pvm = new ProfileViewModel();
                pvm.Id = res.Result.Id;
                pvm.VatNumber = res.Result.VatNumber;
                pvm.PhoneNumber = res.Result.PhoneNumber;
                pvm.FirstName = res.Result.FirstName;
                pvm.LastName = res.Result.LastName;
                pvm.BirthDate = res.Result.BirthDate;
                return pvm;
            }

            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<ProfileViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<ProfileViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(new ProfileViewModel { Id = item.Id, FirstName = item.FirstName, LastName = item.LastName, VatNumber = item.VatNumber, BirthDate = item.BirthDate, PhoneNumber = item.PhoneNumber });
            }
            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] ProfileViewModel profile)
        {
            var currentRes = _bo.Read(profile.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.FirstName == profile.FirstName && current.LastName == profile.LastName &&
                current.BirthDate == profile.BirthDate && current.VatNumber == profile.VatNumber &&
                current.PhoneNumber == profile.PhoneNumber) return StatusCode((int)HttpStatusCode.NotModified);

            if (current.FirstName != profile.FirstName) current.FirstName = profile.FirstName;
            if (current.LastName != profile.LastName) current.LastName = profile.LastName;
            if (current.BirthDate != profile.BirthDate) current.BirthDate = profile.BirthDate;
            if (current.VatNumber != profile.VatNumber) current.VatNumber = profile.VatNumber;
            if (current.PhoneNumber != profile.PhoneNumber) current.PhoneNumber = profile.PhoneNumber;

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