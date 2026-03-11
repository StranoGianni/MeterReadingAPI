using System.ComponentModel.DataAnnotations;

namespace EnsekMeterReadingAPI.Models.DBObjects
{
    /// <summary>
    /// Model class for meter reading accounts
    /// </summary>
    public class MeterReadingAccount
    {
        [Key]
        public int AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
