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
            var resList = bo.ListNotDeleted();
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
            var resList = bo.ListNotDeletedAsync().Result;
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
            var resList = bo.ListNotDeletedAsync().Result;
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

        [TestMethod]
        public void TestCreateSameVatNumberCompany()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var item = bo.ListNotDeleted().Result.First();
            var est = new Company("Ilhas", item.VatNumber);
            var resCreate = bo.Create(est);
            Assert.IsTrue(!resCreate.Success);
        }

        [TestMethod]
        public void TestCreateSameVatNumberCompanyAsync()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var item = bo.ListNotDeletedAsync().Result.Result.First();
            var est = new Company("Ilhas", item.VatNumber);
            var resCreate = bo.CreateAsync(est).Result;
            Assert.IsTrue(!resCreate.Success);
        }


        #region Update Same Name
        [TestMethod]
        public void TestUpdateSameNameCompany()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var com = new Company("Sonae", 12345);
            bo.Create(com);
            com.Name = "pingo ácido";
            var resUpdate = bo.Update(com);
            Assert.IsTrue(!resUpdate.Success && resUpdate.Message == "Name already exists");
        }
        #endregion

        #region Update Assync Same Name
        [TestMethod]
        public void TestUpdateSameNameCompanyAsync()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var reg = new Company("Sonae", 12345);
            bo.Create(reg);
            reg.Name = "pingo ácido";
            var resUpdate = bo.UpdateAsync(reg).Result;
            Assert.IsTrue(!resUpdate.Success && resUpdate.Message == "Name already exists");
        }
        #endregion

        #region Update Same VAT
        [TestMethod]
        public void TestUpdateSameVATNumberCompany()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var com = new Company("Sonae", 12345);
            bo.Create(com);
            com.VatNumber = 123456789;
            var resUpdate = bo.Update(com);
            Assert.IsTrue(!resUpdate.Success && resUpdate.Message == "Vat number already exists");
        }
        #endregion

        #region Update Assync Same VAT
        [TestMethod]
        public void TestUpdateSameVATNumberCompanyAsync()
        {
            ContextSeeder.Seed();
            var bo = new CompanyBusinessObject();
            var reg = new Company("Sonae", 12345);
            bo.Create(reg);
            reg.VatNumber = 123456789;
            var resUpdate = bo.UpdateAsync(reg).Result;
            Assert.IsTrue(!resUpdate.Success && resUpdate.Message == "Vat number already exists");
        }
        #endregion
    }
}
