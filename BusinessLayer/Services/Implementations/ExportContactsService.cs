using System.Collections.Generic;
using System.IO;
using BusinessLayer.Services.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using PhoneBook.DataLayer.Entity;

namespace BusinessLayer.Services.Implementations
{
    public class ExportContactsService : IExportContactsService
    {
        public byte[] WriteCsvToMemory(IEnumerable<PhoneContact> contacts)
        {
            var csvConfig = new CsvConfiguration() {Delimiter = ";"};
            csvConfig.RegisterClassMap<PhoneContactMap>();
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, csvConfig))
            {

                csvWriter.WriteRecords(contacts);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }
    }

    public sealed class PhoneContactMap : CsvClassMap<PhoneContact>
    {
        public PhoneContactMap()
        {
            AutoMap();
            Map(m => m.Id).Ignore();
        }
    }
}
