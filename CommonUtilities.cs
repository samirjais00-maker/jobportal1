using JobPortal.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobPortal.Utility
{
    public class CommonUtilities
    {

        public static IEnumerable<SelectListItem> GetVendorList(ApplicationDbContext context)
        {
            return context.Vendor
                .Select(o => new SelectListItem
                {
                    Value = o.VendorId.ToString(),
                    Text = o.VendorName
                })
                .ToList();
        }
        public static IEnumerable<SelectListItem> GetCategoryList(ApplicationDbContext context)
        {
            return context.Category
                .Select(o => new SelectListItem
                {
                    Value = o.CategoryId.ToString(),
                    Text = o.CategoryName
                })
                .ToList();
        }
    }
}
