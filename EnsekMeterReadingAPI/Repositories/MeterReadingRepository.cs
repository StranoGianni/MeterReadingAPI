using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EnsekMeterReadingAPI.helpers;
using EnsekMeterReadingAPI.Models.DBObjects;
using EnsekMeterReadingAPI.Models.RequestObjects;
using EnsekMeterReadingAPI.Models.ReturnObjects;
using Microsoft.EntityFrameworkCore;

namespace EnsekMeterReadingAPI.Repositories
{
    /// <summary>
    /// Concrete implementation of the repository
    /// </summary>
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly MeterReadingContext _meterReadingContext;

        public MeterReadingRepository(MeterReadingContext meterReadingContext)
        {
            _meterReadingContext = meterReadingContext;
        }

        //Seed the Account DB by default start application with the provided Test_Accounts.csv
        public string SeedMeterReadingAccounts()
        {
            string accountDBCreated = MeterReadingConstants.AccountsAlreadySeeded;
            try
            {
                if (!_meterReadingContext.MeterReadingAccounts.Any())
                {

                    string fileLocationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SeedData/Test_Accounts.csv");
                    MeterReadingConstants.FilePathValidation(fileLocationPath);
                    accountDBCreated = MeterReadingHelper.SeedDB(fileLocationPath, _meterReadingContext);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to initialize the repository constructor", ex);
            }
            return accountDBCreated;
        }

        //Process the meter reading
        public async Task<MeterReadingResult> CreateMeterReading(StreamReader reader)
        {
            var meterReadingentry = await MeterReadingHelper.ValidateMeterReadingEntry(reader, _meterReadingContext);
            return meterReadingentry;
        }

        //Clear the meter reading entries
        public string ClearMeterReadingDB()
        {
            return MeterReadingHelper.ClearDB(_meterReadingContext);
        }

        //Retrieve all the meter readings
        public async Task<IEnumerable<MeterReading>> GetAllMeterReadings()
        {
            return await _meterReadingContext.MeterReadings.ToListAsync();
        }

        //Retrieve all meter reading accounts
        public async Task<IEnumerable<MeterReadingAccount>> GetAllAccounts()
        {
            return await _meterReadingContext.MeterReadingAccounts.ToListAsync();
        }

        //Retrieve single meter reading accont
        public async Task<MeterReadingAccount> GetSingleAccount(int accountId)
        {
            return await _meterReadingContext.MeterReadingAccounts.FindAsync(accountId);
        }

        //Further development of CRUD operations
        ////Delete a meter reading from the database
        //public async Task DeleteMeterReading(int accountId)
        //{
        //    MeterReadingData meterReadingToDelete = await _meterReadingContext.MeterReadingDatas.FindAsync(accountId);
        //    _meterReadingContext.MeterReadingDatas.Remove(meterReadingToDelete);
        //    await _meterReadingContext.SaveChangesAsync();
        //}

        ////Update a Meter Reading POSSIBLE REMOVE
        //public async Task UpdateMeterReading(MeterReadingData meterReadingData)
        //{
        //    _meterReadingContext.Entry(meterReadingData).State = EntityState.Modified;
        //    await _meterReadingContext.SaveChangesAsync();
        //}
    }
}
