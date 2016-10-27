using System.Collections.Generic;
using PhoneBook.DataLayer.Entity;

namespace PhoneBook.DataLayer.Repository.Interfaces
{
   public interface IPhoneBookRepository
   {
       IEnumerable<PhoneContact> GetPhoneContacts(string searchString);

       void InsertPhoneContact(PhoneContact contact);

       void UpdatePhoneContact(PhoneContact contact);

       void DeletePhoneContact(int contactId);

       void Save();
   }
}
