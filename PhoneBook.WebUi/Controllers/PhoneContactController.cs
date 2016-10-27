using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BusinessLayer.Services.Interfaces;
using PhoneBook.DataLayer.Entity;
using PhoneBook.DataLayer.Repository.Interfaces;

namespace PhoneBook.WebUi.Controllers
{
    public class PhoneContactController : Controller
    {
        private readonly IPhoneBookRepository phoneBookRepository;
        private readonly IExportContactsService exportContactsService;

        public PhoneContactController(IPhoneBookRepository repository, IExportContactsService exportContactsService)
        {
            phoneBookRepository = repository;
            this.exportContactsService = exportContactsService;
        }

        // GET: PhoneContact
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PhoneContacts(string searchString)
        {
            var contacts = phoneBookRepository.GetPhoneContacts(searchString);
            return PartialView(contacts);
        }

        public ViewResult Create()
        {
            return View();
        }

        public FileStreamResult ExportContacts(string searchString)
        {
            var contacts = phoneBookRepository.GetPhoneContacts(searchString);
            var result = exportContactsService.WriteCsvToMemory(contacts);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "contacts.csv" };
        }

        [HttpPost]
        public ActionResult Create(PhoneContact phoneContact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    phoneBookRepository.InsertPhoneContact(phoneContact);
                    phoneBookRepository.Save();

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Невозможно сохранить изменения. Попробуйте позже.");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = phoneBookRepository.GetPhoneContacts(string.Empty).FirstOrDefault(p => p.Id == id);

            if(customer == null)  return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(PhoneContact phoneContact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    phoneBookRepository.UpdatePhoneContact(phoneContact);
                    phoneBookRepository.Save();

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Невозможно сохранить изменения. Попробуйте позже.");
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            phoneBookRepository.DeletePhoneContact((int)id);
            phoneBookRepository.Save();

            return RedirectToAction("Index");
        }
    }
}