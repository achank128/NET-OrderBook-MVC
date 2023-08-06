using Microsoft.AspNetCore.Mvc;
using OrderBook.Data;
using OrderBook.Models;

namespace OrderBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Categories;
            return View(categoryList);
        }


        //Get
        public IActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category c)
        {
            if (ModelState.IsValid) { 
                _db.Categories.Add(c);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }


        //Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null) { 
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category c)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(c);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }

        public IActionResult Delete(int? id)
        {
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
