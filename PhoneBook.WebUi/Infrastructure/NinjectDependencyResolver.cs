using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Repository.Implementations;
using Ninject;
using PhoneBook.DataLayer.Repository.Interfaces;

namespace PhoneBook.WebUi.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IPhoneBookRepository>().To<PhoneBookRepository>();
            kernel.Bind<IExportContactsService>().To<ExportContactsService>();
        }
    }
}
