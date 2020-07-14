﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using Recodme.RD.FullStoQReborn.DataAccessLayer.Seeders;
using Recodme.RD.FullStoQReborn.DataLayer.Commercial;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FullStoQTests.Person
{
    [TestClass]
    public class ProfileTest
    {
        [TestMethod]
        public void TestCreateAndReadProfiles()
        {
            ContextSeeder.Seed();
            var bo = new ProfileBusinessObject();
            var reg = new Profile(123456789, "Manuel", "Macabres", 919191918, DateTime.UtcNow);
            var resCreate = bo.Create(reg);
            var resGet = bo.Read(reg.Id);
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestCreateAndReadProfileAsync()
        {
            ContextSeeder.Seed();
            var bo = new ProfileBusinessObject();
            var reg = new Profile(123456789, "Manuel", "Macabres", 919191918, DateTime.UtcNow);
            var resCreate = bo.CreateAsync(reg).Result;
            var resGet = bo.ReadAsync(reg.Id).Result;
            Assert.IsTrue(resCreate.Success && resGet.Success && resGet.Result != null);
        }

        [TestMethod]
        public void TestListProfiles()
        {
            ContextSeeder.Seed();
            var bo = new ProfileBusinessObject();
            var resList = bo.List();
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestListProfileAsync()
        {
            ContextSeeder.Seed();
            var bo = new ProfileBusinessObject();
            var resList = bo.ListAsync().Result;
            Assert.IsTrue(resList.Success && resList.Result.Count == 1);
        }

        [TestMethod]
        public void TestUpdateProfiles()
        {
            ContextSeeder.Seed();
            var bo = new ProfileBusinessObject();
            var resList = bo.List();
            var item = resList.Result.FirstOrDefault();
            item.FirstName = "Jorge";
            var resUpdate = bo.Update(item);
            var resNotList = bo.ListNotDeleted().Result;
            Assert.IsTrue(resUpdate.Success && resNotList.First().FirstName == "Jorge");
        }

        [TestMethod]
        public void TestUpdateProfilenAsync()
        {
            ContextSeeder.Seed();
            var bo = new ProfileBusinessObject();
            var resList = bo.ListAsync().Result;
            var item = resList.Result.FirstOrDefault();
            item.FirstName = "Jorge";
            var resUpdate = bo.UpdateAsync(item).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resList.Success && resUpdate.Success && resList.Result.First().FirstName == "Jorge");
        }

        [TestMethod]
        public void TestDeleteProfiles()
        {
            ContextSeeder.Seed();
            var bo = new ProfileBusinessObject();
            var resList = bo.List();
            var resDelete = bo.Delete(resList.Result.First().Id);
            var resNotList = bo.List().Result.Where(x => !x.IsDeleted).ToList();
            Assert.IsTrue(resDelete.Success && resNotList.Count == 0);
        }

        [TestMethod]
        public void TestDeleteProfileAsync()
        {
            ContextSeeder.Seed();
            var bo = new ProfileBusinessObject();
            var resList = bo.ListAsync().Result;
            var resDelete = bo.DeleteAsync(resList.Result.First().Id).Result;
            resList = bo.ListNotDeletedAsync().Result;
            Assert.IsTrue(resDelete.Success && resList.Success && resList.Result.Count == 0);
        }
    }
}