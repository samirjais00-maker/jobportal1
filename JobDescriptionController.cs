using JobPortal.Data;
using JobPortal.EntityModel;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Controllers
{
    [Authorize]
    public class JobDescriptionController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public JobDescriptionController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            JobDescriptionViewModel model = new JobDescriptionViewModel();
            model.JobDescriptionList= (from o in _applicationDbContext.JobDescription
                                       join v in _applicationDbContext.Vendor on o.VendorOrgId equals v.VendorId
                                       join c in _applicationDbContext.Category on o.CategoryId equals c.CategoryId
                                       where o.DeletedDate == null
                                       select new JobDescriptionViewModel
                                       {
                                            JobDescriptionId = o.JobDescriptionId,
                                            VendorOrgId = o.VendorOrgId,
                                            CategoryId =o.CategoryId,
                                            SubCategory =o.SubCategory,
                                            Position = o.Position,
                                            StartDate = o.StartDate,
                                            EndDate =o.EndDate,
                                            Education = o.Education,
                                            Level = o.Level,
                                            NoOfVacancy = o.NoOfVacancy,
                                            Description = o.Description,
                                            Experience = o.Experience,
                                            Salary = o.Salary,
                                            VendorName = v.VendorName,
                                            CategoryName = c.CategoryName,

                                       }).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            JobDescriptionViewModel model = new JobDescriptionViewModel();
            model. VendorList = Utility.CommonUtilities.GetVendorList(_applicationDbContext);
            model.CategoryList = Utility.CommonUtilities.GetCategoryList(_applicationDbContext);
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(JobDescriptionViewModel model)
        {
            JobDescription jobdescriptionentity = new JobDescription();
            jobdescriptionentity.JobDescriptionId = model.JobDescriptionId;
            jobdescriptionentity.VendorOrgId = model.VendorOrgId;
            jobdescriptionentity.CategoryId = model.CategoryId;
            jobdescriptionentity.SubCategory = model.SubCategory;
            jobdescriptionentity.Position = model.Position;
            jobdescriptionentity.StartDate = model.StartDate;
            jobdescriptionentity.EndDate = model.EndDate;
            jobdescriptionentity.Education = model.Education;
            jobdescriptionentity.Level = model.Level;
            jobdescriptionentity.NoOfVacancy = model.NoOfVacancy;
            jobdescriptionentity.Description = model.Description;
            jobdescriptionentity.Experience = model.Experience;
            jobdescriptionentity.Salary = model.Salary;
            jobdescriptionentity.CreatedDate = DateTime.Now;
            jobdescriptionentity.CreatedBy = 1;
            _applicationDbContext.Entry(jobdescriptionentity).State = EntityState.Added;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            JobDescriptionViewModel model = new JobDescriptionViewModel();
            var data = _applicationDbContext.JobDescription.Where(x => x.JobDescriptionId == id).FirstOrDefault();
            if (data != null)
            {
                model.JobDescriptionId = data.JobDescriptionId;
                model.VendorOrgId = data.VendorOrgId;
                model.CategoryId = data.CategoryId;
                model.SubCategory = data.SubCategory;
                model.Position = data.Position;
                model.StartDate = data.StartDate;
                model.EndDate = data.EndDate;
                model.Education = data.Education;
                model.Level = data.Level;
                model.NoOfVacancy = data.NoOfVacancy;
                model.Description = data.Description;
                model.Experience = data.Experience;
                model.Salary = data.Salary;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(JobDescriptionViewModel model)
        {
            JobDescription jobdescriptionentity = new JobDescription();
            jobdescriptionentity.JobDescriptionId = model.JobDescriptionId;
            jobdescriptionentity.VendorOrgId = model.VendorOrgId;
            jobdescriptionentity.CategoryId = model.CategoryId;
            jobdescriptionentity.SubCategory = model.SubCategory;
            jobdescriptionentity.Position = model.Position;
            jobdescriptionentity.StartDate = model.StartDate;
            jobdescriptionentity.EndDate = model.EndDate;
            jobdescriptionentity.Education = model.Education;
            jobdescriptionentity.Level = model.Level;
            jobdescriptionentity.NoOfVacancy = model.NoOfVacancy;
            jobdescriptionentity.Description = model.Description;
            jobdescriptionentity.Experience = model.Experience;
            jobdescriptionentity.Salary = model.Salary;
            jobdescriptionentity.UpdatedBy = 1;
            jobdescriptionentity.UpdatedDate = DateTime.Now;
            _applicationDbContext.Entry(jobdescriptionentity).State = EntityState.Modified;
            _applicationDbContext.Entry(jobdescriptionentity).Property(x => x.CreatedBy).IsModified = false;
            _applicationDbContext.Entry(jobdescriptionentity).Property(x => x.CreatedDate).IsModified = false;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var data = _applicationDbContext.JobDescription.Where(x => x.JobDescriptionId == id).FirstOrDefault();
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
