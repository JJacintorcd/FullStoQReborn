using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System.Linq;

namespace FullStoQTests.Commercial
{
    [TestClass]
    public class CompanyTests
    {
        #region Create and Read
        [TestMethod]
        public void TestCreateAndReadCompany()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var com = new Company("Sonae", 12345);
            var resCreate = bo.Create(com);
            var resGet = bo.Read(com.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }
        #endregion

        #region Create and Read Async
        [TestMethod]
        public void TestCreateAndReadCompanyAsync()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var reg = new Company("Sonae", 1823445);
            var resCreate = bo.CreateAsync(reg).Result;
            var resGet = bo.ReadAsync(reg.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }
        #endregion

        #region Update
        [TestMethod]
        public void TestUpdateCompany()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            item.Name = "Jerónimo Martins";
            var resUpdate = bo.Update(item);
            var resNotList = bo.ListNotDeleted().Result;
            Assert.IsTrue(resUpdate.Success && resNotList.First().Name == "Jerónimo Martins");
        }
        #endregion

        #region Update Assync
        [TestMethod]
        public void TestUpdateCompanyAsync()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            item.Name = "Jerónimo Martins";
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().Name == "Jerónimo Martins");
        }
        #endregion

        #region Delete
        [TestMethod]
        public void TestDeleteCompany()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var com = new Company("Sonae", 12345);
            bo.Create(com);

            var result = bo.Delete(com.Id);
            Assert.IsTrue(result.Success);
        }
        #endregion        

        #region Assync Delete
        [TestMethod]
        public void TestDeleteCompanyAsync()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
        #endregion

        #region List
        [TestMethod]
        public void TestListCompany()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }
        #endregion

        #region List Async
        [TestMethod]
        public void TestListCompanyAsync()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }
        #endregion
    }
}
