using ContactManager.Core.DTOs;
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
        public async Task<List<ContactDto>> ParseCsvAsync(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
            });

            var records = new List<ContactDto>();
            await foreach (var record in csv.GetRecordsAsync<ContactDto>())
            {
                records.Add(record);
            }

            return records;
        }
    }
}