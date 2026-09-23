using System.ComponentModel.DataAnnotations;

namespace JobPortal.EntityModel
{
    public class Vendor: Common
    {
        [Key]
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorContact { get; set; }
        public string VendorEmail { get; set; }
        public string VendorLocation { get; set; }
        
    }
}
