using System.Data.Entity;
using PhoneBook.DataLayer.Entity;

namespace DataAccessLayer
{
   public class PhoneBookDbContext :DbContext
    {
        public DbSet<PhoneContact> Contacts { get; set; }

       public PhoneBookDbContext() : base("DefaultConnection")
       {
           Database.SetInitializer(new PhoneBookDBInitializer());
       }

      
    }
}
