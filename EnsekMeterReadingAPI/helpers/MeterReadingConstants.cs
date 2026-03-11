using System;
using System.IO;

namespace EnsekMeterReadingAPI.helpers
{
    public class MeterReadingConstants
    {
        public MeterReadingConstants()
        {
        }

        public static void FilePathValidation(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("Loading failure - File " + Path.GetFileName(filePath) + " not found");
            }
        }

        public static int MaxLengthReading { get { return 5; } }
        public static string ValidEntry { get { return "Valid Entry"; } }
        public static string DuplicatedEntry { get { return "Duplicated Entry"; } }
        public static string AccountIDError { get { return "Error: The Account Id does not exists."; } }
        public static string EmptyValue { get { return "Error: The provided value is empty. "; } }
        public static string WrongFormat { get { return "Error: The value provided is in wrong format - Allowed format: numeric NNNNN"; } }
        public static string MeterReadingDBCleared { get { return "Meter Reading records cleared."; } }
        public static string NoDBCleared { get { return "Not need to clear database"; } }
        public static string AccountsAlreadySeeded { get { return "Meter Account database already seeded"; } }
        public static string AccountsSuccesfullySeeded { get { return "Meter Reading account succesfully seeded"; } }
    }
}
