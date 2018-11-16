using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore_Web_Shop_Application.Domain.Abstract;

namespace SportsStore_Web_Shop_Application.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List()
        {
            return View(repository.Products);
        }

    }
}