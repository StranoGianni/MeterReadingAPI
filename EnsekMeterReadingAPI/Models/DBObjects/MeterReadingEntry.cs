using System;

namespace EnsekMeterReadingAPI.Models.DBObjects
{
    /// <summary>
    /// Model class for meter reading entry
    /// </summary>
    // Not in sue but reatined for further development
    public class MeterReadingEntry
    {
        public int AccountId { get; set; }

        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadingValue { get; set; }
    }
}