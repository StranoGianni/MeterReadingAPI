using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EnsekMeterReadingAPI.Models.DBObjects;
using EnsekMeterReadingAPI.Models.RequestObjects;
using EnsekMeterReadingAPI.Models.ReturnObjects;

namespace EnsekMeterReadingAPI.Repositories
{
    /// <summary>
    /// Interface that perform operation against database
    /// </summary>
    public interface IMeterReadingRepository
    {
        Task<MeterReadingResult> CreateMeterReading(StreamReader reader);
        string ClearMeterReadingDB();
        string SeedMeterReadingAccounts();
        Task<IEnumerable<MeterReading>> GetAllMeterReadings();
        Task<IEnumerable<MeterReadingAccount>> GetAllAccounts();
        Task<MeterReadingAccount> GetSingleAccount(int accountId);

        //Further development of CRUD operations
        //Task UpdateMeterReading(MeterReadingData meterReadingData);
        //Task DeleteMeterReading(int accountId);
    }
}
