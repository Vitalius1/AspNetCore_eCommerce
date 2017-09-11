using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using eCommerce.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {
        private CommerceContext _context;

        public HomeController(CommerceContext context)
        {
            _context = context;
        }
// ================================================================================================

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Product> AllProducts = _context.Products.Take(5).ToList();
            List<Order> AllOrders = _context.Orders.Include(o => o.Customer).Include(o => o.Product).OrderByDescending(o => o.CreatedAt).Take(3).ToList();
            
            foreach(var order in AllOrders)
            {
                if(order.OrderedQty > 1)
                {
                    order.Product.ProductName += 's';
                }
                // TimeSpan duration = DateTime.Now - order.CreatedAt;
                // order.CreatedAt = duration;
            }
            ViewBag.AllProducts = AllProducts;

            ViewBag.AllOrders = AllOrders;
            return View("Dashboard");
        }
// ================================================================================================
        
        [HttpGet]
        [Route("/orders")]
        public IActionResult ShowOrders()
        {
            ViewBag.ErrorQty = TempData["ErrorQty"];
            List<Customer> AllCustomers = _context.Customers.ToList();
            ViewBag.AllCustomers = AllCustomers;
            List<Product> AllProducts = _context.Products.OrderBy(p => p.ProductName).ToList();
            ViewBag.AllProducts = AllProducts;
            List<Order> AllOrders = _context.Orders.Include(o => o.Customer).Include(o => o.Product).ToList();
            ViewBag.AllOrders = AllOrders;
            return View("Orders", AllOrders);
        }
// ================================================================================================
        
        [HttpGet]
        [Route("/products")]
        public IActionResult ShowProducts()
        {
            List<Product> allProducts = _context.Products.OrderBy(p => p.ProductName).ToList();
            ViewBag.allProducts = allProducts;
            return View("Products");
        }
// ================================================================================================
        
        [HttpGet]
        [Route("/customers")]
        public IActionResult ShowCustomers()
        {
            ViewBag.Exist = TempData["exist"];
            ViewBag.NoEmpty = TempData["NoEmpty"];
            List<Customer> AllCustomers = _context.Customers.ToList();
            return View("Customers", AllCustomers);
        }
// ================================================================================================
        
        [HttpGet]
        [Route("/settings")]
        public IActionResult ShowSettings()
        {
            return View("Settings");
        }
// ================================================================================================

        [HttpPost]
        [Route("addProduct")]
        public IActionResult AddProduct(ProductViewModel NewProduct)
        {
            if(ModelState.IsValid)
            {
                Product prod = new Product
                {
                    ProductName = NewProduct.ProductName,
                    ImageUrl = NewProduct.ImageUrl,
                    Description = NewProduct.Description,
                    TotalQty = NewProduct.TotalQty,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Products.Add(prod);
                _context.SaveChanges();
                return RedirectToAction("ShowProducts");
            }
            return View("Products");
        }
// ================================================================================================

        [HttpPost]
        [Route("addCustomer")]
        public IActionResult AddCustomer(string custName = null)
        {
            if(custName == null)
            {
                TempData["NoEmpty"] = "No empty fields. Please give a name.";
                return RedirectToAction("ShowCustomers");
            }
            bool exist = _context.Customers.Any(c => c.CustomerName == custName);
            if(exist)
            {
                TempData["exist"] = "Customer already exists.";
                return RedirectToAction("ShowCustomers");
            }
            Customer customer = new Customer
            {
                CustomerName = custName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now 
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("ShowCustomers");
        }
// ================================================================================================

        [HttpPost]
        [Route("addOrder")]
        public IActionResult AddOrder(int custId, int prodId, int Qty)
        {
            if(Qty < 1)
            {
                TempData["ErrorQty"] = "You have to place an order for 1 or more pieces of the selected product.";
                return RedirectToAction("ShowOrders");
            }
            Product theProduct = _context.Products.Single(p => p.ProductId == prodId);
            if(Qty > theProduct.TotalQty)
            {
                TempData["ErrorQty"] = "You can not place this order. There are not enough items in stock. Consult with the warehouse manager to get the available quantity of the product.";
                return RedirectToAction("ShowOrders");
            }
            Order newOrder = new Order
            {
                ProductId = prodId,
                CustomerId = custId,
                OrderedQty = Qty,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now 
            };
            _context.Orders.Add(newOrder);
            theProduct.TotalQty -= Qty;
            _context.SaveChanges();
            return RedirectToAction("ShowOrders");
        }
// ================================================================================================
    }
}
