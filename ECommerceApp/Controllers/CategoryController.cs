using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        // constructor
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Views
        public IActionResult Index()
        {
            List<Category> objCategoryList = _context.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _context.Find<Category>(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            var category = _context.Find<Category>(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        
        
        // Access Data Logic 
        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order cannot exactly match the name");
            }

            if (category.Name != null && category.Name.ToLower() == "test")
            {
                ModelState.AddModelError("Name", "test is an invalid value");
            }
            // if add success the page will redirect to category list page
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index","Category");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _context.Find<Category>(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");

        }
    }
}