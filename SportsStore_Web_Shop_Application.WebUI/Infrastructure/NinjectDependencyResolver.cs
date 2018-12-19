using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using SportsStore_Web_Shop_Application.Domain.Entities;
using System.Configuration;
using SportsStore_Web_Shop_Application.Domain.Abstract;
using SportsStore_Web_Shop_Application.Domain.Concrete;
using SportsStore_Web_Shop_Application.WebUI.Infrastructure.Abstract;
using SportsStore_Web_Shop_Application.WebUI.Infrastructure.Concrete;

namespace SportsStore_Web_Shop_Application.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
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

        public void AddBindings()
        {

            kernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSettings emailSettings = new EmailSettings()
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };


            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>()
            //{
            //    new Product {Name = "Piłka nożna", Price = 25},
            //    new Product {Name = "Deska surfingowa", Price = 179},
            //    new Product {Name = "Buty do biegania", Price = 95}
            //});

            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}