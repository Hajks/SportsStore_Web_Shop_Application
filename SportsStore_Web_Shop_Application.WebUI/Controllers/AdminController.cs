using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore_Web_Shop_Application.Domain.Abstract;

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
        
    }
}