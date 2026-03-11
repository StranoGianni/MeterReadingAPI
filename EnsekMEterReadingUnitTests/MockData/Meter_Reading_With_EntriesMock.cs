using System.Collections.Generic;
using EnsekMeterReadingAPI.Models.RequestObjects;
using EnsekMeterReadingAPI.Models.ReturnObjects;

public class Meter_Reading_With_EntriesMock
{

    public static MeterReadingResult meterReadingNoEntriesMockData = new MeterReadingResult()
    {
        TotalValid = 0,
        MeterReadingValidResults = new List<MeterReading>(),
        TotalFailed = 36,
        MeterReadingFailedResults = new List<MeterReadingFailed>()
        {
            new MeterReadingFailed() { AccountId = "2344", MeterReadingDateTime = "22/04/2019 09:24", MeterReadingValue = "01002", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2344", MeterReadingDateTime = "22/04/2019 09:24", MeterReadingValue = "01780", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2233", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "00323", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "8766", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "03440", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2344", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "01002", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2345", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "45522", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2346", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "999999", ErrorDescription = "Error: The value provided is in wrong format - Allowed format: numeric NNNNN"},
            new MeterReadingFailed() { AccountId = "2347", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "00054", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2348", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "00123", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2349", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "VOID", ErrorDescription = "Error: The value provided is in wrong format - Allowed format: numeric NNNNN"},
            new MeterReadingFailed() { AccountId = "2350", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "05684", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2351", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "57579", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2352", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "00455", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2353", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "01212", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2354", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "00889", ErrorDescription = "Error: The Account Id does not exists."},
            new MeterReadingFailed() { AccountId = "2355", MeterReadingDateTime = "06/05/2019 09:24", MeterReadingValue = "00001", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2356", MeterReadingDateTime = "07/05/2019 09:24", MeterReadingValue = "00000", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "2344", MeterReadingDateTime = "08/05/2019 09:24", MeterReadingValue = "0X765", ErrorDescription = "Error: The value provided is in wrong format - Allowed format: numeric NNNNN"},
            new MeterReadingFailed() { AccountId = "6776", MeterReadingDateTime = "09/05/2019 09:24", MeterReadingValue = "-06575", ErrorDescription = "Error: The value provided is in wrong format - Allowed format: numeric NNNNN"},
            new MeterReadingFailed() { AccountId = "6776", MeterReadingDateTime = "10/05/2019 09:24", MeterReadingValue = "23566", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "4534", MeterReadingDateTime = "11/05/2019 09:24", MeterReadingValue = "", ErrorDescription = "Error: The provided value is empty. "},
            new MeterReadingFailed() { AccountId = "1234", MeterReadingDateTime = "12/05/2019 09:24", MeterReadingValue = "09787", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "1235", MeterReadingDateTime = "13/05/2019 09:24", MeterReadingValue = "", ErrorDescription = "Error: The Account Id does not exists.Error: The provided value is empty. "},
            new MeterReadingFailed() { AccountId = "1236", MeterReadingDateTime = "10/04/2019 19:34", MeterReadingValue = "08898", ErrorDescription = "Error: The Account Id does not exists."},
            new MeterReadingFailed() { AccountId = "1237", MeterReadingDateTime = "15/05/2019 09:24", MeterReadingValue = "03455", ErrorDescription = "Error: The Account Id does not exists."},
            new MeterReadingFailed() { AccountId = "1238", MeterReadingDateTime = "16/05/2019 09:24", MeterReadingValue = "00000", ErrorDescription = "Error: The Account Id does not exists."},
            new MeterReadingFailed() { AccountId = "1239", MeterReadingDateTime = "17/05/2019 09:24", MeterReadingValue = "45345", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "1240", MeterReadingDateTime = "18/05/2019 09:24", MeterReadingValue = "00978", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "1241", MeterReadingDateTime = "11/04/2019 09:24", MeterReadingValue = "00436", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "1242", MeterReadingDateTime = "20/05/2019 09:24", MeterReadingValue = "00124", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "1243", MeterReadingDateTime = "21/05/2019 09:24", MeterReadingValue = "00077", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "1244", MeterReadingDateTime = "25/05/2019 09:24", MeterReadingValue = "03478", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "1245", MeterReadingDateTime = "25/05/2019 14:26", MeterReadingValue = "00676", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "1246", MeterReadingDateTime = "25/05/2019 09:24", MeterReadingValue = "03455", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "1247", MeterReadingDateTime = "25/05/2019 09:24", MeterReadingValue = "00003", ErrorDescription = "Duplicated Entry"},
            new MeterReadingFailed() { AccountId = "1248", MeterReadingDateTime = "26/05/2019 09:24", MeterReadingValue = "03467", ErrorDescription = "Duplicated Entry"}
        }
    };
}