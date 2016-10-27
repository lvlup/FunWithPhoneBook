using System.Collections.Generic;
using PhoneBook.DataLayer.Entity;

namespace BusinessLayer.Services.Interfaces
{
   public interface IExportContactsService
   {
       byte[] WriteCsvToMemory(IEnumerable<PhoneContact> contacts);
   }
}
