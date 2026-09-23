using System.ComponentModel.DataAnnotations;

namespace JobPortal.EntityModel
{
    public class JobDescription : Common
    {
        [Key]
        public int JobDescriptionId { get; set; }
        public int VendorOrgId { get; set; }
        public int CategoryId { get; set; }
        public string SubCategory { get; set; }
        public string Position { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Education { get; set; }
        public string Level { get; set; }
        public string NoOfVacancy { get; set; }
        public string Description { get; set; }
        public int Experience { get; set; }
        public string Salary { get; set; }
    }
}
