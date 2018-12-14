using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore_Web_Shop_Application.Domain.Abstract;
using SportsStore_Web_Shop_Application.Domain.Entities;

namespace SportsStore_Web_Shop_Application.WebUI.Controllers
{
    public class AdminController : Controller
    {
       private IProductRepository repository;

       public AdminController(IProductRepository repo)
       {
            repository = repo;
       }

       public ViewResult Index()
       {
            return View(repository.Products);
       }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["Message"] = string.Format("Zapisano {0}", product.Name);
                return RedirectToAction(("Index"));
            }
            else
            {
                //bład
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Usunięto {0}", deletedProduct.Name);
            }

            return RedirectToAction("Index");
        }

        
    }
}