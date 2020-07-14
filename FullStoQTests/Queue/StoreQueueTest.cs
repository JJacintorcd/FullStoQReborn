using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.BusinessLayer.Queue;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataLayer.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FullStoQTests.Queue
{
    [TestClass]
    public class StoreQueueTest
    {
        [TestMethod]
        public void TestCreateAndReadStoreQueue()
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
            var bo = new StoreQueueBusinessObject();
            var stoQ = new StoreQueue(32, est.Id);
            var stoQCreate = bo.Create(stoQ);
            var stoQGet = bo.Read(stoQ.Id);
            Assert.IsTrue(stoQCreate.Success && stoQGet.Success && stoQGet.Result != null);
        }

        [TestMethod]
        public void TestCreateAndReadStoreQueueAsync()
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
            var bo = new StoreQueueBusinessObject();
            var storeQueue = new StoreQueue(32, est.Id);
            var stoQCreate = bo.CreateAsync(storeQueue).Result;
            var stoQGet = bo.ReadAsync(storeQueue.Id).Result;
            Assert.IsTrue(stoQCreate.Success && stoQGet.Success && stoQGet.Result != null);
        }

        [TestMethod]
        public void TestListStoreQueue()
        {
            ContextSeeder.Seed();
            var bo = new StoreQueueBusinessObject();
            var storeQueueList = bo.List();
            Assert.IsTrue(storeQueueList.Success && storeQueueList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListStoreQueueAsync()
        {
            ContextSeeder.Seed();
            var bo = new StoreQueueBusinessObject();
            var stoQList = bo.ListAsync().Result;
            Assert.IsTrue(stoQList.Success && stoQList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateStoreQueue()
        {
            ContextSeeder.Seed();
            var bo = new StoreQueueBusinessObject();
            var stoQList = bo.List();
            var item = stoQList.Result.FirstOrDefault();
            item.Quantity = 10;
            var stoQUpdate = bo.Update(item);
            var stoQNotList = bo.ListNotDeleted().Result;
            Assert.IsTrue(stoQUpdate.Success && stoQNotList.First().Quantity == 10);
        }

        [TestMethod]
        public void TestUpdateStoreQueueAsync()
        {
            ContextSeeder.Seed();
            var bo = new StoreQueueBusinessObject();
            var stoQList = bo.ListAsync().Result;
            var item = stoQList.Result.FirstOrDefault();
            item.Quantity = 11;
            var stoQUpdate = bo.UpdateAsync(item).Result;
            stoQList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(stoQList.Success && stoQUpdate.Success && stoQList.Result.First().Quantity == 11);
        }

        [TestMethod]
        public void TestDeleteStoreQueue()
        {
            ContextSeeder.Seed();
            var bo = new StoreQueueBusinessObject();
            var stoQList = bo.List();
            var stoQDelete = bo.Delete(stoQList.Result.First().Id);
            var stoQNotList = bo.List().Result.Where(x => !x.IsDeleted).ToList();
            Assert.IsTrue(stoQDelete.Success && stoQNotList.Count == 0);
        }

        [TestMethod]
        public void TestDeleteStoreQueueAsync()
        {
            ContextSeeder.Seed();
            var bo = new StoreQueueBusinessObject();
            var stoQList = bo.ListAsync().Result;
            var stoQDelete = bo.DeleteAsync(stoQList.Result.First().Id).Result;
            stoQList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(stoQDelete.Success && stoQList.Success && stoQList.Result.Count == 0);
        }
    }
}
