using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    public class VendorViewModel
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorContact { get; set; }
        public string VendorEmail { get; set; }
        public string VendorLocation { get; set; }
        public List<VendorViewModel> VendorList { get; set; }

        public VendorViewModel()
        {
            VendorList = new List<VendorViewModel>();
        }
    }
}
