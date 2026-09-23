using JobPortal.Data;
using JobPortal.EntityModel;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace JobPortal.Controllers
{
    [Authorize]
public class OrganizationController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public OrganizationController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            OrganizationViewModel model = new OrganizationViewModel();
            model.OrganizationList = (from o in _applicationDbContext.Organization
                                      where o.DeletedDate == null
                                      select new OrganizationViewModel
                                      {
                                          OrgId = o.OrgId,
                                          OrgName = o.OrgName,
                                          OrgAddress = o.OrgAddress,
                                          OrgContact = o.OrgContact,
                                          OrgEmail = o.OrgEmail,
                                          OrgLocation = o.OrgLocation,
                                          OrgImage = o.OrgImage
                                      }).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrganizationViewModel model, IFormFile? file)
        {
            if (file != null)
            {
                var GuidId = Guid.NewGuid().ToString();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                    string fileName = GuidId + file.FileName;
                    string fileNameWithPath = Path.Combine(path, fileName);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    model.OrgImage = "/Files/" + GuidId + file.FileName;
                
            }
            Organization organizationEntity = new Organization();
            organizationEntity.OrgName = model.OrgName;
            organizationEntity.OrgAddress = model.OrgAddress;
            organizationEntity.OrgContact = model.OrgContact;
            organizationEntity.OrgEmail = model.OrgEmail;
            organizationEntity.OrgLocation = model.OrgLocation;
            organizationEntity.OrgImage = model.OrgImage;
            organizationEntity.CreatedDate = DateTime.Now;
            organizationEntity.CreatedBy = 1;
            _applicationDbContext.Entry(organizationEntity).State = EntityState.Added;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            OrganizationViewModel model = new OrganizationViewModel();
            var data = _applicationDbContext.Organization.Where(x => x.OrgId == id).FirstOrDefault();
            if (data != null)
            {
                model.OrgId = data.OrgId;
                model.OrgName = data.OrgName;
                model.OrgAddress = data.OrgAddress;
                model.OrgContact = data.OrgContact;
                model.OrgEmail = data.OrgEmail;
                model.OrgLocation = data.OrgLocation;
                model.OrgImage = data.OrgImage;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(OrganizationViewModel model, IFormFile? file)
        {
            if (file != null)
            {
                var GuidId = Guid.NewGuid().ToString();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                    string fileName = GuidId + file.FileName;
                    string fileNameWithPath = Path.Combine(path, fileName);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    model.OrgImage = "/Files/" + GuidId + file.FileName;
            }
            Organization organizationEntity = new Organization();
            organizationEntity.OrgId = model.OrgId;
            organizationEntity.OrgName = model.OrgName;
            organizationEntity.OrgAddress = model.OrgAddress;
            organizationEntity.OrgContact = model.OrgContact;
            organizationEntity.OrgEmail = model.OrgEmail;
            organizationEntity.OrgLocation = model.OrgLocation;
            organizationEntity.OrgImage = model.OrgImage;
            organizationEntity.UpdatedBy = 1;
            organizationEntity.UpdatedDate = DateTime.Now;
            _applicationDbContext.Entry(organizationEntity).State = EntityState.Modified;
            _applicationDbContext.Entry(organizationEntity).Property(x => x.CreatedBy).IsModified = false;
            _applicationDbContext.Entry(organizationEntity).Property(x => x.CreatedDate).IsModified = false;
            if (file == null)
            {
                _applicationDbContext.Entry(organizationEntity).Property(x => x.OrgImage).IsModified = false;
            }
            
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var data = _applicationDbContext.Organization.Where(x => x.OrgId == id).FirstOrDefault();
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