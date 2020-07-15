using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.FullStoQReborn.BusinessLayer.EssentialGoods;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using Recodme.RD.FullStoQReborn.DataLayer.EssentialGoods;
using System.Linq;

namespace FullStoQTests.EssentialGoods
{
    [TestClass]
    public class ShoppingBasketTest
    {
        #region Create and Read
        [TestMethod]
        public void TestCreateAndReadShoppingBasket()
        {
            ContextSeeder.Seed();
            var bo = new ShoppingBasketBusinessObject();
            var pbo = new ProfileBusinessObject();
            var pro = pbo.List().Result.FirstOrDefault();
            var com = new ShoppingBasket(pro.Id);
            var resCreate = bo.Create(com);
            var resGet = bo.Read(com.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }
        #endregion

        #region Create and Read Async
        [TestMethod]
        public void TestCreateAndReadShoppingBasketAsync()
        {
            ContextSeeder.Seed();
            var bo = new ShoppingBasketBusinessObject();
            var pbo = new ProfileBusinessObject();
            var pro = pbo.ListAsync().Result.Result.FirstOrDefault();
            var sb = new ShoppingBasket(pro.Id);
            var resCreate = bo.CreateAsync(sb).Result;
            var resGet = bo.ReadAsync(sb.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }
        #endregion

        #region Update
        [TestMethod]
        public void TestUpdateShoppingBasket()
        {
            ContextSeeder.Seed();
            var bo = new ShoppingBasketBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            var pbo = new ProfileBusinessObject();
            var pro = pbo.List().Result.FirstOrDefault();
            item.ProfileId = pro.Id;
            var resUpdate = bo.Update(item);
            var resNotList = bo.ListNotDeleted().Result;
            Assert.IsTrue(resUpdate.Success && resNotList.First().ProfileId == pro.Id);
        }
        #endregion

        #region Update Assync
        [TestMethod]
        public void TestUpdateShoppingBasketAsync()
        {
            ContextSeeder.Seed();
            var bo = new ShoppingBasketBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            var pbo = new ProfileBusinessObject();
            var pro = pbo.List().Result.FirstOrDefault();
            item.ProfileId = pro.Id;
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().ProfileId == pro.Id);
        }
        #endregion

        #region Delete
        [TestMethod]
        public void TestDeleteShoppingBasket()
        {
            ContextSeeder.Seed();
            var bo = new ShoppingBasketBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            var resNotList = bo.List().Result.Where(x => !x.IsDeleted).ToList();
            Assert.IsTrue(resDelete.Success && resNotList.Count == 0);
        }
        #endregion        

        #region Assync Delete
        [TestMethod]
        public void TestDeleteShoppingBasketAsync()
        {
            ContextSeeder.Seed();
            var bo = new ShoppingBasketBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
        #endregion

        #region List
        [TestMethod]
        public void TestListShoppingBasket()
        {
            ContextSeeder.Seed();
            var bo = new ShoppingBasketBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }
        #endregion

        #region List Async
        [TestMethod]
        public void TestListShoppingBasketAsync()
        {
            ContextSeeder.Seed();
            var bo = new ShoppingBasketBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }
        #endregion
    }
}
