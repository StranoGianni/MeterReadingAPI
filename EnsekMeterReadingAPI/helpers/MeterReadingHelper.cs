using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EnsekMeterReadingAPI.Models.DBObjects;
using EnsekMeterReadingAPI.Models.RequestObjects;
using EnsekMeterReadingAPI.Models.ReturnObjects;
using EnsekMeterReadingAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnsekMeterReadingAPI.helpers
{
    public class MeterReadingHelper
    {
        public MeterReadingHelper()
        {
        }

        public static string SeedDB(string filePath, MeterReadingContext meterReadingContext)
        {
            try
            {
                List<MeterReadingAccount> csvMeterReadingList = new List<MeterReadingAccount>();
                List<string> csvMeterReadingLines = File.ReadAllLines(filePath).Skip(1).ToList();                    
                foreach (string csvMeterReadingLinesItem in csvMeterReadingLines)
                {
                    List<string> value = csvMeterReadingLinesItem.Split(',').ToList();
                    csvMeterReadingList.Add(new MeterReadingAccount { AccountId = int.Parse(value[0]), FirstName = value[1], LastName = value[2] });
                }

                meterReadingContext.MeterReadingAccounts.AddRange(csvMeterReadingList);
                meterReadingContext.SaveChanges();
                return MeterReadingConstants.AccountsSuccesfullySeeded;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute the SeedDB function", ex);
            }
        }

        //Pass Generic class
        public static string ClearDB(MeterReadingContext meterReadingContext)
        {
            try
            {
                string clearDBMessage = string.Empty;

                //check if there is data and if clear or empty
                if (meterReadingContext.MeterReadings.Any())
                {
                    meterReadingContext.MeterReadings.RemoveRange(meterReadingContext.MeterReadings);
                    clearDBMessage = MeterReadingConstants.MeterReadingDBCleared;
                }

                else
                {
                    clearDBMessage = MeterReadingConstants.NoDBCleared;
                }
                meterReadingContext.SaveChanges();
                return clearDBMessage;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute the ClearDB function", ex);
            }
        }

        //Function that validate the entries
        public static async Task<MeterReadingResult> ValidateMeterReadingEntry(StreamReader reader, MeterReadingContext meterReadingContext)
        {
            try
            {
                //Simply read the first line to omit the data headers
                await reader.ReadLineAsync();

                List<MeterReading> readingDatasList = new List<MeterReading>();
                List<int> meterReadingDataIDs = meterReadingContext.MeterReadingAccounts.Select(x => x.AccountId).ToList();
                DbSet<MeterReading> meterReadingActualEntries = meterReadingContext.MeterReadings;
                List<MeterReading> meterReadingListOfEntries = new List<MeterReading>();
                if (meterReadingActualEntries.Any())
                {
                    foreach (MeterReading meterReadingActualEntriesItem in meterReadingActualEntries)
                    {
                        meterReadingListOfEntries.Add(new MeterReading
                        {
                            AccountId = meterReadingActualEntriesItem.AccountId,
                            MeterReadingDateTime = meterReadingActualEntriesItem.MeterReadingDateTime,
                            MeterReadingValue = meterReadingActualEntriesItem.MeterReadingValue
                        });
                    }
                }

                string meterReadingLine;
                int validEntryCounter = GetLastId(meterReadingContext);

                List<MeterReadingFailed> meterReadingFaileds = new List<MeterReadingFailed>();
                while ((meterReadingLine = await reader.ReadLineAsync()) != null)
                {
                    List<string> meterReadingEntryData = meterReadingLine.Split(',').ToList();
                    string meterReadingEntryMessage = IsMeterReadingValid(meterReadingEntryData, meterReadingDataIDs, meterReadingListOfEntries);

                    if (meterReadingEntryMessage == MeterReadingConstants.ValidEntry)
                    {
                        int accountIdEntry = int.Parse(meterReadingEntryData[0]);
                        DateTime meterReadingDateTimeEntry = DateTime.ParseExact(meterReadingEntryData[1], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                        int meterReadingValueEntry = int.Parse(meterReadingEntryData[2]);
                        MeterReading meterReadingEntry = new MeterReading()
                        {
                            MeterReadingId = validEntryCounter,
                            AccountId = accountIdEntry,
                            MeterReadingDateTime = meterReadingDateTimeEntry,
                            MeterReadingValue = meterReadingValueEntry
                        };

                        readingDatasList.Add(meterReadingEntry);
                        validEntryCounter++;
                    }
                    else
                    {
                        meterReadingFaileds.Add(new MeterReadingFailed
                        {
                            AccountId = meterReadingEntryData[0],
                            MeterReadingDateTime = meterReadingEntryData[1],
                            MeterReadingValue = meterReadingEntryData[2],
                            ErrorDescription = meterReadingEntryMessage
                        });
                    }
                }

                meterReadingContext.MeterReadings.AddRange(readingDatasList);
                meterReadingContext.SaveChanges();

                MeterReadingResult meterReadingResult = new MeterReadingResult
                {
                    TotalValid = readingDatasList.Count,
                    MeterReadingValidResults = readingDatasList,
                    TotalFailed = meterReadingFaileds.Count,
                    MeterReadingFailedResults = meterReadingFaileds
                };
                return meterReadingResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute the ValidateMeterReadingEntry function", ex);
            }
        }

        //Function that validate the entry
        private static string IsMeterReadingValid(List<string> meterReadingData, List<int> meterReadingDataIDs, List<MeterReading> meterReadingListOfEntries)
        {
            try
            { 
                string meterReadingEntryMessage = string.Empty;
                bool isAccountIdValid = meterReadingDataIDs.Any(idVal => idVal == int.Parse(meterReadingData[0]));
                bool isValueEmpty = !string.IsNullOrEmpty(meterReadingData[2]);
                bool isValueNumeric = meterReadingData[2].All(char.IsDigit);            
                bool isValueFiveN = meterReadingData[2].Length <= MeterReadingConstants.MaxLengthReading;
                bool isAllValuesValid = isAccountIdValid && isValueEmpty && isValueNumeric && isValueFiveN;                        
                
                if (isAllValuesValid)
                {
                    MeterReading meterReadingEntry = new MeterReading()
                    {
                        AccountId = int.Parse(meterReadingData[0]),
                        MeterReadingDateTime = DateTime.ParseExact(meterReadingData[1], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                        MeterReadingValue = int.Parse(meterReadingData[2])                    
                    };

                    //if all valid then compare the entry
                    if (meterReadingListOfEntries.Any())
                    {
                        bool isMeterReadingOnDB = meterReadingListOfEntries.Any(entry => entry.AccountId == meterReadingEntry.AccountId
                                                      && entry.MeterReadingDateTime == meterReadingEntry.MeterReadingDateTime
                                                      && entry.MeterReadingValue == meterReadingEntry.MeterReadingValue);

                        meterReadingEntryMessage = isMeterReadingOnDB ? MeterReadingConstants.DuplicatedEntry : MeterReadingConstants.ValidEntry;
                        return meterReadingEntryMessage;
                    }
                    meterReadingEntryMessage = MeterReadingConstants.ValidEntry;
                }
                else
                {
                    if (!isAccountIdValid)
                    {
                        meterReadingEntryMessage += MeterReadingConstants.AccountIDError;
                    }

                    if (!isValueEmpty)
                    {
                        meterReadingEntryMessage += MeterReadingConstants.EmptyValue;
                    }

                    if (!isValueNumeric || !isValueFiveN)
                    {
                        meterReadingEntryMessage += MeterReadingConstants.WrongFormat;
                    }
                }

                return meterReadingEntryMessage;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute the IsMeterReadingValid function", ex);
            }
        }

        //Function that return the last meter reading accounts
        private static int GetLastId(MeterReadingContext meterReadingContext)
        {
            try
            {
                int lastId = 0;
                DbSet<MeterReading> meterReadingsObj = meterReadingContext.MeterReadings;
                if (meterReadingsObj.Any())
                {
                    lastId = meterReadingsObj.OrderBy(x => x.MeterReadingId).Select(x => x.MeterReadingId).LastOrDefault() + 1;
                }

                return lastId;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute the GetLastId function", ex);
            }
        } 
    }
}

