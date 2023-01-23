

using System.ComponentModel.DataAnnotations.Schema;

namespace our_site_asp_net.Models
{
    public class Jobs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [NotMapped]
        public IFormFile JobPhoto { get; set; }
        public string photoUrl { get; set; }
        public string currentCity { get; set; }
        public string JobContent { get; set; }
    }
}
