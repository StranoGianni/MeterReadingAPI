using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EnsekMeterReadingAPI.helpers;
using EnsekMeterReadingAPI.Models.DBObjects;
using EnsekMeterReadingAPI.Models.RequestObjects;
using EnsekMeterReadingAPI.Models.ReturnObjects;
using EnsekMeterReadingAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EnsekMeterReadingAPI.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingRepository _meterReadingRepository;
        public MeterReadingHelper meterReadingHelperObj = new MeterReadingHelper();

        public MeterReadingController(IMeterReadingRepository meterReadingRepository)
        {
            _meterReadingRepository = meterReadingRepository;
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        public async Task<ActionResult<MeterReadingResult>> PostMeterReadingFile()
        {
            using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
            return await _meterReadingRepository.CreateMeterReading(reader);
        }

        [HttpGet]
        [Route("seed-accounts")]
        public string GetSeedMeterReadingAccounts()
        {
            return _meterReadingRepository.SeedMeterReadingAccounts();
        }

        [HttpGet]
        [Route("clear-database-entries")]
        public string GetClearDB()
        {
            return _meterReadingRepository.ClearMeterReadingDB();
        }

        [HttpGet]
        [Route("meter-reading-entries")]
        public async Task<IEnumerable<MeterReading>> GetAllMeterReadings()
        {
            return await _meterReadingRepository.GetAllMeterReadings();
        }

        [HttpGet]
        [Route("all-accounts")]
        public async Task<IEnumerable<MeterReadingAccount>> GetAllAccounts()
        {
            return await _meterReadingRepository.GetAllAccounts();
        }

        [HttpGet]
        [Route("single-account/{accountId}")]
        public async Task<MeterReadingAccount> GetSingleAccount(int accountId)
        {
            return await _meterReadingRepository.GetSingleAccount(accountId);
        }
    }
}
