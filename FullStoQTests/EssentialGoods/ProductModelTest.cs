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
    public class ProductModelTest
    {
        [TestMethod]
        public void TestCreateAndReadProductModel()
        {
            ContextSeeder.Seed();
            var braBo = new BrandBusinessObject();
            var bra = new Brand("Barraca do Tejo");
            braBo.Create(bra);
            var catBo = new CategoryBusinessObject();
            var cat = new Category("Non-Alcoholic Beverages");
            catBo.Create(cat);
            var bo = new ProductModelBusinessObject();
            var prodMod = new ProductModel("Vinho Branco", false, "506-1237-422", 4.24, 0.80, bra.Id, cat.Id);
            var resCreate = bo.Create(prodMod);
            var resGet = bo.Read(prodMod.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateAndReadProductModelAsync()
        {
            ContextSeeder.Seed();
            var braBo = new BrandBusinessObject();
            var bra = new Brand("Barraca do Tejo");
            braBo.Create(bra);
            var catBo = new CategoryBusinessObject();
            var cat = new Category("Non-Alcoholic Beverages");
            catBo.Create(cat);
            var bo = new ProductModelBusinessObject();
            var prodMod = new ProductModel("Vinho Branco da Barraca do Tejo", false, "506-1237-422", 4.24, 0.80, bra.Id, cat.Id);
            var resCreate = bo.CreateAsync(prodMod).Result;
            var resGet = bo.ReadAsync(prodMod.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListProductModel()
        {
            ContextSeeder.Seed();
            var bo = new ProductModelBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListProductModelAsync()
        {
            ContextSeeder.Seed();
            var bo = new ProductModelBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateProductModel()
        {
            ContextSeeder.Seed();
            var bo = new ProductModelBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            item.Name = "It's just wine";
            var resUpdate = bo.Update(item);
            var resNotList = bo.ListNotDeleted().Result;
            Assert.IsTrue(resUpdate.Success && resNotList.First().Name == "It's just wine");
        }

        [TestMethod]
        public void TestUpdateProductModelAsync()
        {
            ContextSeeder.Seed();
            var bo = new ProductModelBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            item.Name = "It's just wine";
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Name == "It's just wine");
        }

        [TestMethod]
        public void TestDeleteProductModel()
        {
            ContextSeeder.Seed();
            var bo = new ProductModelBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            var resNotList = bo.List().Result.Where(x => !x.IsDeleted).ToList();
            Assert.IsTrue(resDelete.Success && resNotList.Count == 0);
        }

        [TestMethod]
        public void TestDeleteProductModelAsync()
        {
            ContextSeeder.Seed();
            var bo = new ProductModelBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
    }
}
