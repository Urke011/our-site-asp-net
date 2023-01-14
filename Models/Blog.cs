namespace our_site_asp_net.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime  Date { get; set; }
        public string TextDescription { get; set; }
    }
}
