using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsekMeterReadingAPI.Controllers;
using EnsekMeterReadingAPI.Models.DBObjects;
using EnsekMeterReadingAPI.Models.ReturnObjects;
using EnsekMeterReadingAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EnsekMEterReadingUnitTests.MeterReadingUnitTests
{
    [TestFixture]
    public class MeterReadingUnitTests
    {
        Mock<IMeterReadingRepository> IMeterReadingRepositoryMock;
        MeterReadingController meterReadingController;
        //MOCK the DbContext - further development")]

        [SetUp]
        public void Setup()
        {
            IMeterReadingRepositoryMock = new Mock<IMeterReadingRepository>();
        }

        [Test]
        public void Get_All_Accounts_test()
        {
            //Arrange
            IMeterReadingRepositoryMock.Setup(x => x.GetAllAccounts()).ReturnsAsync(Test_AccountsMock.testAccountsMockData);
            meterReadingController = new MeterReadingController(IMeterReadingRepositoryMock.Object);

            //Act
            IEnumerable<MeterReadingAccount> accountyEntry = meterReadingController.GetAllAccounts().Result;

            //Assert
            Assert.AreEqual(27, accountyEntry.Select(x => x).ToList().Count);
        }

        [Test]
        [Ignore("Ignore for now - further development")]
        public void Post_Meter_Reading_File_test_no_Entry()
        {
            //Arrange
            var meterReadingFileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SeedDataMock/Meter_Reading.csv");
            using StreamReader reader = new StreamReader(meterReadingFileLocation, Encoding.UTF8);
            IMeterReadingRepositoryMock.Setup(x => x.CreateMeterReading(reader)).ReturnsAsync(Meter_Reading_No_EntriesMock.meterReadingNoEntriesMockData);
            meterReadingController = new MeterReadingController(IMeterReadingRepositoryMock.Object);

            //Act
            ActionResult<MeterReadingResult> meterReadingEntries = meterReadingController.PostMeterReadingFile().Result;
            int totalValid = meterReadingEntries.Value.TotalValid;
            int totalFailed = meterReadingEntries.Value.TotalFailed;

            Assert.AreEqual(26, totalValid);
            Assert.AreEqual(10, totalFailed);
        }

        [Test]
        [Ignore("Ignore for now - further development")]
        public void Get_Seed_Meter_Reading_Accounts_test()
        {
            //Repository function SeedMeterReadingAccounts();
        }

        [Test]
        [Ignore("Ignore for now - further development")]
        public void Get_Clear_DB_test()
        {
            //Repository function ClearMeterReadingDB();
        }

        [Test]
        [Ignore("Ignore for now - further development")]
        public void Get_All_Meter_Readings_test()
        {
            //Repository function GetAllMeterReadings();
        }

        [Test]
        [TestCase(1234)]
        public async Task GetSingleAccountAsync(int accountId)
        {
            IMeterReadingRepositoryMock.Setup(x => x.GetSingleAccount(1234))
                .ReturnsAsync(new MeterReadingAccount { AccountId = 1234, FirstName = "Freya", LastName = "Test" });
            meterReadingController = new MeterReadingController(IMeterReadingRepositoryMock.Object);
            MeterReadingAccount accountyEntry = await meterReadingController.GetSingleAccount(accountId);

            Assert.AreEqual(1234, accountyEntry.AccountId);
            Assert.AreEqual("Freya", accountyEntry.FirstName);
            Assert.AreEqual("Test", accountyEntry.LastName);
        }
    }
}