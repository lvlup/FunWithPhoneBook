using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PhoneBook.DataLayer.Entity;
using PhoneBook.DataLayer.Repository.Interfaces;

namespace DataAccessLayer.Repository.Implementations
{
   public class PhoneBookRepository:IPhoneBookRepository
    {
        readonly PhoneBookDbContext context = new PhoneBookDbContext();

        public IEnumerable<PhoneContact> GetPhoneContacts(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                return context.Contacts.Where(c => c.Surname.ToLower().Contains(searchString)
                                            || c.Name.ToLower().Contains(searchString)
                                            || c.PhoneNumber.ToString().ToLower().Contains(searchString)).ToList();
            }
            return context.Contacts.ToList();
        }

       public void InsertPhoneContact(PhoneContact contact)
       {
           context.Contacts.Add(contact);
       }

       public void UpdatePhoneContact(PhoneContact contact)
       {
            context.Entry(contact).State = EntityState.Modified;
        }

       public void DeletePhoneContact(int contactId)
       {
            PhoneContact order = context.Contacts.Find(contactId);
            context.Contacts.Remove(order);
        }

       public void Save()
       {
           context.SaveChanges();
       }
    }
}
