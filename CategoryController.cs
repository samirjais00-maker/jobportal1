using JobPortal.Data;
using JobPortal.Models;
using JobPortal.EntityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace JobPortal.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryController(ApplicationDbContext applicationDbContext) 
        { 
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            CategoryViewModel model = new CategoryViewModel();
            model.CategoryList = (from o in _applicationDbContext.Category
                                      where o.DeletedDate == null
                                  select new CategoryViewModel
                                  {
                                      CategoryId = o.CategoryId,
                                      CategoryName = o.CategoryName,
                                  }).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            Category categoryentity = new Category();
            categoryentity.CategoryId = model.CategoryId;
            categoryentity.CategoryName = model.CategoryName;
            categoryentity.CreatedDate = DateTime.Now;
            categoryentity.CreatedBy = 1;
            _applicationDbContext.Entry(categoryentity).State = EntityState.Added;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            CategoryViewModel model = new CategoryViewModel();
            var data = _applicationDbContext.Category.Where(x => x.CategoryId == id).FirstOrDefault();
            if (data != null)
            {
                model.CategoryId = data.CategoryId;
                model.CategoryName = data.CategoryName;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            Category categoryentity = new Category();
            categoryentity.CategoryId = model.CategoryId;
            categoryentity.CategoryName = model.CategoryName;
            categoryentity.UpdatedBy = 1;
            categoryentity.UpdatedDate = DateTime.Now;
            _applicationDbContext.Entry(categoryentity).State = EntityState.Modified;
            _applicationDbContext.Entry(categoryentity).Property(x => x.CreatedBy).IsModified = false;
            _applicationDbContext.Entry(categoryentity).Property(x => x.CreatedDate).IsModified = false;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var data = _applicationDbContext.Category.Where(x => x.CategoryId == id).FirstOrDefault();
            if (data != null)
            {
                data.DeletedBy = 1;
                data.DeletedDate = DateTime.Now;
                _applicationDbContext.Entry(data).State = EntityState.Modified;
                _applicationDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
