using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Services.Interfaces;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using NUnit.Framework;
using PhoneBook.DataLayer.Entity;
using PhoneBook.DataLayer.Repository.Interfaces;
using PhoneBook.WebUi.Controllers;
using Ploeh.AutoFixture;

namespace PhoneBook.Tests
{
    [TestFixture]
    public class PhoneContactControllerTests
    {
        private readonly Fixture _fixture;
        private  Mock<IPhoneBookRepository> _mockRepository;
        private readonly Mock<IExportContactsService> _mockExportContactsService;
        private  PhoneContactController _phoneContactController;

        public PhoneContactControllerTests()
        {
            _fixture = new Fixture();
            _mockRepository = new Mock<IPhoneBookRepository>();
           

            _mockExportContactsService = new Mock<IExportContactsService>();
        }

        [Test]
        public void Create_AddNewPhoneContact_ShouldCallInsertWithSave()
        {
            //arrange
            _phoneContactController = new PhoneContactController(_mockRepository.Object,
            _mockExportContactsService.Object);
            var phoneContact = _fixture.Build<PhoneContact>().Create();

            //act
            _phoneContactController.Create(phoneContact);

            //assert
            using (new AssertionScope())
            {
                _mockRepository.Verify(m => m.InsertPhoneContact(It.Is<PhoneContact>(p => p.Id == phoneContact.Id)),
                    Times.Once());
                _mockRepository.Verify(m => m.Save(), Times.Once());
            }
        }

        [Test]
        public void Edit_UpdatePhoneContact_ShouldCallUpdateWithSave()
        {
            //arrange
            _phoneContactController = new PhoneContactController(_mockRepository.Object,
               _mockExportContactsService.Object);
            _mockRepository.Setup(m => m.GetPhoneContacts(string.Empty)).Returns(new List<PhoneContact>
            {
                new PhoneContact() {Id = 1, Name = "Jack", Surname = "Surname1"},
                new PhoneContact() {Id = 2, Name = "Corki", Surname = "Surname2"},
                new PhoneContact() {Id = 3, Name = "Leo", Surname = "Surname3"},
                new PhoneContact() {Id = 4, Name = "Mort", Surname = "Surname4"},
                new PhoneContact() {Id = 5, Name = "Exmar", Surname = "Surname5"}
            });
            var phoneContact = _mockRepository.Object.GetPhoneContacts(string.Empty).First();
            phoneContact.Name = "NewName";

            //act
            _phoneContactController.Edit(phoneContact);

            //assert
            using (new AssertionScope())
            {
                _mockRepository.Verify(m => m.UpdatePhoneContact(It.Is<PhoneContact>(p => p.Id == phoneContact.Id)),
                    Times.Once());
                _mockRepository.Verify(m => m.Save(), Times.Once());
            }
        }

        [Test]
        public void Delete_DeletePhoneContact_ShouldCallDeleteWithSave()
        {
            //arrange
            _phoneContactController = new PhoneContactController(_mockRepository.Object,
              _mockExportContactsService.Object);
            var phoneContact = _fixture.Build<PhoneContact>().Create();

            //act
            _phoneContactController.Delete(phoneContact.Id);

            //assert
            using (new AssertionScope())
            {
                _mockRepository.Verify(m => m.DeletePhoneContact(It.Is<int>(id => id == phoneContact.Id)), Times.Once());
                _mockRepository.Verify(m => m.Save(), Times.Once());
            }
        }

        [Test]
        public void PhoneContacts_SearhPhoneContact_ShouldFindContactWithNameJack()
        {
           // arrange
            _mockRepository.Setup(m => m.GetPhoneContacts("ack")).Returns(new List<PhoneContact>
            {
                new PhoneContact() {Id = 1, Name = "Jack", Surname = "Surname1"},
                new PhoneContact() {Id = 2, Name = "Corki", Surname = "Surname2"},
                new PhoneContact() {Id = 3, Name = "Leo", Surname = "Surname3"},
                new PhoneContact() {Id = 4, Name = "Mort", Surname = "Surname4"}
            });
            _phoneContactController = new PhoneContactController(_mockRepository.Object,
               _mockExportContactsService.Object);

           // act
            var contacts = (List<PhoneContact>)_phoneContactController.PhoneContacts("ack").Model;

            //assert
            using (new AssertionScope())
            {
                contacts.First().Name.ShouldBeEquivalentTo("Jack");
                contacts.Should().HaveCount(1);
            }
        }

        [Test]
        public void ExportContacts_ExportingContacts_ShouldBeFileWithNameContacts()
        {
            //arrange
            _phoneContactController = new PhoneContactController(_mockRepository.Object,
               _mockExportContactsService.Object);

            //act
            var fileStreamResult = _phoneContactController.ExportContacts(string.Empty);

            //assert
            fileStreamResult.FileDownloadName.ShouldBeEquivalentTo("contacts.csv");
        }

    }
}
