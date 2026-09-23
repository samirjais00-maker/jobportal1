using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    public class OrganizationViewModel
    {
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public string OrgAddress { get; set; }
        public string OrgContact { get; set; }
        public string OrgEmail { get; set; }
        public string OrgLocation { get; set; }
        public string OrgImage { get; set; }
        public List<OrganizationViewModel> OrganizationList { get; set; }
        public IEnumerable< SelectListItem> OrgList { get; set; }
        public OrganizationViewModel()
        {
            OrganizationList = new List<OrganizationViewModel>();
        }
    }
}
