using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System.Linq;

namespace FullStoQTests.Commercial
{
    [TestClass]
    public class RegionTest
    {
        [TestMethod]
        public void TestCreateAndReadRegion()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var reg = new Region("Lisboa");
            var resCreate = bo.Create(reg);
            var resGet = bo.Read(reg.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateAndReadRegionAsync()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var reg = new Region("Lisboa");
            var resCreate = bo.CreateAsync(reg).Result;
            var resGet = bo.ReadAsync(reg.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListRegion()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListRegionAsync()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateRegion()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            item.Name = "another";
            var resUpdate = bo.Update(item);
            var resNotList = bo.ListNotDeleted().Result;
            Assert.IsTrue(resUpdate.Success && resNotList.First().Name == "another");
        }

        [TestMethod]
        public void TestUpdateRegionAsync()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            item.Name = "another";
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Name == "another");
        }

        [TestMethod]
        public void TestDeleteRegion()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            var resNotList = bo.List().Result.Where(x => !x.IsDeleted).ToList();
            Assert.IsTrue(resDelete.Success && resNotList.Count == 0);
        }

        [TestMethod]
        public void TestDeleteRegionAsync()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }

        [TestMethod]
        public void TestCreateSameNameRegion()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var item = bo.List().Result.FirstOrDefault();
            var reg = new Region(item.Name);
            var resCreate = bo.Create(reg);
            Assert.IsTrue(resCreate.Success);
        }

        [TestMethod]
        public void TestCreateSameNameRegionAsync()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var item = bo.ListAsync().Result.Result.FirstOrDefault();
            var reg = new Region(item.Name);
            var resCreate = bo.CreateAsync(reg).Result;
            Assert.IsTrue(resCreate.Success);
        }


        [TestMethod]
        public void TestUpdateSameNameRegion()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var reg = new Region("another");
            bo.Create(reg);
            reg.Name = "Covilhã";
            var resUpdate = bo.Update(reg);
            Assert.IsTrue(resUpdate.Success);
        }

        [TestMethod]
        public void TestUpdateSameNameRegionAsync()
        {
            ContextSeeder.Seed();
            var bo = new RegionBusinessObject();
            var reg = new Region("another");
            bo.Create(reg);
            reg.Name = "Covilhã";
            var resUpdate = bo.UpdateAsync(reg).Result;
            Assert.IsTrue(resUpdate.Success);
        }
    }
}
