using System;
using System.Collections.Generic;
using System.Globalization;
using EnsekMeterReadingAPI.Models.RequestObjects;
using EnsekMeterReadingAPI.Models.ReturnObjects;

public class Meter_Reading_No_EntriesMock
{
    //Extract to a helper class
    private static DateTime parsedDate(string theDate)
    {
        DateTime dateParsed = DateTime.ParseExact(theDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        return dateParsed;
    }

    public static MeterReadingResult meterReadingNoEntriesMockData = new MeterReadingResult()
    {
        TotalValid = 26,
        MeterReadingValidResults = new List<MeterReading>()
        {
            new MeterReading() { MeterReadingId = 0, AccountId = 2344, MeterReadingDateTime = parsedDate("2019-04-22T09:24:00"), MeterReadingValue = 1002 },
            new MeterReading() { MeterReadingId = 1, AccountId = 2344, MeterReadingDateTime = parsedDate("2019-04-22T09:24:00"), MeterReadingValue = 1780 },
            new MeterReading() { MeterReadingId = 2, AccountId = 2233, MeterReadingDateTime = parsedDate("2019-04-22T12:25:00"), MeterReadingValue = 323 },
            new MeterReading() { MeterReadingId = 3, AccountId = 8766, MeterReadingDateTime = parsedDate("2019-04-22T12:25:00"), MeterReadingValue = 3440 },
            new MeterReading() { MeterReadingId = 4, AccountId = 2344, MeterReadingDateTime = parsedDate("2019-04-22T12:25:00"), MeterReadingValue = 1002 },
            new MeterReading() { MeterReadingId = 5, AccountId = 2345, MeterReadingDateTime = parsedDate("2019-04-22T12:25:00"), MeterReadingValue = 45522 },
            new MeterReading() { MeterReadingId = 6, AccountId = 2347, MeterReadingDateTime = parsedDate("2019-04-22T12:25:00"), MeterReadingValue = 54 },
            new MeterReading() { MeterReadingId = 7, AccountId = 2348, MeterReadingDateTime = parsedDate("2019-04-22T12:25:00"), MeterReadingValue = 123 },
            new MeterReading() { MeterReadingId = 8, AccountId = 2350, MeterReadingDateTime = parsedDate("2019-04-22T12:25:00"), MeterReadingValue = 5684 },
            new MeterReading() { MeterReadingId = 9, AccountId = 2351, MeterReadingDateTime = parsedDate("2019-04-22T12:25:00"), MeterReadingValue = 57579 },
            new MeterReading() { MeterReadingId = 10, AccountId = 2352, MeterReadingDateTime = parsedDate("2019-04-22T12:25:00"), MeterReadingValue = 455 },
            new MeterReading() { MeterReadingId = 11, AccountId = 2353, MeterReadingDateTime = parsedDate("2019-04-22T12:25:00"), MeterReadingValue = 1212 },
            new MeterReading() { MeterReadingId = 12, AccountId = 2355, MeterReadingDateTime = parsedDate("2019-05-06T09:24:00"), MeterReadingValue = 1 },
            new MeterReading() { MeterReadingId = 13, AccountId = 2356, MeterReadingDateTime = parsedDate("2019-05-07T09:24:00"), MeterReadingValue = 0 },
            new MeterReading() { MeterReadingId = 14, AccountId = 6776, MeterReadingDateTime = parsedDate("2019-05-10T09:24:00"), MeterReadingValue = 23566 },
            new MeterReading() { MeterReadingId = 15, AccountId = 1234, MeterReadingDateTime = parsedDate("2019-05-12T09:24:00"), MeterReadingValue = 9787 },
            new MeterReading() { MeterReadingId = 16, AccountId = 1239, MeterReadingDateTime = parsedDate("2019-05-17T09:24:00"), MeterReadingValue = 45345 },
            new MeterReading() { MeterReadingId = 17, AccountId = 1240, MeterReadingDateTime = parsedDate("2019-05-18T09:24:00"), MeterReadingValue = 978 },
            new MeterReading() { MeterReadingId = 18, AccountId = 1241, MeterReadingDateTime = parsedDate("2019-04-11T09:24:00"), MeterReadingValue = 436 },
            new MeterReading() { MeterReadingId = 19, AccountId = 1242, MeterReadingDateTime = parsedDate("2019-05-20T09:24:00"), MeterReadingValue = 124 },
            new MeterReading() { MeterReadingId = 20, AccountId = 1243, MeterReadingDateTime = parsedDate("2019-05-21T09:24:00"), MeterReadingValue = 77 },
            new MeterReading() { MeterReadingId = 21, AccountId = 1244, MeterReadingDateTime = parsedDate("2019-05-25T09:24:00"), MeterReadingValue = 3478 },
            new MeterReading() { MeterReadingId = 22, AccountId = 1245, MeterReadingDateTime = parsedDate("2019-05-25T14:26:00"), MeterReadingValue = 676 },
            new MeterReading() { MeterReadingId = 23, AccountId = 1246, MeterReadingDateTime = parsedDate("2019-05-25T09:24:00"), MeterReadingValue = 3455 },
            new MeterReading() { MeterReadingId = 24, AccountId = 1247, MeterReadingDateTime = parsedDate("2019-05-25T09:24:00"), MeterReadingValue = 3 },
            new MeterReading() { MeterReadingId = 25, AccountId = 1248, MeterReadingDateTime = parsedDate("2019-05-26T09:24:00"), MeterReadingValue = 3467 }
        },
        TotalFailed = 10,
        MeterReadingFailedResults = new List<MeterReadingFailed>()
        {
            new MeterReadingFailed() { AccountId = "2346", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "999999", ErrorDescription = "Error: The value provided is in wrong format - Allowed format: numeric NNNNN" },
            new MeterReadingFailed() { AccountId = "2349", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "VOID", ErrorDescription = "Error: The value provided is in wrong format - Allowed format: numeric NNNNN" },
            new MeterReadingFailed() { AccountId = "2354", MeterReadingDateTime = "22/04/2019 12:25", MeterReadingValue = "00889", ErrorDescription = "Error: The Account Id does not exists." },
            new MeterReadingFailed() { AccountId = "2344", MeterReadingDateTime = "08/05/2019 09:24", MeterReadingValue = "0X765", ErrorDescription = "Error: The value provided is in wrong format - Allowed format: numeric NNNNN" },
            new MeterReadingFailed() { AccountId = "6776", MeterReadingDateTime = "09/05/2019 09:24", MeterReadingValue = "-06575", ErrorDescription = "Error: The value provided is in wrong format - Allowed format: numeric NNNNN" },
            new MeterReadingFailed() { AccountId = "4534", MeterReadingDateTime = "11/05/2019 09:24", MeterReadingValue = "", ErrorDescription = "Error: The provided value is empty. " },
            new MeterReadingFailed() { AccountId = "1235", MeterReadingDateTime = "13/05/2019 09:24", MeterReadingValue = "", ErrorDescription = "Error: The Account Id does not exists.Error: The provided value is empty. " },
            new MeterReadingFailed() { AccountId = "1236", MeterReadingDateTime = "10/04/2019 19:34", MeterReadingValue = "08898", ErrorDescription = "Error: The Account Id does not exists." },
            new MeterReadingFailed() { AccountId = "1237", MeterReadingDateTime = "15/05/2019 09:24", MeterReadingValue = "03455", ErrorDescription = "Error: The Account Id does not exists." },
            new MeterReadingFailed() { AccountId = "1238", MeterReadingDateTime = "16/05/2019 09:24", MeterReadingValue = "00000", ErrorDescription = "Error: The Account Id does not exists." },

        }
    };
}

        //new MeterReading() { MeterReadingId = 0, AccountId = 2344, MeterReadingDateTime = parsedDate("22/04/2019 09:24"), MeterReadingValue = 1002 },
        //new MeterReading() { MeterReadingId = 1, AccountId = 2344, MeterReadingDateTime = parsedDate("2019/04/22 09:24"), MeterReadingValue = 1780 },
        //new MeterReading() { MeterReadingId = 2, AccountId = 2233, MeterReadingDateTime = parsedDate("2019/04/22 12:25"), MeterReadingValue = 323 },
        //new MeterReading() { MeterReadingId = 3, AccountId = 8766, MeterReadingDateTime = parsedDate("2019/04/22 12:25"), MeterReadingValue = 3440 },
        //new MeterReading() { MeterReadingId = 4, AccountId = 2344, MeterReadingDateTime = parsedDate("2019/04/22 12:25"), MeterReadingValue = 1002 },
        //new MeterReading() { MeterReadingId = 5, AccountId = 2345, MeterReadingDateTime = parsedDate("2019/04/22 12:25"), MeterReadingValue = 45522 },
        //new MeterReading() { MeterReadingId = 6, AccountId = 2347, MeterReadingDateTime = parsedDate("2019/04/22 12:25"), MeterReadingValue = 54 },
        //new MeterReading() { MeterReadingId = 7, AccountId = 2348, MeterReadingDateTime = parsedDate("2019/04/22 12:25"), MeterReadingValue = 123 },
        //new MeterReading() { MeterReadingId = 8, AccountId = 2350, MeterReadingDateTime = parsedDate("2019/04/22 12:25"), MeterReadingValue = 5684 },
        //new MeterReading() { MeterReadingId = 9, AccountId = 2351, MeterReadingDateTime = parsedDate("2019/04/22 12:25"), MeterReadingValue = 57579 },
        //new MeterReading() { MeterReadingId = 10, AccountId = 2352, MeterReadingDateTime = parsedDate("2019/04/22 12:25"), MeterReadingValue = 455 },
        //new MeterReading() { MeterReadingId = 11, AccountId = 2353, MeterReadingDateTime = parsedDate("2019/04/22 12:25"), MeterReadingValue = 1212 },
        //new MeterReading() { MeterReadingId = 12, AccountId = 2355, MeterReadingDateTime = parsedDate("2019/05/06 09:24"), MeterReadingValue = 1 },
        //new MeterReading() { MeterReadingId = 13, AccountId = 2356, MeterReadingDateTime = parsedDate("2019/05/07 09:24"), MeterReadingValue = 0 },
        //new MeterReading() { MeterReadingId = 14, AccountId = 6776, MeterReadingDateTime = parsedDate("2019/05/10 09:24"), MeterReadingValue = 23566 },
        //new MeterReading() { MeterReadingId = 15, AccountId = 1234, MeterReadingDateTime = parsedDate("2019/05/12 09:24"), MeterReadingValue = 9787 },
        //new MeterReading() { MeterReadingId = 16, AccountId = 1239, MeterReadingDateTime = parsedDate("2019/05/17 09:24"), MeterReadingValue = 45345 },
        //new MeterReading() { MeterReadingId = 17, AccountId = 1240, MeterReadingDateTime = parsedDate("2019/05/18 09:24"), MeterReadingValue = 978 },
        //new MeterReading() { MeterReadingId = 18, AccountId = 1241, MeterReadingDateTime = parsedDate("2019/04/11 09:24"), MeterReadingValue = 436 },
        //new MeterReading() { MeterReadingId = 19, AccountId = 1242, MeterReadingDateTime = parsedDate("2019/05/20 09:24"), MeterReadingValue = 124 },
        //new MeterReading() { MeterReadingId = 20, AccountId = 1243, MeterReadingDateTime = parsedDate("2019/05/21 09:24"), MeterReadingValue = 77 },
        //new MeterReading() { MeterReadingId = 21, AccountId = 1244, MeterReadingDateTime = parsedDate("2019/05/25 09:24"), MeterReadingValue = 3478 },
        //new MeterReading() { MeterReadingId = 22, AccountId = 1245, MeterReadingDateTime = parsedDate("2019/05/25 14:26"), MeterReadingValue = 676 },
        //new MeterReading() { MeterReadingId = 23, AccountId = 1246, MeterReadingDateTime = parsedDate("2019/05/25 09:24"), MeterReadingValue = 3455 },
        //new MeterReading() { MeterReadingId = 24, AccountId = 1247, MeterReadingDateTime = parsedDate("2019/05/25 09:24"), MeterReadingValue = 3 },
        //new MeterReading() { MeterReadingId = 25, AccountId = 1248, MeterReadingDateTime = parsedDate("2019/05/26 09:24"), MeterReadingValue = 3467 }

 