using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using Recodme.RD.FullStoQReborn.BusinessLayer.Queue;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using Recodme.RD.FullStoQReborn.DataLayer.Queue;
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
        public void TestUpdateReservedQueues()
        {
            ContextSeeder.Seed();
            var boProf = new ProfileBusinessObject();
            var profList = boProf.List().Result.First();

            var boEst = new EstablishmentBusinessObject();
            var estList = boEst.List().Result.First();

            var bo = new ReservedQueueBusinessObject();
            var resList = bo.List();

            var item = resList.Result.FirstOrDefault();
            item.ProfileId = item.Id;

            var resUpdate = bo.Update(item);
            var resNotList = bo.List().Result.Where(x => !x.IsDeleted);

            Assert.IsTrue(resUpdate.Success && resNotList.First().ProfileId == item.Id);
        }

        [TestMethod]
        public void TestUpdateReservedQueueAsync()
        {
            ContextSeeder.Seed();                     

            var bo = new ReservedQueueBusinessObject();
            var resList = bo.List(); 

            var itemPro = resList.Result.FirstOrDefault();
            itemPro.ProfileId = itemPro.Id;

            var itemEst = resList.Result.FirstOrDefault();
            itemEst.EstablishmentId = itemEst.Id;           
            
            var reg = new ReservedQueue(itemEst.EstablishmentId, itemPro.ProfileId);
            var resCreate = bo.CreateAsync(reg).Result;
            var resGet = bo.ReadAsync(reg.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
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
    }
}


