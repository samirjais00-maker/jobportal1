using System.ComponentModel.DataAnnotations;

namespace JobPortal.EntityModel
{
    public class Organization: Common
    {
        public Organization() { }

        [Key]
        public  int OrgId { get; set; }
        public string OrgName { get; set; }
        public string OrgAddress { get; set; }
        public string OrgContact { get; set; }
        public string OrgEmail { get; set; }
        public string OrgLocation { get; set; }
        public string OrgImage { get; set; }


    }
}
