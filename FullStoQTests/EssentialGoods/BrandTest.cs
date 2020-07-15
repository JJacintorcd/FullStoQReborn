using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FullStoQTests.EssentialGoods
{
    [TestClass]
    public class BrandTest
    {
        [TestMethod]
        public void TestCreateAndReadBrand()
        {
            ContextSeeder.Seed();
            var bo = new BrandBusinessObject();
            var reg = new Brand("Dona Ermelinda");
            var resCreate = bo.Create(reg);
            var resGet = bo.Read(reg.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateAndReadBrandAsync()
        {
            ContextSeeder.Seed();
            var bo = new BrandBusinessObject();
            var reg = new Brand("Dona Ermelinda");
            var resCreate = bo.CreateAsync(reg).Result;
            var resGet = bo.ReadAsync(reg.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListBrand()
        {
            ContextSeeder.Seed();
            var bo = new BrandBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListBrandAsync()
        {
            ContextSeeder.Seed();
            var bo = new BrandBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateBrand()
        {
            ContextSeeder.Seed();
            var bo = new BrandBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            item.Name = "It's just wine";
            var resUpdate = bo.Update(item);
            var resNotList = bo.ListNotDeleted().Result;
            Assert.IsTrue(resUpdate.Success && resNotList.First().Name == "It's just wine");
        }

        [TestMethod]
        public void TestUpdateBrandAsync()
        {
            ContextSeeder.Seed();
            var bo = new BrandBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            item.Name = "It's just wine";
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Name == "It's just wine");
        }

        [TestMethod]
        public void TestDeleteBrand()
        {
            ContextSeeder.Seed();
            var bo = new BrandBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            var resNotList = bo.List().Result.Where(x => !x.IsDeleted).ToList();
            Assert.IsTrue(resDelete.Success && resNotList.Count == 0);
        }

        [TestMethod]
        public void TestDeleteBrandAsync()
        {
            ContextSeeder.Seed();
            var bo = new BrandBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }

        [TestMethod]
        public void TestCreateSameNameBrand()
        {
            ContextSeeder.Seed();
            var bo = new BrandBusinessObject();
            var item = bo.List().Result.FirstOrDefault();
            var reg = new Brand(item.Name);
            var resCreate = bo.Create(reg);
            Assert.IsTrue(!resCreate.Success);
        }

        [TestMethod]
        public void TestCreateSameNameBrandAsync()
        {
            ContextSeeder.Seed();
            var bo = new BrandBusinessObject();
            var item = bo.ListAsync().Result.Result.FirstOrDefault();
            var reg = new Brand(item.Name);
            var resCreate = bo.CreateAsync(reg).Result;
            Assert.IsTrue(!resCreate.Success);
        }
    }
}
