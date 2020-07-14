using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using Recodme.RD.FullStoQReborn.BusinessLayer.Queue;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
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
            var reg = new ReservedQueue(estList.Id, profList.Id);
            var resCreate = bo.CreateAsync(reg).Result;
            var resGet = bo.ReadAsync(reg.Id).Result;
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
            var bo = new ReservedQueueBusinessObject();
            var resList = bo.List();

            var item = resList.Result.FirstOrDefault();

            var comp2 = new Company("Mecadinho ponto 10", 123456798);

            var reg2 = new Region("Setubal");

            var est2 = new Establishment("rua do ópio", "09h00", "20h00", "sundays and holidays",
                reg2.Id, comp2.Id);

            var cbo = new CompanyBusinessObject();
            cbo.Create(comp2);

            var rbo = new RegionBusinessObject();
            rbo.Create(reg2);

            var ebo = new EstablishmentBusinessObject();
            ebo.Create(est2);

            item.EstablishmentId = est2.Id;

            var resUpdate = bo.Update(item);
            var resNotList = bo.ListNotDeleted().Result;

            Assert.IsTrue(resUpdate.Success && resNotList.First().EstablishmentId == est2.Id);
        }

        [TestMethod]
        public void TestUpdateReservedQueueAsync()
        {
            ContextSeeder.Seed();
            var bo = new ReservedQueueBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            var comp2 = new Company("pingo sulfúrico", 123456798);
            var reg2 = new Region("Covilhona");
            var est2 = new Establishment("rua do ópio", "09h00", "20h00", "sundays and holidays",
                reg2.Id, comp2.Id);

            var cbo = new CompanyBusinessObject();
            cbo.Create(comp2);

            var rbo = new RegionBusinessObject();
            rbo.Create(reg2);

            var ebo = new EstablishmentBusinessObject();
            ebo.Create(est2);

            item.EstablishmentId = est2.Id;

            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNotDeletedAsync().Result;

            Assert.IsTrue(resList.Success && resUpdate.Success &&
                resList.Result.First().EstablishmentId == est2.Id);
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

            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
        #endregion
    }
}

