using ContactManager.Core.DTOs;
using ContactManager.Core.Helpers;
using ContactManager.Core.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Services
{
    public class CsvFileParserService : IFileParserService
    {
        public async Task<List<ContactDto>> ParseCsvAsync(string tempFilePath)
        {
            using var reader = new StreamReader(tempFilePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
            });

            var records = new List<ContactDto>(); 
            csv.Context.RegisterClassMap<ContactDtoMap>();
            await foreach (var record in csv.GetRecordsAsync<ContactDto>())
            {
                records.Add(record);
            }

            return records;
        }
    }
}