using System.Collections.Generic;
using EnsekMeterReadingAPI.Models.RequestObjects;

namespace EnsekMeterReadingAPI.Models.ReturnObjects
{
    /// <summary>
    /// Model class for final output result
    /// </summary>
    public class MeterReadingResult
    {
        public int TotalValid { get; set; }

        public List<MeterReading> MeterReadingValidResults { get; set; }

        public int TotalFailed { get; set; }

        public List<MeterReadingFailed> MeterReadingFailedResults { get; set; }
    }
}