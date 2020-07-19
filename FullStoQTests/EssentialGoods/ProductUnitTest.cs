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
    public class ProductUnitTest
    {
        [TestMethod]
        public void TestCreateAndReadProductUnit()
        {
            ContextSeeder.Seed();
            var pmbo = new ProductModelBusinessObject();
            var prodMod = pmbo.ListNotDeleted().Result.First();

            var bo = new ProductUnitBusinessObject();
            var prodUnit = new ProductUnit("werkyt235", false, prodMod.Id);
            var resCreate = bo.Create(prodUnit);

            var resGet = bo.Read(prodUnit.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateAndReadProductUnitAsync()
        {
            ContextSeeder.Seed();
       
            var pmbo = new ProductModelBusinessObject();
            var prodMod = pmbo.ListNotDeletedAsync().Result.Result.First();

            var bo = new ProductUnitBusinessObject();
            var prodUnit = new ProductUnit("werkyt235", false, prodMod.Id);
            var resCreate = bo.CreateAsync(prodUnit).Result;

            var resGet = bo.ReadAsync(prodUnit.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListProductUnit()
        {
            ContextSeeder.Seed();
            var bo = new ProductUnitBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListProductUnitAsync()
        {
            ContextSeeder.Seed();
            var bo = new ProductUnitBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateProductUnit()
        {
            ContextSeeder.Seed();
            var bo = new ProductUnitBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            item.IsReserved = true;
            var resUpdate = bo.Update(item);
            var resNotList = bo.ListNotDeleted().Result;
            Assert.IsTrue(resUpdate.Success && resNotList.First().IsReserved == true);
        }

        [TestMethod]
        public void TestUpdateProductUnitAsync()
        {
            ContextSeeder.Seed();
            var bo = new ProductUnitBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            item.IsReserved = true;
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().IsReserved == true);
        }

        [TestMethod]
        public void TestDeleteProductUnit()
        {
            ContextSeeder.Seed();
            var bo = new ProductModelBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            var resNotList = bo.List().Result.Where(x => !x.IsDeleted).ToList();
            Assert.IsTrue(resDelete.Success && resNotList.Count == 0);
        }

        [TestMethod]
        public void TestDeleteProductUnitAsync()
        {
            ContextSeeder.Seed();
            var bo = new ProductModelBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }

        [TestMethod]
        public void TestCreateSameSerialNumberProductUnit()
        {
            ContextSeeder.Seed();
            var braBo = new BrandBusinessObject();
            var bra = new Brand("Barraca do Tejo");
            braBo.Create(bra);
            var catBo = new CategoryBusinessObject();
            var cat = new Category("Non-Alcoholic Beverages");
            catBo.Create(cat);
            var pmbo = new ProductModelBusinessObject();
            var prodMod = new ProductModel("Vinho Branco", "506-1237-422", 4.24, 0.80, bra.Id, cat.Id);
            pmbo.Create(prodMod);

            var bo = new ProductUnitBusinessObject();
            var item = bo.List().Result.FirstOrDefault();
            var prodUnit = new ProductUnit(item.SerialNumber, false, prodMod.Id);
            var resCreate = bo.Create(prodUnit);
            Assert.IsTrue(!resCreate.Success);
        }

        [TestMethod]
        public void TestCreateSameSerialNumberProductUnitAsync()
        {
            ContextSeeder.Seed();
            var braBo = new BrandBusinessObject();
            var bra = new Brand("Barraca do Tejo");
            braBo.Create(bra);
            var catBo = new CategoryBusinessObject();
            var cat = new Category("Non-Alcoholic Beverages");
            catBo.Create(cat);
            var pmbo = new ProductModelBusinessObject();
            var prodMod = new ProductModel("Vinho Branco", "506-1237-422", 4.24, 0.80, bra.Id, cat.Id);
            pmbo.Create(prodMod);

            var bo = new ProductUnitBusinessObject();
            var item = bo.ListAsync().Result.Result.FirstOrDefault();
            var prodUnit = new ProductUnit(item.SerialNumber, false, prodMod.Id);
            var resCreate = bo.CreateAsync(prodUnit).Result;
            Assert.IsTrue(!resCreate.Success);
        }
    }
}
