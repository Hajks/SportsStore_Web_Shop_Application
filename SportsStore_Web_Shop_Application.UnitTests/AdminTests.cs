﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore_Web_Shop_Application.Domain.Abstract;
using SportsStore_Web_Shop_Application.Domain.Entities;
using SportsStore_Web_Shop_Application.WebUI.Controllers;
using SportsStore_Web_Shop_Application.WebUI.Infrastructure.Abstract;
using SportsStore_Web_Shop_Application.WebUI.Models;

namespace SportsStore_Web_Shop_Application.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            });

            AdminController target = new AdminController(mock.Object);

            Product[] result = ((IEnumerable<Product>) target.Index().ViewData.Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);

        }

        [TestMethod]
        public void Can_Edit_Product()
        { 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            });

            AdminController target = new AdminController(mock.Object);

            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            });

            AdminController target = new AdminController(mock.Object);

            Product result = target.Edit(4).ViewData.Model as Product;


            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            AdminController target = new AdminController(mock.Object);

            Product product = new Product {Name = "Test"};

            ActionResult result = target.Edit(product);

            mock.Verify(m => m.SaveProduct(product));

            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            AdminController target = new AdminController(mock.Object);

            Product product = new Product {Name = "Test"};

            target.ModelState.AddModelError("error", "error");

            ActionResult result = target.Edit(product);

            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            Product prod = new Product {ProductID = 2, Name = "Test"};

            Mock<IProductRepository> mock = new Mock <IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                prod,
                new Product {ProductID = 3, Name = "P3"}
            });

            AdminController target = new AdminController(mock.Object);

            target.Delete(prod.ProductID);

            mock.Verify(m => m.DeleteProduct(prod.ProductID));
        }

        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "test1234")).Returns(true);

            LoginViewModel model = new LoginViewModel {UserName = "admin", Password = "test1234"};

            AccountController target = new AccountController(mock.Object);

            ActionResult result = target.Login(model, "/MyURL");

            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult) result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUsername", "badPassword"));

            LoginViewModel model = new LoginViewModel {UserName = "admin", Password = "test1234"};

            AccountController target = new AccountController(mock.Object);

            ActionResult result = target.Login(model, "/MyURL");

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
