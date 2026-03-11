namespace EnsekMeterReadingAPI.Models.ReturnObjects
{
    /// <summary>
    /// Model class for meter reading that failed
    /// </summary>
    public class MeterReadingFailed
    {
        public string AccountId { get; set; }

        public string MeterReadingDateTime { get; set; }

        public string MeterReadingValue { get; set; }

        public string ErrorDescription { get; set; }
    }
}