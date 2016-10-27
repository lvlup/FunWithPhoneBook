using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using Newtonsoft.Json;
using PhoneBook.DataLayer.Entity;

namespace DataAccessLayer
{
   public class PhoneBookDBInitializer : CreateDatabaseIfNotExists<PhoneBookDbContext>
    {
       protected override void Seed(PhoneBookDbContext context)
       {
            using (StreamReader reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"Data\FakeContacts.json")))
            {
                string contactsJson = reader.ReadToEnd();
                var contacts = JsonConvert.DeserializeObject<List<PhoneContact>>(contactsJson);
                foreach (var cont in contacts)
                {
                    context.Contacts.Add(cont);
                }
            }

            base.Seed(context);
       }
    }
}
