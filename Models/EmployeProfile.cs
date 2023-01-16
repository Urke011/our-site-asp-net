

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace our_site_asp_net.Models
{
    public class EmployeProfile
    {
      
        public int id { get; set; }
        public string employeName { get; set; }

        public string position { get; set; }

        public string schwerpunkte { get; set; }
        public string askMyanyThing { get; set; }
        [NotMapped]
        [Display(Name = "Choose Employe photo")]
        public IFormFile EmployePhoto { get; set; }
        public string photoUrl { get; set; }
    }
}
