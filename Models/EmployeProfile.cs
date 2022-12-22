

namespace our_site_asp_net.Models
{
    public class EmployeProfile
    {
        /*
        public EmployeProfile(DbContextOptions<EmployeProfile>options) : base(options)
        {

        }
        */
        public int id  { get; set; }
        public string employeName { get; set; }

        public string employeImage { get; set; }

        public string position { get; set; }

        public string schwerpunkte { get; set; }
        public string askMyanyThing { get; set; }
    }
}
