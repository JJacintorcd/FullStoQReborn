using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using Recodme.RD.FullStoQReborn.BusinessLayer.Queue;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataLayer.Queue;
using System;
using System.Linq;

namespace FullStoQTests.Queue
{
    [TestClass]
    public class ReservedQueueTest
    {
        #region Create And Read
        [TestMethod]
        public void TestCreateAndReadReservedQueues()
        {
            ContextSeeder.Seed();

            var boProf = new ProfileBusinessObject();
            var profList = boProf.List().Result.First();

            var boEst = new EstablishmentBusinessObject();
            var estList = boEst.List().Result.First();

            var bo = new ReservedQueueBusinessObject();
            var res = new ReservedQueue(estList.Id, profList.Id);
            var resCreate = bo.Create(res);
            var resGet = bo.Read(res.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateAndReadReservedQueueAsync()
        {
            ContextSeeder.Seed();

            var boProf = new ProfileBusinessObject();
            var profList = boProf.List().Result.First();

            var boEst = new EstablishmentBusinessObject();
            var estList = boEst.List().Result.First();

            var bo = new ReservedQueueBusinessObject();
            var res = new ReservedQueue(estList.Id, profList.Id);
            var resCreate = bo.CreateAsync(res).Result;
            var resGet = bo.ReadAsync(res.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }
        #endregion

        #region List
        [TestMethod]
        public void TestListReservedQueues()
        {
            ContextSeeder.Seed();
            var bo = new ReservedQueueBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListReservedQueueAsync()
        {
            ContextSeeder.Seed();
            var bo = new ReservedQueueBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }
        #endregion

        #region Update
        [TestMethod]
        public void TestUpdateReservedQueue()
        {
            ContextSeeder.Seed();
            var regBo = new RegionBusinessObject();
            var reg = new Region("Algordos");
            regBo.Create(reg);
            var compBo = new CompanyBusinessObject();
            var comp = new Company("", 1234567890);
            compBo.Create(comp);
            var estBo = new EstablishmentBusinessObject();
            var est = new Establishment("anywhere", "sempre", "nunca", "8º dia da semana", reg.Id, comp.Id);
            estBo.Create(est); 
            var bo = new ReservedQueueBusinessObject();
            var stoQList = bo.List();
            var item = stoQList.Result.FirstOrDefault();
            item.EstablishmentId = est.Id;
            var stoQUpdate = bo.Update(item);
            var stoQNotList = bo.ListNotDeleted().Result;
            Assert.IsTrue(stoQUpdate.Success && stoQNotList.First().EstablishmentId == est.Id);
        }

        [TestMethod]
        public void TestUpdateReservedQueueAsync()
        {
            ContextSeeder.Seed();
            var regBo = new RegionBusinessObject();
            var reg = new Region("Algordos");
            regBo.Create(reg);
            var compBo = new CompanyBusinessObject();
            var comp = new Company("", 1234567890);
            compBo.Create(comp);
            var estBo = new EstablishmentBusinessObject();
            var est = new Establishment("anywhere", "sempre", "nunca", "8º dia da semana", reg.Id, comp.Id);
            estBo.Create(est); 
            var bo = new ReservedQueueBusinessObject();
            var stoQList = bo.ListAsync().Result;
            var item = stoQList.Result.FirstOrDefault();
            item.EstablishmentId = est.Id;
            var stoQUpdate = bo.UpdateAsync(item).Result;
            stoQList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(stoQList.Success && stoQUpdate.Success && stoQList.Result.First().EstablishmentId == est.Id);
        }
        #endregion

        #region Delete
        [TestMethod]
        public void TestDeleteReservedQueues()
        {
            ContextSeeder.Seed();
            var bo = new ReservedQueueBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            var resNotList = bo.List().Result.Where(x => !x.IsDeleted).ToList();

            Assert.IsTrue(resDelete.Success && resNotList.Count == 0);
        }

        [TestMethod]
        public void TestDeleteReservedQueueAsync()
        {
            ContextSeeder.Seed();
            var bo = new ReservedQueueBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNotDeletedAsync().Result;

            Assert.IsTrue(resDelete.Success && resList.Success &&
                resList.Result.Count == 0);
        }
        #endregion

        [TestMethod]
        public void TestHourLimit()
        {
            ContextSeeder.Seed();

            var boProf = new ProfileBusinessObject();
            var profList = boProf.List().Result.First();
            var boEst = new EstablishmentBusinessObject();
            var estList = boEst.List().Result.First();

            var bo = new ReservedQueueBusinessObject();
            var res = new ReservedQueue(Guid.NewGuid(), DateTime.UtcNow.AddHours(-3), DateTime.UtcNow, false, estList.Id, profList.Id);
           bo.Create(res);
            var limit = bo.TwoHourLimitReserve(res.Id);

            Assert.IsTrue(res.IsDeleted && limit.Success && !limit.Result);
        }

        [TestMethod]
        public void TestHourLimitAsync()
        {
            ContextSeeder.Seed();

            var boProf = new ProfileBusinessObject();
            var profList = boProf.List().Result.First();
            var boEst = new EstablishmentBusinessObject();
            var estList = boEst.List().Result.First();

            var bo = new ReservedQueueBusinessObject();
            var res = new ReservedQueue(Guid.NewGuid(), DateTime.UtcNow.AddHours(-3), DateTime.UtcNow, false, estList.Id, profList.Id);

            var resCreate = bo.CreateAsync(res);
            var limit = bo.TwoHourLimitReserveAsync(res.Id);

            Assert.IsTrue(res.IsDeleted && limit.Result.Success && !limit.Result.Result);
        }
    }
}

