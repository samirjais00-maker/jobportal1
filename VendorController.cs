using JobPortal.Data;
using JobPortal.EntityModel;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Controllers
{
    [Authorize]
    public class VendorController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public VendorController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            VendorViewModel model = new VendorViewModel();
            model.VendorList = (from o in _applicationDbContext.Vendor
                               where o.DeletedDate == null
                               select new VendorViewModel
                               {
                                   VendorId = o.VendorId,
                                   VendorName = o.VendorName,
                                   VendorAddress = o.VendorAddress,
                                   VendorContact = o.VendorContact,
                                   VendorEmail = o.VendorEmail,
                                   VendorLocation = o.VendorLocation,
                               }).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VendorViewModel model)
        {
            Vendor vendorentity = new Vendor();
            vendorentity.VendorId = model.VendorId;
            vendorentity.VendorName = model.VendorName;
            vendorentity.VendorEmail = model.VendorEmail;
            vendorentity.VendorLocation = model.VendorLocation;
            vendorentity.VendorContact = model.VendorContact;
            vendorentity.VendorAddress = model.VendorAddress;
            vendorentity.CreatedDate = DateTime.Now;
            vendorentity.CreatedBy = 1;
            _applicationDbContext.Entry(vendorentity).State = EntityState.Added;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            VendorViewModel model = new VendorViewModel();
            var data = _applicationDbContext.Vendor.Where(x => x.VendorId == id).FirstOrDefault();
            if (data != null)
            {
                model.VendorId = data.VendorId;
                model.VendorName = data.VendorName;
                model.VendorAddress = data.VendorAddress;
                model.VendorEmail = data.VendorEmail;
                model.VendorLocation = data.VendorLocation;
                model.VendorContact = data.VendorContact;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(VendorViewModel model)
        {
            Vendor vendorentity = new Vendor();
            vendorentity.VendorId = model.VendorId;
            vendorentity.VendorName = model.VendorName;
            vendorentity.VendorEmail = model.VendorEmail;
            vendorentity.VendorLocation = model.VendorLocation;
            vendorentity.VendorContact = model.VendorContact;
            vendorentity.VendorAddress = model.VendorAddress;
            vendorentity.UpdatedBy = 1;
            vendorentity.UpdatedDate = DateTime.Now;
            _applicationDbContext.Entry(vendorentity).State = EntityState.Modified;
            _applicationDbContext.Entry(vendorentity).Property(x => x.CreatedBy).IsModified = false;
            _applicationDbContext.Entry(vendorentity).Property(x => x.CreatedDate).IsModified = false;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var data = _applicationDbContext.Vendor.Where(x => x.VendorId == id).FirstOrDefault();
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
