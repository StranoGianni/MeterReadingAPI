using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnsekMeterReadingAPI.Models.RequestObjects
{
    /// <summary>
    /// Model class for meter reading
    /// </summary>
    public class MeterReading
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MeterReadingId { get; set; }

        public int AccountId { get; set; }

        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadingValue { get; set; }
    }
}
