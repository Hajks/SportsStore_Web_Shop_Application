using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using SportsStore_Web_Shop_Application.Domain.Entities;
using SportsStore_Web_Shop_Application.Domain.Abstract;

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
            return kernel.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void AddBindings()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>()
            {
                new Product {Name = "Piłka nożna", Price = 25},
                new Product {Name = "Deska surfingowa", Price = 179},
                new Product {Name = "Buty do biegania", Price = 95}
            });

            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}