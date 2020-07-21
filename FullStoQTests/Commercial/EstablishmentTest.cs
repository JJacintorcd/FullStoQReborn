using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using System.Linq;

namespace FullStoQTests.Commercial
{
    [TestClass]
    public class EstablishmentTest
    {
        #region Create and Read
        [TestMethod]
        public void TestCreateReadEstablishment()
        {
            ContextSeeder.Seed();
            var boReg = new RegionBusinessObject();
            var reg1 = boReg.ListNotDeleted().Result.FirstOrDefault();

            var boComp = new CompanyBusinessObject();
            var com1 = boComp.ListNotDeleted().Result.FirstOrDefault();

            var bo = new EstablishmentBusinessObject();
            var est = new Establishment("Avenida da liberdade, numero 1022, Lisboa", "09:00", "20:00", "Domingo", reg1.Id, com1.Id);
            var resCreate = bo.Create(est);
            var resGet = bo.Read(est.Id);

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }
        #endregion

        #region Create and Read Async
        [TestMethod]
        public void TestCreateAndReadEstablishmentAsync()
        {
            ContextSeeder.Seed();

            var boReg = new RegionBusinessObject();
            var reg1 = boReg.ListNotDeleted().Result.First();

            var boComp = new CompanyBusinessObject();
            var com1 = boComp.ListNotDeleted().Result.First();

            var bo = new EstablishmentBusinessObject();
            var est = new Establishment("Avenida da liberdade, numero 1022, Lisboa", "09:00", "20:00", "Domingo", reg1.Id, com1.Id);
            var resCreate = bo.CreateAsync(est).Result;

            var resGet = bo.ReadAsync(est.Id).Result;

            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }
        #endregion

        #region Update
        [TestMethod]
        public void TestUpdateEstablishment()
        {
            ContextSeeder.Seed();
            var bo = new EstablishmentBusinessObject();
            var resList = bo.ListNotDeleted();
            var item = resList.Result.FirstOrDefault();
            item.Address = "Rua Augusta, n 1213, Lisboa";
            var resUpdate = bo.Update(item);
            var resNotList = bo.ListNotDeleted().Result.Where(x => !x.IsDeleted);

            Assert.IsTrue(resUpdate.Success && resNotList.First().Address == "Rua Augusta, n 1213, Lisboa");
        }
        #endregion

        #region Update Async
        [TestMethod]
        public void TestUpdateEstablishmentAsync()
        {
            ContextSeeder.Seed();
            var bo = new EstablishmentBusinessObject();
            var restList = bo.ListNotDeletedAsync().Result;
            var item = restList.Result.FirstOrDefault();
            item.ClosingDays = "Terça-feira";
            var resUpdate = bo.UpdateAsync(item).Result;
            restList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(restList.Success && resUpdate.Success && restList.Result.First().ClosingDays == "Terça-feira");
        }
        #endregion

        #region Delete
        [TestMethod]
        public void TestDeleteEstablishment()
        {
            ContextSeeder.Seed();
            var boReg = new RegionBusinessObject();
            var boComp = new CompanyBusinessObject();
            var reg1 = boReg.ListNotDeleted().Result.First();
            var com1 = boComp.ListNotDeleted().Result.First();

            var objEst = new EstablishmentBusinessObject();
            var est = new Establishment("Rua da pitaia, numero 1234, Açores", "07:00",
                "20:00", "Domingo", reg1.Id, com1.Id);
            objEst.Create(est);
            var res = objEst.Delete(est);
            Assert.IsTrue(res.Success);
        }
        #endregion

        #region Async Delete
        [TestMethod]
        public void TestDeleteEstablishmentAsync()
        {
            ContextSeeder.Seed();
            var bo = new EstablishmentBusinessObject();
            var resList = bo.ListNotDeletedAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
        #endregion

        #region List
        [TestMethod]
        public void TestListEstablishment()
        {
            ContextSeeder.Seed();
            var bo = new EstablishmentBusinessObject();
            var resList = bo.ListNotDeleted();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }
        #endregion

        #region List Async
        [TestMethod]
        public void TestListEstablishmentAsync()
        {
            ContextSeeder.Seed();
            var bo = new EstablishmentBusinessObject();
            var restList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(restList.Success && restList.Result.Count == 1);
        }
        #endregion

        #region Same Address
        [TestMethod]
        public void TestCreateSameAddressRegion()
        {
            ContextSeeder.Seed();
            var bo = new EstablishmentBusinessObject();
            var boReg = new RegionBusinessObject();
            var boComp = new CompanyBusinessObject();
            var reg1 = boReg.ListNotDeleted().Result.First();
            var com1 = boComp.ListNotDeleted().Result.First();
            var item = bo.ListNotDeleted().Result.FirstOrDefault();
            var est = new Establishment(item.Address, "9h00", "21h00", "monday", reg1.Id, com1.Id);
            var resCreate = bo.Create(est);
            Assert.IsTrue(!resCreate.Success);
        }
        #endregion

        #region Same Address Async
        [TestMethod]
        public void TestCreateSameAddressRegionAsync()
        {
            ContextSeeder.Seed();
            var bo = new EstablishmentBusinessObject();
            var boReg = new RegionBusinessObject();
            var boComp = new CompanyBusinessObject();
            var reg1 = boReg.ListNotDeletedAsync().Result.Result.First();
            var com1 = boComp.ListNotDeletedAsync().Result.Result.First();
            var item = bo.ListNotDeletedAsync().Result.Result.FirstOrDefault();
            var est = new Establishment(item.Address, "9h00", "21h00", "monday", reg1.Id, com1.Id);
            var resCreate = bo.CreateAsync(est).Result;
            Assert.IsTrue(!resCreate.Success);
        }
        #endregion


        #region Update Same Address
        [TestMethod]
        public void TestUpdateSameAddressEstablishment()
        {
            ContextSeeder.Seed();

            var boReg = new RegionBusinessObject();
            var reg1 = boReg.ListNotDeleted().Result.First();
            var boComp = new CompanyBusinessObject();
            var com1 = boComp.ListNotDeleted().Result.First();
            var bo = new EstablishmentBusinessObject();
            var est = new Establishment("Avenida da liberdade, numero 1022, Lisboa", "09:00", "20:00", "Domingo", reg1.Id, com1.Id);
            bo.Create(est);
            est.Address = "Avenida da liberdade, numero 1029, Lisboa";
            var resUpdate = bo.Update(est);

            Assert.IsFalse(resUpdate.Success);
        }
        #endregion

        #region Update Async SameAddress
        [TestMethod]
        public void TestUpdateSameNameEstablishmentAsync()
        {
            ContextSeeder.Seed();
            var boReg = new RegionBusinessObject();
            var reg1 = boReg.ListNotDeleted().Result.First();
            var boComp = new CompanyBusinessObject();
            var com1 = boComp.ListNotDeleted().Result.First();
            var bo = new EstablishmentBusinessObject();
            var est = new Establishment("Avenida da liberdade, numero 1022, Lisboa", "09:00", "20:00", "Domingo", reg1.Id, com1.Id);
            bo.Create(est);
            est.Address = "Avenida da liberdade, numero 1029, Lisboa";
            var resUpdate = bo.UpdateAsync(est).Result;
            Assert.IsTrue(!resUpdate.Success);
        }
        #endregion
    }
}
